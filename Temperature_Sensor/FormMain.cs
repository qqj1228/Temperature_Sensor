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
        private readonly LoggerCS.Logger m_log;
        private readonly Config m_cfg;
        private readonly Temper m_tester;
        private readonly SerialPortCS.SerialPortClass m_sp;
        private string m_serialRecvBuf;
        readonly System.Timers.Timer m_timerInterval;
        readonly System.Timers.Timer m_timerTotal;
        private DateTime m_start;

        public Main() {
            InitializeComponent();
            m_bTesting = false;
            m_lastHeight = this.Height;
            m_serialRecvBuf = "";
            this.Text = Properties.Resources.MainTitle + " Ver: " + MainFileVersion.AssemblyVersion;
            m_log = new LoggerCS.Logger(".\\log", LoggerCS.EnumLogLevel.LogLevelAll, true, 100);
            m_log.TraceInfo("==================================================================");
            m_log.TraceInfo("===================== START Ver: " + MainFileVersion.AssemblyVersion + " =====================");
            m_cfg = new Config(m_log);
            try {
                m_cfg.LoadConfigAll();
            } catch (ApplicationException ex) {
                MessageBox.Show(ex.Message.Split(':')[0] + "配置文件读取出错，将会使用默认配置", "初始化错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            m_tester = new Temper(m_log, m_cfg);
            if (!m_tester.TCPClientInit()) {
                MessageBox.Show("无法连接WiFi串口服务器", "初始化错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (m_cfg.Setting.Data.ScannerPort.Length > 0) {
                m_sp = new SerialPortCS.SerialPortClass(
                    m_cfg.Setting.Data.ScannerPort,
                    m_cfg.Setting.Data.ScannerBaud,
                    Parity.None,
                    8,
                    StopBits.One
                );
                try {
                    m_sp.OpenPort();
                    m_sp.DataReceived += new SerialPortCS.SerialPortClass.SerialPortDataReceiveEventArgs(SerialDataReceived);
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

        private void ResizeFont(Control control, float scale) {
            control.Font = new Font(control.Font.FontFamily, control.Font.Size * scale, control.Font.Style);
        }

        private void OnTimeInterval(object source, System.Timers.ElapsedEventArgs e) {
            m_tester.GetData(m_start);
            this.Invoke((EventHandler)delegate {
                this.chart1.DataBind();
            });
        }

        private void OnTimeTotal(object source, System.Timers.ElapsedEventArgs e) {
            m_timerInterval.Enabled = false;
            m_timerTotal.Enabled = false;
            DataTable dt = m_tester.GetDtTemper();
            m_tester.ExportResultFile();
            this.Invoke((EventHandler)delegate {
                this.lblInfo.Text = "检测结束";
            });
            m_bTesting = false;
        }

        private void StartTest() {
            m_log.TraceInfo(">>>>> Get VIN: " + m_tester.StrVIN + ", Ver: " + MainFileVersion.AssemblyVersion + "<<<<<");
            m_serialRecvBuf = "";
            this.Invoke((EventHandler)delegate {
                this.lblInfo.Text = "检测开始。。。";
            });
            m_tester.ClearPoints();
            m_timerInterval.Enabled = true;
            m_timerTotal.Enabled = true;
            m_start = DateTime.Now;
            m_tester.GetData(m_start);
            this.Invoke((EventHandler)delegate {
                this.chart1.DataBind();
            });
        }

        private void Main_Resize(object sender, EventArgs e) {
            if (m_lastHeight == 0) {
                return;
            }
            float scale = this.Height / m_lastHeight;
            ResizeFont(this.lblLogo, scale);
            ResizeFont(this.txtBoxVIN, scale);
            ResizeFont(this.label1, scale);
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

        private void Main_Load(object sender, EventArgs e) {
            this.chart1.Series[0].Name = "空调温度1";
            this.chart1.Series.Add("空调温度2");
            this.chart1.Series[1].Color = Color.SeaGreen;
            this.chart1.Series[0].ChartType = SeriesChartType.FastLine;
            this.chart1.Series[1].ChartType = SeriesChartType.FastLine;
            this.chart1.Series[0].BorderWidth = 2;
            this.chart1.Series[1].BorderWidth = 2;
            this.chart1.Series[0].XValueMember = "Time";
            this.chart1.Series[0].YValueMembers = "Temper1";
            this.chart1.Series[1].XValueMember = "Time";
            this.chart1.Series[1].YValueMembers = "Temper2";
            this.chart1.ChartAreas[0].AxisX.IsStartedFromZero = true;
            this.chart1.ChartAreas[0].AxisX.Minimum = 0;
            this.chart1.ChartAreas[0].AxisX.Interval = 1;
            this.chart1.ChartAreas[0].AxisX.Title = "时间（秒）";
            this.chart1.ChartAreas[0].AxisY.Title = "温度（℃）";
            this.chart1.DataSource = m_tester.GetDtTemper();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e) {
            if (m_sp != null) {
                m_sp.ClosePort();
            }
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
