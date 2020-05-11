using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimTemperData {
    class Program {
        const int TCPPort = 8899;
        const int BufSize = 1024;
        const string CMD = "#01\r";
        static DataTable m_dtCSV = new DataTable("CSV");

        static void Main(string[] args) {
            CSVFile.OpenCSVFile(ref m_dtCSV, "./data.csv");
            TcpListener TCPListener = new TcpListener(IPAddress.Any, TCPPort);
            TCPListener.Start();
            IPEndPoint serverAddress = (IPEndPoint)TCPListener.LocalEndpoint;
            string time = DateTime.Now.ToString("HH:mm:ss.fff");
            Console.WriteLine(string.Format("{0} - TCP Server start listenning on {1}:{2}", time, serverAddress.Address, serverAddress.Port));
            while (true) {
                try {
                    TcpClient client = TCPListener.AcceptTcpClient();
                    IPEndPoint remoteAddress = (IPEndPoint)client.Client.RemoteEndPoint;
                    time = DateTime.Now.ToString("HH:mm:ss.fff");
                    Console.WriteLine(string.Format("{2} - TCP listener accept client: {0}:{1}", remoteAddress.Address, remoteAddress.Port, time));
                    Task.Factory.StartNew(HandleClient, client);
                } catch (Exception ex) {
                    time = DateTime.Now.ToString("HH:mm:ss.fff");
                    Console.WriteLine(time + " - TCP listener occur error: " + ex.Message);
                }
            }
        }

        static void HandleClient(object param) {
            TcpClient client = (TcpClient)param;
            NetworkStream clientStream = client.GetStream();
            byte[] recv = new byte[BufSize];
            string strRecv;
            int bytesRead;
            string time;
            int count = 0;
            while (true) {
                try {
                    bytesRead = clientStream.Read(recv, 0, BufSize);
                } catch (Exception ex) {
                    time = DateTime.Now.ToString("HH:mm:ss.fff");
                    Console.WriteLine(time + " - TCP client occur error: " + ex.Message);
                    clientStream.Close();
                    client.Close();
                    return;
                }
                if (bytesRead == 0) {
                    break;
                }
                strRecv = Encoding.ASCII.GetString(recv, 0, bytesRead);
                IPEndPoint remoteAddress = (IPEndPoint)client.Client.RemoteEndPoint;
                time = DateTime.Now.ToString("HH:mm:ss.fff");
                Console.WriteLine(string.Format("{3} - Received message[{0}], from {1}:{2}", strRecv.Replace("\r", "\\r"), remoteAddress.Address, remoteAddress.Port, time));
                if (CMD == strRecv) {
                    string strSend;
                    if (m_dtCSV.Rows.Count > 0) {
                        strSend = ">" + m_dtCSV.Rows[count]["Temper1"].ToString() + m_dtCSV.Rows[count]["Temper2"].ToString();
                        strSend += "-0000\r";
                        count = ++count % m_dtCSV.Rows.Count;
                    } else {
                        Random rd = new Random();
                        strSend = ">+" + rd.Next(20, 40).ToString("d3") + "." + rd.Next(0, 100).ToString("d2");
                        strSend += rd.Next(-30, 0).ToString("d3") + "." + rd.Next(0, 100).ToString("d2") + "-0000\r";
                    }
                    byte[] sendMessage = Encoding.ASCII.GetBytes(strSend);
                    clientStream.Write(sendMessage, 0, sendMessage.Length);
                    clientStream.Flush();
                    time = DateTime.Now.ToString("HH:mm:ss.fff");
                    Console.WriteLine(string.Format("{3} - Sent message[{0}], to {1}:{2}", strSend.Replace("\r", "\\r"), remoteAddress.Address, remoteAddress.Port, time));
                }
            }
            clientStream.Close();
            client.Close();
        }

    }

    public static class CSVFile {
        public static bool OpenCSVFile(ref DataTable dtIn, string filepath) {
            try {
                bool blnFlag = true;
                DataColumn mydc;
                DataRow mydr;
                string strline;
                string[] aryline;
                StreamReader mysr = new StreamReader(filepath, Encoding.UTF8);

                while ((strline = mysr.ReadLine()) != null) {
                    aryline = strline.Split(new char[] { ',' });
                    //第一行是列的名字，给datatable加上列名,
                    if (blnFlag) {
                        blnFlag = false;
                        foreach (var item in aryline) {
                            if (!dtIn.Columns.Contains(item)) {
                                mydc = new DataColumn(item);
                                dtIn.Columns.Add(mydc);
                            }
                        }
                        continue;
                    }
                    //填充数据并加入到datatable中
                    mydr = dtIn.Rows.Add(aryline);
                }
                mysr.Close();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        /// <summary>
        /// 将datatable中的数据保存到csv中
        /// </summary>
        /// <param name="dtOut">数据来源</param>
        /// <param name="savaPath">保存的路径</param>
        /// <param name="strName">保存文件的名称</param>
        public static void ExportToCSV(DataTable dtOut, string savaPath) {
            if (File.Exists(savaPath)) {
                File.Delete(savaPath);
            }
            //先打印表头
            StringBuilder strColu = new StringBuilder();
            StringBuilder strValue = new StringBuilder();
            StreamWriter sw = new StreamWriter(new FileStream(savaPath, FileMode.CreateNew), Encoding.GetEncoding("UTF8"));
            try {
                for (int i = 0; i <= dtOut.Columns.Count - 1; i++) {
                    strColu.Append(dtOut.Columns[i].ColumnName);
                    strColu.Append(",");
                }
                strColu.Remove(strColu.Length - 1, 1);//移出掉最后一个,字符
                sw.WriteLine(strColu);
                foreach (DataRow dr in dtOut.Rows) {
                    strValue.Remove(0, strValue.Length);//移出
                    for (int i = 0; i <= dtOut.Columns.Count - 1; i++) {
                        strValue.Append(dr[i].ToString());
                        strValue.Append(",");
                    }
                    strValue.Remove(strValue.Length - 1, 1);//移出掉最后一个,字符
                    sw.WriteLine(strValue);
                }
                sw.Close();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                sw.Close();
            }
        }

    }
}
