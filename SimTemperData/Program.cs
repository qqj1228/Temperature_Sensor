using System;
using System.Collections.Generic;
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

        static void Main(string[] args) {
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
                    Random rd = new Random();
                    string strSend = ">+" + rd.Next(20, 40).ToString("d3") + "." + rd.Next(0, 100).ToString("d2");
                    strSend += rd.Next(-30, 0).ToString("d3") + "." + rd.Next(0, 100).ToString("d2") + "-0000\r";
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
}
