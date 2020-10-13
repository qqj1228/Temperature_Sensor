using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Temperature_Sensor {
    public partial class FormPwd : Form {
        private readonly BaseLib.Model m_db;

        public FormPwd(BaseLib.Model db) {
            InitializeComponent();
            m_db = db;
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(Encoding.Default.GetBytes(this.txtBoxPassWord.Text.Trim()));
            string strValue = BitConverter.ToString(output).Replace("-", "");
            if (strValue == m_db.GetPassWord()) {
                this.DialogResult = DialogResult.Yes;
            } else {
                this.DialogResult = DialogResult.No;
            }
            md5.Dispose();
            this.Close();
        }
    }
}
