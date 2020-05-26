using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SH_OBD;
using SH_OBD_DLL;

namespace Temperature_Sensor {
    public class OBDDll {
        private readonly BaseLib.Logger m_log;
        private readonly SH_OBD_Main m_obd;

        public OBDDll(BaseLib.Logger logger) {
            m_log = logger;
            m_obd = new SH_OBD_Main();
        }

        public bool InitOBDDll() {
            if (!m_obd.ConnectOBD()) {
                m_log.TraceError("SH_OBD_Main.ConnectOBD() failed");
                return false;
            }
            if (!m_obd.SetSupportStatus()) {
                m_log.TraceError("SH_OBD_Main.SetSupportStatus() failed");
                return false;
            }
            return true;
        }

        public string GetPID0C() {
            string strRet = "";
            OBDParameter param = new OBDParameter();
            if (m_obd.OBDif.STDType == StandardType.ISO_27145) {
                param.OBDRequest = "22F40C";
                param.Service = 0x22;
                param.Parameter = 0xF40C;
                param.SubParameter = 0;
                param.ValueTypes = (int)OBDParameter.EnumValueTypes.Double;
            } else if (m_obd.OBDif.STDType == StandardType.ISO_15031) {
                param.OBDRequest = "010C";
                param.Service = 1;
                param.Parameter = 0x0C;
                param.SubParameter = 0;
                param.ValueTypes = (int)OBDParameter.EnumValueTypes.Double;
            }
            List<OBDParameterValue> valueList = m_obd.OBDif.GetValueList(param);
            foreach (OBDParameterValue value in valueList) {
                if (value.ErrorDetected) {
                    continue;
                }
                if (m_obd.Mode01Support.ContainsKey(value.ECUResponseID) && m_obd.Mode01Support[value.ECUResponseID][(param.Parameter & 0x00FF) - 1]) {
                    if ((param.ValueTypes & (int)OBDParameter.EnumValueTypes.Bool) != 0) {
                        if (value.BoolValue) {
                            strRet = "ON";
                        } else {
                            strRet = "OFF";
                        }
                    } else if ((param.ValueTypes & (int)OBDParameter.EnumValueTypes.Double) != 0) {
                        strRet = value.DoubleValue.ToString();
                    } else if ((param.ValueTypes & (int)OBDParameter.EnumValueTypes.String) != 0) {
                        strRet = value.StringValue;
                    } else if ((param.ValueTypes & (int)OBDParameter.EnumValueTypes.ShortString) != 0) {
                        strRet = value.ShortStringValue;
                    }
                    break;
                }
            }
            return strRet;
        }

    }
}
