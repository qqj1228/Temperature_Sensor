using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Temperature_Sensor {
    public partial class FormSetup : Form {
        private readonly Config m_cfg;
        private readonly BaseLib.Model m_db;
        private const double m_dAmbient = 20;
        private double m_SetupValue;
        private bool m_Cooling;
        private int m_Temper;
        private int m_Rule;
        private double m_SuccessiveValue;

        public FormSetup(Config cfg, BaseLib.Model db) {
            InitializeComponent();
            m_cfg = cfg;
            m_db = db;
        }

        private void FormSetup_Load(object sender, EventArgs e) {
            txtBoxSetupValue.Text = m_cfg.Setting.Data.SetupValue.ToString();
            cmbBoxCooling.Items.Add("制冷");
            cmbBoxCooling.Items.Add("制热");
            if (m_cfg.Setting.Data.Cooling) {
                cmbBoxCooling.SelectedIndex = 0;
            } else {
                cmbBoxCooling.SelectedIndex = 1;
            }
            cmbBoxTemper.Items.Add("全部");
            cmbBoxTemper.Items.Add("温度点1");
            cmbBoxTemper.Items.Add("温度点2");
            cmbBoxTemper.SelectedIndex = m_cfg.Setting.Data.Temper;
            cmbBoxRule.Items.Add("温度曲线拟合");
            cmbBoxRule.Items.Add("温度采样平均值");
            cmbBoxRule.Items.Add("连续温度采样");
            cmbBoxRule.SelectedIndex = m_cfg.Setting.Data.Rule;
            txtBoxSuccessiveValue.Text = m_cfg.Setting.Data.SuccessiveValue.ToString();
            txtBoxEnvTemper.Text = m_dAmbient.ToString();
            txtBoxSetupTemper.Text = GetSetupTemper(m_dAmbient, m_cfg.Setting.Data.SetupValue).ToString("F2");
        }

        private double GetSetupTemper(double dAmbient, double dSetupValue) {
            double dSetup;
            if (m_cfg.Setting.Data.SetupValue > 1) {
                // 绝对值
                if (m_cfg.Setting.Data.Cooling) {
                    dSetup = dAmbient - dSetupValue;
                } else {
                    dSetup = dAmbient + dSetupValue;
                }
            } else {
                // 比率
                if (m_cfg.Setting.Data.Cooling) {
                    dSetup = dAmbient * (1 - dSetupValue);
                } else {
                    dSetup = dAmbient * (1 + dSetupValue);
                }
            }
            return dSetup;
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar != (char)Keys.Back && !char.IsDigit(e.KeyChar)) {
                if (e.KeyChar == '.') {
                    TextBox textBox = (TextBox)sender;
                    if (textBox.Text.Contains('.')) {
                        e.Handled = true;
                    }
                } else {
                    e.Handled = true;
                }
            }
        }

        private void TxtBoxSetupValue_TextChanged(object sender, EventArgs e) {
            m_SetupValue = Convert.ToDouble(txtBoxSetupValue.Text);
            txtBoxSetupTemper.Text = GetSetupTemper(m_dAmbient, m_SetupValue).ToString("F2");
        }

        private void CmbBoxCooling_SelectedIndexChanged(object sender, EventArgs e) {
            if (cmbBoxCooling.SelectedIndex == 0) {
                m_Cooling = true;
            } else {
                m_Cooling = false;
            }
        }

        private void CmbBoxTemper_SelectedIndexChanged(object sender, EventArgs e) {
            m_Temper = cmbBoxTemper.SelectedIndex;
        }

        private void CmbBoxRule_SelectedIndexChanged(object sender, EventArgs e) {
            m_Rule = cmbBoxRule.SelectedIndex;
        }

        private void TxtBoxSuccessiveValue_TextChanged(object sender, EventArgs e) {
            m_SuccessiveValue = Convert.ToDouble(txtBoxSuccessiveValue.Text);
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            m_cfg.Setting.Data.SetupValue = m_SetupValue;
            m_cfg.Setting.Data.Cooling = m_Cooling;
            m_cfg.Setting.Data.Temper = m_Temper;
            m_cfg.Setting.Data.Rule = m_Rule;
            m_cfg.Setting.Data.SuccessiveValue = m_SuccessiveValue;
            m_cfg.SaveConfig(m_cfg.Setting);
            Close();
        }

        private void BtnPwd_Click(object sender, EventArgs e) {
            if (txtBoxOriginalPwd.TextLength > 0 && txtBoxNewPwd1.TextLength > 0 && txtBoxNewPwd2.TextLength > 0) {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] output = md5.ComputeHash(Encoding.Default.GetBytes(txtBoxOriginalPwd.Text.Trim()));
                string strValue = BitConverter.ToString(output).Replace("-", "");
                if (strValue == m_db.GetPassWord()) {
                    if (txtBoxNewPwd1.Text != txtBoxNewPwd2.Text) {
                        MessageBox.Show("两次输入的新密码不一致！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } else {
                        output = md5.ComputeHash(Encoding.Default.GetBytes(txtBoxNewPwd1.Text.Trim()));
                        strValue = BitConverter.ToString(output).Replace("-", "");
                        if (m_db.SetPassWord(strValue) == 1) {
                            MessageBox.Show("修改管理员密码成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        } else {
                            MessageBox.Show("修改管理员密码失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                } else {
                    MessageBox.Show("原密码不正确！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                md5.Dispose();
            }
        }
    }
}
