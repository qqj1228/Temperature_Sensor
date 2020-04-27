using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Temperature_Sensor {
    public partial class FormStatistics : Form {
        private readonly Temper m_tester;
        private readonly BaseLib.Logger m_log;
        private readonly Config m_cfg;
        private readonly BaseLib.Model m_db;
        private const int QtyPerPage = 10; // 每页显示的记录数

        public FormStatistics(Temper tester) {
            InitializeComponent();
            m_tester = tester;
            m_log = m_tester.GetLogger();
            m_cfg = m_tester.GetConfig();
            m_db = m_tester.GetModel();
        }

        private void FormStatistics_Load(object sender, EventArgs e) {
            this.cmbBoxTime.Items.Add("当天内");
            this.cmbBoxTime.Items.Add("本月内");
            this.cmbBoxTime.Items.Add("今年内");
            this.cmbBoxTime.SelectedIndex = 0;
            this.cmbBoxResult.Items.Add("不筛选");
            this.cmbBoxResult.Items.Add("合格");
            this.cmbBoxResult.Items.Add("不合格");
            this.cmbBoxResult.SelectedIndex = 0;
            this.txtBoxStation.Text = m_cfg.Setting.Data.TCPServerIP.Split('.')[3];
            this.txtBoxVIN.Clear();
            int iToatlQty = 0;
            try {
                iToatlQty = m_db.GetRecordsCount("TemperResult");
            } catch (Exception ex) {
                m_log.TraceError(ex.Message);
                MessageBox.Show(ex.Message, "GetRecordsCount() ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            int iToatlPage = iToatlQty / QtyPerPage + (iToatlQty % QtyPerPage > 0 ? 1 : 0);
            this.lblTotalPage.Text = "共" + iToatlPage.ToString() + "页";
            this.numUDPage.Maximum = iToatlPage;
            if (iToatlPage == 0) {
                this.numUDPage.Minimum = 0;
            }
        }
    }
}
