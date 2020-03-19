using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temperature_Sensor {
    [Serializable]
    public class Setting {
        public string TCPServerIP { get; set; }
        public int TCPServerPort { get; set; }
        public string ScannerPort { get; set; } // 扫码枪串口号，空字符串表示不用串口扫码枪
        public int ScannerBaud { get; set; } // 扫码枪波特率
        public int Interval { get; set; } // 温度采样时间间隔，单位ms
        public int TotalTime { get; set; } // 温度采样总时间，单位ms

        public Setting() {
            TCPServerIP = "10.10.100.254";
            TCPServerPort = 8899;
            ScannerPort = "COM5";
            ScannerBaud = 9600;
            Interval = 500;
            TotalTime = 10000;
        }
    }
}
