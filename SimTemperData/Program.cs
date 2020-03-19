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
            Console.WriteLine(string.Format("TCP Server start listenning on {0}:{1}", serverAddress.Address, serverAddress.Port));
            while (true) {
                try {
                    TcpClient client = TCPListener.AcceptTcpClient();
                    Task.Factory.StartNew(HandleClient, client);
                } catch (Exception ex) {
                    Console.WriteLine("TCP listener occur error: " + ex.Message);
                }
            }
        }

        static void HandleClient(object param) {
            TcpClient client = (TcpClient)param;
            NetworkStream clientStream = client.GetStream();
            byte[] recv = new byte[BufSize];
            string strRecv;
            int bytesRead;
            while (true) {
                try {
                    bytesRead = clientStream.Read(recv, 0, BufSize);
                } catch (Exception ex) {
                    Console.WriteLine("TCP client occur error: " + ex.Message);
                    clientStream.Close();
                    client.Close();
                    return;
                }
                if (bytesRead == 0) {
                    break;
                }
                strRecv = Encoding.ASCII.GetString(recv, 0, bytesRead);
                IPEndPoint remoteAddress = (IPEndPoint)client.Client.RemoteEndPoint;
                Console.WriteLine(string.Format("Received message[{0}], from {1}:{2}", strRecv.Replace("\r", "\\r"), remoteAddress.Address, remoteAddress.Port));
                if (CMD == strRecv) {
                    Random rd = new Random();
                    string strSend = ">+" + rd.Next(20, 40).ToString("d3") + "." + rd.Next(0, 100).ToString("d2");
                    strSend += rd.Next(-30, 0).ToString("d3") + "." + rd.Next(0, 100).ToString("d2") + "-0000\r";
                    byte[] sendMessage = Encoding.ASCII.GetBytes(strSend);
                    clientStream.Write(sendMessage, 0, sendMessage.Length);
                    clientStream.Flush();
                    Console.WriteLine(string.Format("Sent message[{0}], to {1}:{2}", strSend.Replace("\r", "\\r"), remoteAddress.Address, remoteAddress.Port));
                }
            }
            clientStream.Close();
            client.Close();
        }
    }
}
