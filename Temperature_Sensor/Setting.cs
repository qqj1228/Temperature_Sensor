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
        public int TotalTime { get; set; } // 温度采样总时间，单位s
        public string DBIP { get; set; } // 数据库IP地址
        public string DBPort { get; set; } // 数据库端口号
        public string DBUser { get; set; } // 数据库用户名
        public string DBPwd { get; set; } // 数据库密码
        public double SetupValue { get; set; } // 温度设定值，用于计算设定温度，小于1为比率，大于1为绝对值
        public bool Cooling { get; set; } // 空调工作模式，true - 制冷，false - 制热
        public int TestFailedQty { get; set; } // 连续n辆车检测失败将提示报警，n为设定值
        public bool UsingRPM { get; set; } // 使用引擎转速来判断空调开启状态
        public int IdleRPMMin { get; set; } // 最低怠速转速，大于等于该转速的话即认为空调已开
        public int Temper { get; set; } // 用哪个温度点作为评判标准，0：全部，1：温度点1，2：温度点2
        public int Rule { get; set; } // 是否合格的评判标准，1：平均值，2：连续点，3：斜率（未实现），4：曲线拟合（未实现）
        public double SuccessiveValue { get; set; } // 连续点设定值，小于1为比率，大于1为绝对值

        public Setting() {
            TCPServerIP = "10.10.100.254";
            TCPServerPort = 8899;
            ScannerPort = "COM5";
            ScannerBaud = 9600;
            Interval = 500;
            TotalTime = 10;
            DBIP = "127.0.0.1";
            DBPort = "1433";
            DBUser = "sa";
            DBPwd = "sh49";
            SetupValue = 0.25;
            Cooling = true;
            TestFailedQty = 3;
            UsingRPM = false;
            IdleRPMMin = 600;
            Temper = 0;
            Rule = 1;
            SuccessiveValue = 0.5;
        }
    }
}
