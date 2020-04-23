using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temperature_Sensor {
    [Serializable]
    public class Setting {
        public string TCPServerIP { get; set; } // Wifi串口服务器的IP地址
        public int TCPServerPort { get; set; } // Wifi串口服务器的端口号
        public string ScannerPort { get; set; } // 扫码枪串口号，空字符串表示不用串口扫码枪
        public int ScannerBaud { get; set; } // 扫码枪波特率
        public int Interval { get; set; } // 温度采样时间间隔，单位ms
        public int TotalTime { get; set; } // 温度采样总时间，单位ms
        public string DBIP { get; set; } // 数据库IP地址
        public string DBPort { get; set; } // 数据库端口号
        public string DBUser { get; set; } // 数据库用户名
        public string DBPwd { get; set; } // 数据库密码
        public double STDTemper { get; set; } // 设定温度值，单位℃，低于此温度判为不合格
        public int TestFailedQty { get; set; } // 连续n辆车检测失败将提示报警，n为设定值

        public Setting() {
            TCPServerIP = "10.10.100.254";
            TCPServerPort = 8899;
            ScannerPort = "COM5";
            ScannerBaud = 9600;
            Interval = 500;
            TotalTime = 10000;
            DBIP = "127.0.0.1";
            DBPort = "1433";
            DBUser = "sa";
            DBPwd = "sh49";
            STDTemper = 20;
            TestFailedQty = 3;
        }
    }
}
