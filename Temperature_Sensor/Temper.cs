using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Temperature_Sensor {
    public class Temper {
        private const int TotalChannel = 3; // RTD模块的通道总数
        private const string OverRange = "+9999"; // RTD模块读数超过上限时返回的字符串
        private const string UnderRange = "-0000"; // RTD模块读数超过下限时返回的字符串
        private const int NormalLength = 7; // RTD模块返回正常读数的字符串长度
        private const int TimeoutScale = 10; // 获取温度返回值的超时倍数
        private readonly BaseLib.Logger m_log;
        private readonly Config m_cfg;
        private readonly BaseLib.Model m_db;
        private readonly DataTable m_dtTemper;
        private readonly int m_bufSize;
        private readonly byte[] m_recvBuf;
        private TcpClient m_client;
        private NetworkStream m_clientStream;
        private bool m_bInited; // 是否初始化过标志，未初始化的话使用m_client.Connected或m_client.Client会抛出NullReference异常
        private bool m_bLastTestStatus;
        private string m_strTimeStamp; // 当前检测过程的时间戳

        public string StrVIN { get; set; }

        public Temper(BaseLib.Logger logger, Config config, BaseLib.Model db) {
            m_log = logger;
            m_cfg = config;
            m_db = db;
            m_dtTemper = new DataTable("Temper");
            ChartInit();
            m_bufSize = 4096;
            m_recvBuf = new byte[m_bufSize];
            m_bInited = false;
            m_bLastTestStatus = false;
        }

        ~Temper() {
            SafeClose();
        }

        private void SafeClose() {
            if (m_clientStream != null) {
                m_clientStream.Close();
            }
            if (m_client != null) {
                m_client.Close();
            }
        }

        public bool TCPClientInit(bool bLog = true) {
            try {
                m_client = new TcpClient(m_cfg.Setting.Data.TCPServerIP, m_cfg.Setting.Data.TCPServerPort);
                m_clientStream = m_client.GetStream();
                if (bLog || !m_bLastTestStatus) {
                    m_log.TraceInfo("TcpClient init success");
                }
                m_bInited = true;
            } catch (Exception ex) {
                if (m_clientStream != null) {
                    m_clientStream.Close();
                }
                if (m_client != null) {
                    m_client.Close();
                }
                if (bLog || m_bLastTestStatus) {
                    m_log.TraceError("TcpClient init error: " + ex.Message);
                }
                m_bInited = false;
            }
            m_bLastTestStatus = m_bInited;
            return m_bInited;
        }

        public bool GetInitStatus() {
            return m_bInited;
        }

        public DataTable GetDtTemper() {
            return m_dtTemper;
        }

        public BaseLib.Logger GetLogger() {
            return m_log;
        }

        public Config GetConfig() {
            return m_cfg;
        }

        public BaseLib.Model GetModel() {
            return m_db;
        }

        private void ChartInit() {
            m_dtTemper.Columns.Add("Time");
            m_dtTemper.Columns.Add("Temper1");
            m_dtTemper.Columns.Add("Temper2");
            m_dtTemper.Columns.Add("TemperSTD");
            DataRow dr = m_dtTemper.NewRow();
            dr["Time"] = "0.0";
            dr["Temper1"] = "20";
            dr["Temper2"] = "20";
            dr["TemperSTD"] = "20";
            m_dtTemper.Rows.Add(dr);
        }

        /// <summary>
        /// 获取I-7033的温度值，同步函数，会阻塞线程
        /// 以string[]格式返回，[通道0, 通道1]
        /// </summary>
        public string[] GetTemper() {
            // 获取当前连接状态
            bool bConnected = TestConnect();
            // 若断线的话重连
            if (!bConnected) {
                SafeClose();
                bConnected = TCPClientInit();
                if (!bConnected) {
                    return SplitTemper("");
                }
            }

            DateTime before = DateTime.Now;
            TimeSpan interval;

            // 发送读取温度命令
            string strMsg = "#01\r";
            byte[] sendMsg = Encoding.ASCII.GetBytes(strMsg);
            try {
                m_clientStream.Write(sendMsg, 0, sendMsg.Length);
                m_log.TraceInfo("TcpClient sent: " + strMsg.Replace("\r", "\\r"));
            } catch (Exception ex) {
                m_log.TraceError("TcpClient sending error: " + ex.Message);
                m_client.Close();
                bConnected = TCPClientInit();
            }

            // 接收温度值
            int bytesRead;
            string strRecv = "";
            if (bConnected) {
                try {
                    while (!strRecv.Contains("\r")) {
                        interval = DateTime.Now - before;
                        if (interval.TotalMilliseconds > m_cfg.Setting.Data.Interval * TimeoutScale) {
                            m_log.TraceWarning("TcpClient timeout receieved: " + strRecv.Replace("\r", "\\r"));
                            throw new ApplicationException("TcpClient receieving timeout");
                        }
                        while (m_clientStream.DataAvailable) {
                            bytesRead = m_clientStream.Read(m_recvBuf, 0, m_bufSize);
                            strRecv += Encoding.ASCII.GetString(m_recvBuf, 0, bytesRead);
                        }
                    }
                    m_log.TraceInfo("TcpClient receieved: " + strRecv.Replace("\r", "\\r"));
                } catch (Exception ex) {
                    m_log.TraceError("TcpClient receieving ERROR: " + ex.Message);
                }
            } else {
                m_log.TraceError("TcpClient can't connect server");
            }
            return SplitTemper(strRecv);
        }

        private string[] SplitTemper(string strMsg) {
            string[] result = new string[TotalChannel];
            try {
                if (strMsg.StartsWith(">") && strMsg.EndsWith("\r")) {
                    string strTemp = strMsg.Substring(1, strMsg.Length - 1).Trim();
                    int OverLen = OverRange.Length;
                    for (int i = 0; i < TotalChannel; i++) {
                        if (strTemp.StartsWith(OverRange) || strTemp.StartsWith(UnderRange)) {
                            result[i] = strTemp.Substring(0, OverLen);
                            strTemp = strTemp.Substring(OverLen);
                        } else if (strTemp.Length >= NormalLength) {
                            result[i] = strTemp.Substring(0, NormalLength);
                            strTemp = strTemp.Substring(NormalLength);
                        } else {
                            result[i] = "";
                        }
                    }
                } else {
                    for (int i = 0; i < TotalChannel; i++) {
                        result[i] = "";
                    }
                }
            } catch (Exception ex) {
                m_log.TraceError("SplitTemper() ERROR: " + ex.Message);
            }
            return result;
        }

        private string GetCHKSUM(string strMsg) {
            byte CHKSUM = 0;
            foreach (char item in strMsg) {
                CHKSUM += (byte)item;
            }
            return CHKSUM.ToString("X2");
        }

        private bool TestConnect() {
            if (!m_bInited) {
                return false;
            }
            Socket cltSocket = m_client.Client;
            bool bRet = false;
            // This is how you can determine whether a socket is still connected.
            bool blockingState = cltSocket.Blocking;
            try {
                byte[] tmp = new byte[1];
                cltSocket.Blocking = false;
                cltSocket.Send(tmp, 0, 0);
                bRet = true;
            } catch (SocketException ex) {
                // 10035 == WSAEWOULDBLOCK
                if (ex.NativeErrorCode.Equals(10035)) {
                    bRet = true;
                    m_log.TraceWarning(string.Format("Still Connected, but the Send would block[{0}]", ex.NativeErrorCode));
                } else {
                    bRet = false;
                    if (m_bLastTestStatus) {
                        m_log.TraceError(string.Format("Disconnected: {0}[{1}]", ex.Message, ex.NativeErrorCode));
                    }
                }
            } finally {
                m_bLastTestStatus = bRet;
                cltSocket.Blocking = blockingState;
            }
            return bRet;
        }

        public bool SafeTestConnect(int times = 3) {
            bool bRet = false;
            for (int i = 0; i < times; i++) {
                bRet = bRet || TestConnect();
            }
            for (int i = 0; i < times && !bRet; i++) {
                SafeClose();
                bRet = TCPClientInit(false);
            }
            return bRet;
        }

        public string[] GetData(DateTime start, string strSetup) {
            string[] tempers = GetTemper();
            DataRow dr = m_dtTemper.NewRow();
            TimeSpan interval = DateTime.Now - start;
            if (interval.TotalSeconds < 0.001) {
                interval = TimeSpan.Zero;
            }
            dr["Time"] = interval.TotalSeconds.ToString("F1");
            dr["Temper1"] = (tempers[0] != OverRange && tempers[0] != UnderRange) ? tempers[0] : null;
            dr["Temper2"] = (tempers[1] != OverRange && tempers[1] != UnderRange) ? tempers[1] : null;
            dr["TemperSTD"] = strSetup;
            m_dtTemper.Rows.Add(dr);
            m_log.TraceInfo(string.Format("GetData: Time[{0}], Temper1[{1}], Temper2[{2}], TemperSTD[{3}]", dr["Time"], dr["Temper1"], dr["Temper2"], dr["TemperSTD"]));
            return tempers;
        }

        public void ClearPoints() {
            m_dtTemper.Rows.Clear();
        }

        public string GetTimeStamp() {
            m_strTimeStamp = DateTime.Now.ToLocalTime().ToString("yyyyMMdd-HHmmss");
            return m_strTimeStamp;
        }

        private string GetFormatedTimeStamp() {
            if (m_strTimeStamp.Length < 15) {
                return "";
            }
            string strRet = string.Format("{0}-{1}-{2}", m_strTimeStamp.Substring(0, 4), m_strTimeStamp.Substring(4, 2), m_strTimeStamp.Substring(6, 2));
            strRet += string.Format("_{0}:{1}:{2}", m_strTimeStamp.Substring(9, 2), m_strTimeStamp.Substring(11, 2), m_strTimeStamp.Substring(13, 2));
            return strRet;
        }

        public void ExportResultFile(bool bResult, string strTimeStamp) {
            string ExportPath = ".\\Export\\" + DateTime.Now.ToLocalTime().ToString("yyyy-MM") + "\\" + DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd");
            if (!Directory.Exists(ExportPath)) {
                Directory.CreateDirectory(ExportPath);
            }
            ExportPath += "\\" + StrVIN + "_" + strTimeStamp + ".xlsx";
            using (ExcelPackage package = new ExcelPackage()) {
                // 添加工作表/worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("空调温度检测数据");
                // 检测信息及结果
                worksheet.Cells[1, 1].Value = "VIN: " + StrVIN;
                worksheet.Cells[2, 1].Value = "检测时间: " + GetFormatedTimeStamp();
                worksheet.Cells[3, 1].Value = "检测结果: " + (bResult ? "合格" : "不合格");
                // 格式化检测信息及结果
                for (int i = 0; i < 3; i++) {
                    using (ExcelRange range = worksheet.Cells[i + 1, 1, i + 1, m_dtTemper.Columns.Count]) {
                        // 合并单元格
                        range.Merge = true;
                        // 边框
                        range.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                        // 格式化
                        range.Style.Font.Bold = true;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }
                }
                // 标题
                int startRow = 4;
                for (int i = 0; i < m_dtTemper.Columns.Count; i++) {
                    worksheet.Cells[startRow, i + 1].Value = m_dtTemper.Columns[i].ColumnName;
                    // 边框
                    worksheet.Cells[startRow, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                }
                // 格式化标题
                using (ExcelRange range = worksheet.Cells[startRow, 1, startRow, m_dtTemper.Columns.Count]) {
                    range.Style.Font.Bold = true;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }
                // 记录
                for (int iRow = 0; iRow < m_dtTemper.Rows.Count; iRow++) {
                    for (int iCol = 0; iCol < m_dtTemper.Columns.Count; iCol++) {
                        worksheet.Cells[startRow + iRow + 1, iCol + 1].Value = m_dtTemper.Rows[iRow][iCol].ToString();
                        // 边框
                        worksheet.Cells[startRow + iRow + 1, iCol + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    }
                }
                // 格式化记录
                if (m_dtTemper.Rows.Count > 0) {
                    using (ExcelRange range = worksheet.Cells[startRow + 1, 1, startRow + m_dtTemper.Rows.Count, m_dtTemper.Columns.Count]) {
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }
                }
                // 自适应列宽
                worksheet.Cells.AutoFitColumns(0);
                // 保存文件
                FileInfo xlFile = new FileInfo(ExportPath);
                package.SaveAs(xlFile);
            }
        }

        public bool GetResult1(double dSTD, out double dAverage1, out double dAverage2) {
            bool bResult;
            int counter1 = 0;
            int counter2 = 0;
            dAverage1 = 0;
            dAverage2 = 0;
            for (int i = 0; i < m_dtTemper.Rows.Count; i++) {
                bResult = double.TryParse(m_dtTemper.Rows[i]["Temper1"].ToString(), out double iTemper);
                if (bResult) {
                    dAverage1 += iTemper;
                    ++counter1;
                }
                bResult = double.TryParse(m_dtTemper.Rows[i]["Temper2"].ToString(), out iTemper);
                if (bResult) {
                    dAverage2 += iTemper;
                    ++counter2;
                }
            }
            dAverage1 /= counter1;
            dAverage2 /= counter2;
            bool bRet = false;
            if (m_cfg.Setting.Data.Cooling) {
                switch (m_cfg.Setting.Data.Temper) {
                case 0:
                    if (dSTD >= dAverage1 && dSTD >= dAverage2) {
                        bRet = true;
                    }
                    break;
                case 1:
                    if (dSTD >= dAverage1) {
                        bRet = true;
                    }
                    break;
                case 2:
                    if (dSTD >= dAverage2) {
                        bRet = true;
                    }
                    break;
                }
            } else {
                switch (m_cfg.Setting.Data.Temper) {
                case 0:
                    if (dSTD <= dAverage1 && dSTD <= dAverage2) {
                        bRet = true;
                    }
                    break;
                case 1:
                    if (dSTD <= dAverage1) {
                        bRet = true;
                    }
                    break;
                case 2:
                    if (dSTD <= dAverage2) {
                        bRet = true;
                    }
                    break;
                }
            }
            return bRet;
        }

        public bool GetResult2(double dSTD, out double dCount1, out double dCount2) {
            bool bRet = false;
            bool bResult;
            dCount1 = 0;
            dCount2 = 0;
            List<double> counts1 = new List<double>();
            List<double> counts2 = new List<double>();
            for (int i = 0; i < m_dtTemper.Rows.Count; i++) {
                bResult = double.TryParse(m_dtTemper.Rows[i]["Temper1"].ToString(), out double iTemper);
                if (m_cfg.Setting.Data.Cooling) {
                    if (bResult && dSTD >= iTemper) {
                        ++dCount1;
                    } else {
                        counts1.Add(dCount1);
                        dCount1 = 0;
                    }
                } else {
                    if (bResult && dSTD <= iTemper) {
                        ++dCount1;
                    } else {
                        counts1.Add(dCount1);
                        dCount1 = 0;
                    }
                }
                bResult = double.TryParse(m_dtTemper.Rows[i]["Temper2"].ToString(), out iTemper);
                if (m_cfg.Setting.Data.Cooling) {
                    if (bResult && dSTD >= iTemper) {
                        ++dCount2;
                    } else {
                        counts2.Add(dCount2);
                        dCount2 = 0;
                    }
                } else {
                    if (bResult && dSTD <= iTemper) {
                        ++dCount2;
                    } else {
                        counts2.Add(dCount2);
                        dCount2 = 0;
                    }
                }
            }
            dCount1 = Math.Max(counts1.Max(), dCount1);
            dCount2 = Math.Max(counts2.Max(), dCount2);
            if (m_cfg.Setting.Data.SuccessiveValue > 1) {
                // 绝对值
                switch (m_cfg.Setting.Data.Temper) {
                case 0:
                    if (m_cfg.Setting.Data.SuccessiveValue <= dCount1 && m_cfg.Setting.Data.SuccessiveValue <= dCount2) {
                        bRet = true;
                    }
                    break;
                case 1:
                    if (m_cfg.Setting.Data.SuccessiveValue <= dCount1) {
                        bRet = true;
                    }
                    break;
                case 2:
                    if (m_cfg.Setting.Data.SuccessiveValue <= dCount2) {
                        bRet = true;
                    }
                    break;
                }
            } else {
                // 比率
                dCount1 /= m_dtTemper.Rows.Count;
                dCount2 /= m_dtTemper.Rows.Count;
                switch (m_cfg.Setting.Data.Temper) {
                case 0:
                    if (m_cfg.Setting.Data.SuccessiveValue <= dCount1 && m_cfg.Setting.Data.SuccessiveValue <= dCount2) {
                        bRet = true;
                    }
                    break;
                case 1:
                    if (m_cfg.Setting.Data.SuccessiveValue <= dCount1) {
                        bRet = true;
                    }
                    break;
                case 2:
                    if (m_cfg.Setting.Data.SuccessiveValue <= dCount2) {
                        bRet = true;
                    }
                    break;
                }
            }
            return bRet;
        }

        public void WriteDB(bool bResult, string strTimeStamp) {
            DataRow dr;
            string[] columns = m_db.GetTableColumns("TemperData");
            DataTable dtData = new DataTable("TemperData");
            foreach (string col in columns) {
                dtData.Columns.Add(col);
            }
            string SN = m_cfg.Setting.Data.TCPServerIP.Split('.')[3] + "-" + strTimeStamp;
            for (int i = 0; i < m_dtTemper.Rows.Count; i++) {
                dr = dtData.NewRow();
                dr["SN"] = SN;
                dr["VIN"] = StrVIN;
                dr["Time"] = m_dtTemper.Rows[i]["Time"];
                dr["Temper1"] = m_dtTemper.Rows[i]["Temper1"];
                dr["Temper2"] = m_dtTemper.Rows[i]["Temper2"];
                dr["TemperSTD"] = m_dtTemper.Rows[i]["TemperSTD"];
                dtData.Rows.Add(dr);
            }
            m_db.InsertRecords(dtData);

            columns = m_db.GetTableColumns("TemperResult");
            DataTable dtResult = new DataTable("TemperResult");
            foreach (string col in columns) {
                dtResult.Columns.Add(col);
            }
            dr = dtResult.NewRow();
            dr["SN"] = SN;
            dr["VIN"] = StrVIN;
            dr["Result"] = bResult ? "1" : "0";
            dtResult.Rows.Add(dr);
            m_db.InsertRecords(dtResult);
        }
    }
}
