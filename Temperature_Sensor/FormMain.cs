using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Temperature_Sensor {
    public partial class Main : Form {
        private static bool m_bTesting;
        private float m_lastHeight;
        private readonly BaseLib.Logger m_log;
        private readonly Config m_cfg;
        private readonly BaseLib.Model m_db;
        private readonly Temper m_tester;
        private readonly BaseLib.SerialPortClass m_sp;
        private string m_serialRecvBuf;
        private readonly System.Timers.Timer m_timerInterval;
        private readonly System.Timers.Timer m_timerTotal;
        readonly System.Timers.Timer m_timerTick;
        private DateTime m_start;
        private double m_dSetup;
        private bool m_bLastStatus;
        private static object m_lockObj;
        private int m_counterFailed;
        private bool m_bLoop; // 是否需要持续测温，用于调试

        public Main() {
            InitializeComponent();
            m_bTesting = false;
            m_lastHeight = this.Height;
            m_serialRecvBuf = "";
            m_bLastStatus = false;
            m_lockObj = new object();
            m_counterFailed = 0;
            m_bLoop = false;
            this.Text = Properties.Resources.MainTitle + " Ver: " + MainFileVersion.AssemblyVersion;
            m_log = new BaseLib.Logger(".\\log", BaseLib.EnumLogLevel.LogLevelAll, true, 100);
            m_log.TraceInfo("==================================================================");
            m_log.TraceInfo("===================== START Ver: " + MainFileVersion.AssemblyVersion + " =====================");
            m_cfg = new Config(m_log);
            try {
                m_cfg.LoadConfigAll();
            } catch (ApplicationException ex) {
                MessageBox.Show(ex.Message.Split(':')[0] + "配置文件读取出错，将会使用默认配置", "初始化错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            m_db = new BaseLib.Model("TemperSensor", m_cfg, m_log);
            m_tester = new Temper(m_log, m_cfg, m_db);
            m_tester.TCPClientInit();
            if (!m_tester.GetInitStatus()) {
                MessageBox.Show("无法连接WiFi串口服务器", "初始化错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (m_cfg.Setting.Data.ScannerPort.Length > 0) {
                m_sp = new BaseLib.SerialPortClass(
                    m_cfg.Setting.Data.ScannerPort,
                    m_cfg.Setting.Data.ScannerBaud,
                    Parity.None,
                    8,
                    StopBits.One
                );
                try {
                    m_sp.OpenPort();
                    m_sp.DataReceived += new BaseLib.SerialPortClass.SerialPortDataReceiveEventArgs(SerialDataReceived);
                } catch (Exception ex) {
                    m_log.TraceError("Open serial port error: " + ex.Message);
                    MessageBox.Show("打开串口扫码枪出错", "初始化错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            m_timerInterval = new System.Timers.Timer(m_cfg.Setting.Data.Interval);
            m_timerInterval.Elapsed += new System.Timers.ElapsedEventHandler(OnTimeInterval);
            m_timerInterval.AutoReset = true;
            m_timerInterval.Enabled = false;
            m_timerTotal = new System.Timers.Timer(m_cfg.Setting.Data.TotalTime + m_cfg.Setting.Data.Interval);
            m_timerTotal.Elapsed += new System.Timers.ElapsedEventHandler(OnTimeTotal);
            m_timerTotal.AutoReset = true;
            m_timerTotal.Enabled = false;
            m_timerTick = new System.Timers.Timer(m_cfg.Setting.Data.Interval); // 心跳间隔同采样间隔一致
            m_timerTick.Elapsed += new System.Timers.ElapsedEventHandler(OnTimeTick);
            m_timerTick.AutoReset = true;
            m_timerTick.Enabled = true;
        }

        void SerialDataReceived(object sender, SerialDataReceivedEventArgs e, byte[] bits) {
            Control con = this.ActiveControl;
            if (con is TextBox tb) {
                m_serialRecvBuf += Encoding.Default.GetString(bits);
                if (m_serialRecvBuf.Contains("\n")) {
                    if (m_bTesting) {
                        this.Invoke((EventHandler)delegate {
                            this.txtBoxVIN.SelectAll();
                            MessageBox.Show("上一辆车还未完全结束检测过程，请稍后再试", "OBD检测出错", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        });
                        m_serialRecvBuf = "";
                        return;
                    }
                    m_serialRecvBuf = m_serialRecvBuf.Trim();
                    this.Invoke((EventHandler)delegate {
                        this.txtBoxVIN.Text = m_serialRecvBuf;
                    });
                    if (m_serialRecvBuf.Length == 17) {
                        m_bTesting = true;
                        m_tester.StrVIN = m_serialRecvBuf;
                        Task.Factory.StartNew(StartTest);
                    }
                }
            }
        }

        private CancellationTokenSource UpdateUITask(string strMsg, int msDelay) {
            CancellationTokenSource tokenSource = new CancellationTokenSource(msDelay);
            CancellationToken token = tokenSource.Token;
            Task.Factory.StartNew(() => {
                int total = msDelay / 1000;
                int count = 0;
                while (!token.IsCancellationRequested) {
                    try {
                        this.Invoke((EventHandler)delegate {
                            this.lblInfo.ForeColor = Color.Black;
                            if (count == 0) {
                                this.lblInfo.Text = strMsg + "。。。";
                            } else {
                                this.lblInfo.Text = strMsg + "，剩余" + (total - count).ToString() + "秒";
                            }
                        });
                    } catch (ObjectDisposedException ex) {
                        m_log.TraceWarning(ex.Message);
                    }
                    Thread.Sleep(1000);
                    if (total > count) {
                        ++count;
                    }
                }
            }, token);
            return tokenSource;
        }

        private void ResizeFont(Control control, float scale) {
            control.Font = new Font(control.Font.FontFamily, control.Font.Size * scale, control.Font.Style);
        }

        private void OnTimeInterval(object source, System.Timers.ElapsedEventArgs e) {
            m_timerTick.Enabled = false;
            string[] tempers = m_tester.GetData(m_start, m_dSetup.ToString());
            try {
                this.Invoke((EventHandler)delegate {
                    this.chart1.DataBind();
                    this.lblTemper1.Text = GetDisplay(tempers[0]) + "℃";
                    this.lblTemper2.Text = GetDisplay(tempers[1]) + "℃";
                });
            } catch (ObjectDisposedException ex) {
                m_log.TraceWarning(ex.Message);
            }
        }

        private void OnTimeTotal(object source, System.Timers.ElapsedEventArgs e) {
            m_timerInterval.Enabled = false;
            m_timerTotal.Enabled = false;
            m_timerTick.Enabled = true;
            bool bResult = false;
            double dAverage1 = 0;
            double dAverage2 = 0;
            double dCount1 = 0;
            double dCount2 = 0;
            switch (m_cfg.Setting.Data.Rule) {
            case 1:
                bResult = m_tester.GetResult1(m_dSetup, out dAverage1, out dAverage2);
                break;
            case 2:
                bResult = m_tester.GetResult2(m_dSetup, out dCount1, out dCount2);
                break;
            }
            string strTimeStamp = m_tester.GetTimeStamp();
            try {
                m_tester.ExportResultFile(bResult, strTimeStamp);
            } catch (Exception ex) {
                m_log.TraceError(ex.Message);
                MessageBox.Show(ex.Message, "ExportResultFile() ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!m_bLoop) {
                try {
                    m_tester.WriteDB(bResult, strTimeStamp);
                } catch (ApplicationException ex) {
                    MessageBox.Show(ex.Message, "WriteDB() ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } catch (Exception ex) {
                    m_log.TraceError(ex.Message);
                    MessageBox.Show(ex.Message, "WriteDB() ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (bResult) {
                    m_counterFailed = 0;
                } else {
                    ++m_counterFailed;
                }
                this.Invoke((EventHandler)delegate {
                    if (bResult) {
                        this.lblInfo.ForeColor = Color.Green;
                        this.lblInfo.Text = "合格";
                    } else {
                        this.lblInfo.ForeColor = Color.Red;
                        this.lblInfo.Text = "不合格";
                    }
                    switch (m_cfg.Setting.Data.Rule) {
                    case 1:
                        this.lblInfo.Text += ", 平均温度: ";
                        switch (m_cfg.Setting.Data.Temper) {
                        case 0:
                            this.lblInfo.Text += dAverage1.ToString("F2") + "℃/" + dAverage2.ToString("F2") + "℃";
                            break;
                        case 1:
                            this.lblInfo.Text += dAverage1.ToString("F2") + "℃";
                            break;
                        case 2:
                            this.lblInfo.Text += dAverage2.ToString("F2") + "℃";
                            break;
                        }
                        break;
                    case 2:
                        this.lblInfo.Text += ", 连续合格温度点";
                        if (m_cfg.Setting.Data.SuccessiveValue > 1) {
                            // 绝对值
                            switch (m_cfg.Setting.Data.Temper) {
                            case 0:
                                this.lblInfo.Text += "个数: " + dCount1.ToString("F0") + "个/" + dCount2.ToString("F0") + "个";
                                break;
                            case 1:
                                this.lblInfo.Text += dCount1.ToString("F0") + "个";
                                break;
                            case 2:
                                this.lblInfo.Text += dCount2.ToString("F0") + "个";
                                break;
                            }
                        } else {
                            // 比率
                            switch (m_cfg.Setting.Data.Temper) {
                            case 0:
                                this.lblInfo.Text += "占比: " + (dCount1 * 100).ToString("F2") + "%/" + (dCount2 * 100).ToString("F2") + "%";
                                break;
                            case 1:
                                this.lblInfo.Text += (dCount1 * 100).ToString("F2") + "%";
                                break;
                            case 2:
                                this.lblInfo.Text += (dCount2 * 100).ToString("F2") + "%";
                                break;
                            }
                        }
                        break;
                    }
                    if (m_counterFailed >= m_cfg.Setting.Data.TestFailedQty) {
                        this.lblInfo.Text += ", 已连续" + m_counterFailed.ToString() + "辆车不合格";
                        this.lblInfo.BackColor = Color.Red;
                        this.lblInfo.ForeColor = Color.White;
                    }
                });
            } else {
                StartTest();
            }
            m_bTesting = false;
        }

        private void OnTimeTick(object source, System.Timers.ElapsedEventArgs e) {
            lock (m_lockObj) {
                bool bStatus = m_tester.SafeTestConnect(1);
                if (m_bLastStatus != bStatus) {
                    if (bStatus) {
                        this.Invoke((EventHandler)delegate {
                            this.lblStatus1.BackColor = Color.GreenYellow;
                            this.lblStatus2.BackColor = Color.GreenYellow;
                            this.lblStatus2.Text = "已连接";
                        });
                    } else {
                        this.Invoke((EventHandler)delegate {
                            this.lblStatus1.BackColor = Color.Red;
                            this.lblStatus2.BackColor = Color.Red;
                            this.lblStatus2.Text = "连接异常";
                        });
                    }
                }
                m_bLastStatus = bStatus;
            }
        }

        private void StartTest() {
            m_log.TraceInfo(">>>>> Get VIN: " + m_tester.StrVIN + ", Ver: " + MainFileVersion.AssemblyVersion + " <<<<<");
            m_serialRecvBuf = "";
            this.Invoke((EventHandler)delegate {
                this.lblSurrounding.Text = "--℃";
                this.lblSetup.Text = "--℃";
                this.lblTemper1.Text = "--℃";
                this.lblTemper2.Text = "--℃";
                this.lblInfo.BackColor = this.lblLogo.BackColor;
                this.lblInfo.ForeColor = this.lblLogo.ForeColor;
                this.lblInfo.Text = "获取环境温度";
            });
            // 获取环境温度
            string surrounding = GetDisplay(m_tester.GetTemper()[0]);
            if (double.TryParse(surrounding, out double value)) {
                if (m_cfg.Setting.Data.SetupValue > 1) {
                    // 绝对值
                    if (m_cfg.Setting.Data.Cooling) {
                        m_dSetup = value - m_cfg.Setting.Data.SetupValue;
                    } else {
                        m_dSetup = value + m_cfg.Setting.Data.SetupValue;
                    }
                } else {
                    // 比率
                    if (m_cfg.Setting.Data.Cooling) {
                        m_dSetup = value * (1 - m_cfg.Setting.Data.SetupValue);
                    } else {
                        m_dSetup = value * (1 + m_cfg.Setting.Data.SetupValue);
                    }
                }
                // 等待开启空调
                this.Invoke((EventHandler)delegate {
                    this.lblInfo.BackColor = this.lblLogo.BackColor;
                    this.lblInfo.ForeColor = this.lblLogo.ForeColor;
                    this.lblInfo.Text = "启动车辆，打开空调，开至最大";
                });
                if (m_cfg.Setting.Data.UsingRPM) {
                    while (m_cfg.Setting.Data.IdleRPMMin > GetRPM()) {
                        Thread.Sleep(m_cfg.Setting.Data.Interval);
                    }
                }
                // 开始检测
                m_tester.ClearPoints();
                m_timerInterval.Enabled = true;
                m_timerTotal.Enabled = true;
                m_timerTick.Enabled = false;
                m_start = DateTime.Now;
                string[] tempers = m_tester.GetData(m_start, m_dSetup.ToString());
                this.Invoke((EventHandler)delegate {
                    this.lblSurrounding.Text = surrounding + "℃";
                    this.lblSetup.Text = m_dSetup.ToString("F2") + "℃";
                    this.lblTemper1.Text = GetDisplay(tempers[0]) + "℃";
                    this.lblTemper2.Text = GetDisplay(tempers[1]) + "℃";
                    this.lblInfo.BackColor = this.lblLogo.BackColor;
                    this.lblInfo.ForeColor = this.lblLogo.ForeColor;
                    UpdateUITask("正在检测", m_cfg.Setting.Data.TotalTime);
                    this.chart1.DataBind();
                });
            } else {
                this.Invoke((EventHandler)delegate {
                    this.lblInfo.BackColor = this.lblLogo.BackColor;
                    this.lblInfo.ForeColor = Color.Red;
                    this.lblInfo.Text = "获取环境温度失败";
                });
            }
        }

        private void Main_Resize(object sender, EventArgs e) {
            if (m_lastHeight == 0) {
                return;
            }
            float scale = this.Height / m_lastHeight;
            ResizeFont(this.lblLogo, scale);
            ResizeFont(this.txtBoxVIN, scale);
            ResizeFont(this.label1, scale);
            ResizeFont(this.lblInfo, scale);
            ResizeFont(this.lblStatus1, scale);
            ResizeFont(this.lblStatus2, scale);
            ResizeFont(this.grpBoxSurrounding, scale);
            ResizeFont(this.lblSurrounding, scale);
            ResizeFont(this.grpBoxSetup, scale);
            ResizeFont(this.lblSetup, scale);
            ResizeFont(this.grpBoxTemper1, scale);
            ResizeFont(this.lblTemper1, scale);
            ResizeFont(this.grpBoxTemper2, scale);
            ResizeFont(this.lblTemper2, scale);
            ResizeFont(this.grpBoxMode, scale);
            ResizeFont(this.lblMode, scale);
            ResizeFont(this.grpBoxRPM, scale);
            ResizeFont(this.lblRPM, scale);
            m_lastHeight = this.Height;
        }

        private void TxtBoxVIN_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Enter) {
                if (m_bTesting) {
                    this.txtBoxVIN.SelectAll();
                    MessageBox.Show("上一辆车还未完全结束检测过程，请稍后再试", "检测出错", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                TextBox tb = sender as TextBox;
                if (tb.Text.Length == 17) {
                    m_bTesting = true;
                    m_tester.StrVIN = tb.Text;
                    CancellationTokenSource tokenSource = new CancellationTokenSource(m_cfg.Setting.Data.Interval);
                    Task.Factory.StartNew(StartTest, tokenSource.Token);
                    this.txtBoxVIN.SelectAll();
                }
            }
        }

        private string GetDisplay(string strValue) {
            bool bMinus = strValue.StartsWith("-");
            string strRet = strValue.Replace("+", "").Replace("-", "").Trim('0');
            strRet = bMinus ? "-" + strRet : strRet;
            return strRet;
        }

        private int GetRPM() {
            int iRet = 0;
            this.Invoke((EventHandler)delegate {
                this.pgrBarRPM.Value = iRet;
                this.lblRPM.Text = iRet.ToString();
            });
            return iRet;
        }

        private void Main_Load(object sender, EventArgs e) {
            this.chart1.Series[0].Name = "空调温度1";
            this.chart1.Series.Add("空调温度2");
            this.chart1.Series.Add("设定温度");
            this.chart1.Series[1].Color = Color.SeaGreen;
            this.chart1.Series[2].Color = Color.Red;
            this.chart1.Series[0].ChartType = SeriesChartType.FastLine;
            this.chart1.Series[1].ChartType = SeriesChartType.FastLine;
            this.chart1.Series[2].ChartType = SeriesChartType.FastLine;
            this.chart1.Series[0].BorderWidth = 2;
            this.chart1.Series[1].BorderWidth = 2;
            this.chart1.Series[2].BorderWidth = 2;
            this.chart1.Series[0].XValueMember = "Time";
            this.chart1.Series[0].YValueMembers = "Temper1";
            this.chart1.Series[1].XValueMember = "Time";
            this.chart1.Series[1].YValueMembers = "Temper2";
            this.chart1.Series[2].XValueMember = "Time";
            this.chart1.Series[2].YValueMembers = "TemperSTD";
            this.chart1.ChartAreas[0].AxisX.IsStartedFromZero = true;
            this.chart1.ChartAreas[0].AxisX.Minimum = 0;
            this.chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            //this.chart1.ChartAreas[0].AxisX.Interval = 1;
            this.chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            this.chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Gray;
            this.chart1.ChartAreas[0].AxisX.Title = "时间（秒）";
            this.chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            this.chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;
            this.chart1.ChartAreas[0].AxisY.Title = "温度（℃）";
            this.chart1.DataSource = m_tester.GetDtTemper();
            // 测温站编号，取Wifi串口服务器的IP地址最后一位十进制值
            this.lblStatus1.Text = "测温站:" + m_cfg.Setting.Data.TCPServerIP.Split('.')[3];
            this.lblSurrounding.Text = "--℃";
            this.lblSetup.Text = "--℃";
            this.lblTemper1.Text = "--℃";
            this.lblTemper2.Text = "--℃";
            switch (m_cfg.Setting.Data.Temper) {
            case 0:
                this.lblTemper1.ForeColor = Color.Green;
                this.lblTemper2.ForeColor = Color.Green;
                break;
            case 1:
                this.lblTemper1.ForeColor = Color.Green;
                break;
            case 2:
                this.lblTemper2.ForeColor = Color.Green;
                break;
            }
            if (m_cfg.Setting.Data.Cooling) {
                this.lblMode.ForeColor = Color.DodgerBlue;
                this.lblMode.Text = "制冷";
            } else {
                this.lblMode.ForeColor = Color.Red;
                this.lblMode.Text = "制热";
            }
            this.lblRPM.Text = "0RPM";
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e) {
            if (m_sp != null) {
                m_sp.ClosePort();
            }
            m_timerInterval.Enabled = false;
            m_timerTotal.Enabled = false;
            m_timerTick.Enabled = false;
            m_timerInterval.Dispose();
            m_timerTotal.Dispose();
            m_timerTick.Dispose();
        }

        private void MenuItemStatistics_Click(object sender, EventArgs e) {
            FormStatistics form = new FormStatistics(m_tester);
            form.ShowDialog();
            form.Dispose();
        }

        private void MenuItemLoop_Click(object sender, EventArgs e) {
            m_bLoop = true;
            StartTest();
        }
    }

    public static class MainFileVersion {
        public static Version AssemblyVersion {
            get { return ((Assembly.GetEntryAssembly()).GetName()).Version; }
        }

        public static Version AssemblyFileVersion {
            get { return new Version(FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).FileVersion); }
        }

        public static string AssemblyInformationalVersion {
            get { return FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).ProductVersion; }
        }
    }

}
