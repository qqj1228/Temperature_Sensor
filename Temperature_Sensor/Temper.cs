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
        private readonly LoggerCS.Logger m_log;
        private readonly Config m_cfg;
        private readonly DataTable m_dtTemper;
        private readonly int m_bufSize;
        private readonly byte[] m_recvBuf;
        private TcpClient m_client;
        private NetworkStream m_clientStream;

        public string StrVIN { get; set; }

        public Temper(LoggerCS.Logger logger, Config config) {
            m_log = logger;
            m_cfg = config;
            m_dtTemper = new DataTable("Temper");
            ChartInit();
            m_bufSize = 4096;
            m_recvBuf = new byte[m_bufSize];
        }

        ~Temper() {
            if (m_clientStream != null) {
                m_clientStream.Close();
            }
            if (m_client != null) {
                m_client.Close();
            }
        }

        public bool TCPClientInit() {
            try {
                m_client = new TcpClient(m_cfg.Setting.Data.TCPServerIP, m_cfg.Setting.Data.TCPServerPort);
                m_clientStream = m_client.GetStream();
                return true;
            } catch (Exception ex) {
                if (m_clientStream != null) {
                    m_clientStream.Close();
                }
                if (m_client != null) {
                    m_client.Close();
                }
                m_log.TraceError("TcpClient init error: " + ex.Message);
                return false;
            }
        }

        public DataTable GetDtTemper() {
            return m_dtTemper;
        }

        private void ChartInit() {
            m_dtTemper.Columns.Add("Time");
            m_dtTemper.Columns.Add("Temper1");
            m_dtTemper.Columns.Add("Temper2");
            DataRow dr = m_dtTemper.NewRow();
            dr["Time"] = "0.0";
            dr["Temper1"] = "20";
            dr["Temper2"] = "20";
            m_dtTemper.Rows.Add(dr);
        }

        /// <summary>
        /// 获取I-7033的温度值，同步函数，会阻塞线程
        /// 以string[]格式返回，[通道0, 通道1]
        /// </summary>
        public string[] GetTemper() {
            bool bConnected = true;
            string strMsg = "#01\r";
            // 发送命令
            byte[] sendMsg = Encoding.ASCII.GetBytes(strMsg);
            try {
                m_clientStream.Write(sendMsg, 0, sendMsg.Length);
                m_clientStream.Flush();
                m_log.TraceInfo("TcpClient send: " + strMsg.Replace("\r", "\\r"));
            } catch (Exception ex) {
                m_log.TraceError("TcpClient send error: " + ex.Message);
                m_client.Close();
                bConnected = TCPClientInit();
            }

            // 接收温度值
            int bytesRead;
            string strRecv = "";
            if (bConnected) {
                try {
                    while (!strRecv.Contains("\r")) {
                        while (m_clientStream.DataAvailable) {
                            bytesRead = m_clientStream.Read(m_recvBuf, 0, m_bufSize);
                            strRecv += Encoding.ASCII.GetString(m_recvBuf, 0, bytesRead);
                        }
                    }
                } catch (Exception ex) {
                    m_log.TraceError("TcpClient receiev error: " + ex.Message);
                }
                m_log.TraceInfo("TcpClient receiev: " + strRecv.Replace("\r", "\\r"));
            } else {
                m_log.TraceError("TcpClient can't connect server");
            }
            return SplitTemper(strRecv);
        }

        private string[] SplitTemper(string strMsg) {
            string[] result = new string[TotalChannel];
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
                        result[i] = null;
                    }
                }
            } else {
                for (int i = 0; i < TotalChannel; i++) {
                    result[i] = null;
                }
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

        public void GetData(DateTime start) {
            DataRow dr = m_dtTemper.NewRow();
            TimeSpan interval = DateTime.Now - start;
            if (interval.TotalSeconds < 0.001) {
                interval = TimeSpan.Zero;
            }
            dr["Time"] = interval.TotalSeconds.ToString("F1");
            string[] tempers = GetTemper();
            dr["Temper1"] = tempers[0] != OverRange && tempers[0] != UnderRange ? tempers[0] : null;
            dr["Temper2"] = tempers[1] != OverRange && tempers[1] != UnderRange ? tempers[1] : null;
            m_dtTemper.Rows.Add(dr);
        }

        public void ClearPoints() {
            m_dtTemper.Rows.Clear();
        }

        public void ExportResultFile() {
            string ExportPath = ".\\Export\\" + DateTime.Now.ToLocalTime().ToString("yyyy-MM") + "\\" + DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd");
            if (!Directory.Exists(ExportPath)) {
                Directory.CreateDirectory(ExportPath);
            }
            ExportPath += "\\" + StrVIN + "_" + DateTime.Now.ToLocalTime().ToString("yyyyMMdd-HHmmss") + ".xlsx";
            using (ExcelPackage package = new ExcelPackage()) {
                // 添加工作表/worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("空调温度检测数据");
                // 标题
                for (int i = 0; i < m_dtTemper.Columns.Count; i++) {
                    worksheet.Cells[1, i + 1].Value = m_dtTemper.Columns[i].ColumnName;
                    // 边框
                    worksheet.Cells[1, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                }
                // 格式化标题
                using (var range = worksheet.Cells[1, 1, 1, m_dtTemper.Columns.Count]) {
                    range.Style.Font.Bold = true;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }
                // 记录
                for (int iRow = 0; iRow < m_dtTemper.Rows.Count; iRow++) {
                    for (int iCol = 0; iCol < m_dtTemper.Columns.Count; iCol++) {
                        worksheet.Cells[iRow + 2, iCol + 1].Value = m_dtTemper.Rows[iRow][iCol].ToString();
                        // 边框
                        worksheet.Cells[iRow + 2, iCol + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    }
                }
                // 格式化记录
                if (m_dtTemper.Rows.Count > 0) {
                    using (var range = worksheet.Cells[2, 1, m_dtTemper.Rows.Count + 1, m_dtTemper.Columns.Count]) {
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
    }
}
