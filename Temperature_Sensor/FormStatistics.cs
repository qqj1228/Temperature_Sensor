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
        private const int QtyPerPage = 500; // 每页显示的记录数
        private readonly DataTable m_dtShow;

        public FormStatistics(Temper tester) {
            InitializeComponent();
            m_tester = tester;
            m_log = m_tester.GetLogger();
            m_cfg = m_tester.GetConfig();
            m_db = m_tester.GetModel();
            m_dtShow = new DataTable("Show");
            m_dtShow.Columns.Add("测温站ID");
            m_dtShow.Columns.Add("检测日期");
            m_dtShow.Columns.Add("检测时间");
            m_dtShow.Columns.Add("车辆VIN号");
            m_dtShow.Columns.Add("检测结果");
        }

        private void SetGridViewColumnsSortMode(DataGridView gridView, DataGridViewColumnSortMode sortMode) {
            for (int i = 0; i < gridView.Columns.Count; i++) {
                gridView.Columns[i].SortMode = sortMode;
            }
        }

        private void FormStatistics_Load(object sender, EventArgs e) {
            this.txtBoxVIN.Clear(); // 首先清VIN号筛选框，否则下面下拉框增加选项的时候会发生旧VIN号筛选
            this.cmbBoxTime.Items.Add("当天内");
            this.cmbBoxTime.Items.Add("本月内");
            this.cmbBoxTime.Items.Add("今年内");
            this.cmbBoxTime.SelectedIndex = 0;
            this.cmbBoxResult.Items.Add("不筛选");
            this.cmbBoxResult.Items.Add("合格");
            this.cmbBoxResult.Items.Add("不合格");
            this.cmbBoxResult.SelectedIndex = 0;
            this.txtBoxStation.Text = m_cfg.Setting.Data.TCPServerIP.Split('.')[3];
            this.dataGVResult.DataSource = m_dtShow;
            this.dataGVResult.Columns["车辆VIN号"].Width = 2 * this.dataGVResult.Columns["测温站ID"].Width;
            SetGridViewColumnsSortMode(this.dataGVResult, DataGridViewColumnSortMode.NotSortable);
            SetStatistics();
        }

        private string GetSN() {
            string strStation = "%";
            if (this.txtBoxStation.Text.Length > 0) {
                strStation = this.txtBoxStation.Text;
            }
            string strSN = "%";
            string[] strDates = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd").Split('-');
            switch (this.cmbBoxTime.SelectedIndex) {
            case 0:
                // 当天内
                strSN = strDates[0] + strDates[1] + strDates[2] + "-%";
                break;
            case 1:
                // 本月内
                strSN = strDates[0] + strDates[1] + "__-%";
                break;
            case 2:
                // 今年内
                strSN = strDates[0] + "____-%";
                break;
            }
            strSN = strStation + "-" + strSN;
            return strSN;
        }

        /// <summary>
        /// 返回一个整形数组：[总数, 合格数]
        /// </summary>
        /// <returns></returns>
        private int[] GetQty() {
            int[] iRets = new int[2];
            string SN = GetSN();
            string strSQL = "select count(*) from TemperResult where SN like '" + SN + "'";
            iRets[0] = Convert.ToInt32(m_db.QueryOne(strSQL));
            strSQL = "select count(*) from TemperResult where SN like '" + SN + "' and Result = '1'";
            iRets[1] = Convert.ToInt32(m_db.QueryOne(strSQL));
            return iRets;
        }

        private void SetStatistics() {
            int[] iQtys = new int[2];
            try {
                iQtys = GetQty();
            } catch (ApplicationException ex) {
                MessageBox.Show(ex.Message, "GetQty() ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } catch (Exception ex) {
                m_log.TraceError(ex.Message);
                MessageBox.Show(ex.Message, "GetQty() ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.lblQTY.Text = iQtys[0].ToString();
            int iToatlPage = iQtys[0] / QtyPerPage + (iQtys[0] % QtyPerPage > 0 ? 1 : 0);
            this.lblTotalPage.Text = "共" + iToatlPage.ToString() + "页";
            this.numUDPage.Maximum = iToatlPage;
            this.numUDPage.Minimum = iToatlPage == 0 ? 0 : 1;
            this.numUDPage.Value = this.numUDPage.Minimum;
            this.lblRate.Text = (iQtys[0] != 0 ? (iQtys[1] * 100.0 / iQtys[0]).ToString("F2") : "0") + "%";
        }

        private void SetShowData() {
            m_dtShow.Rows.Clear();
            string strSN = GetSN();
            string strVIN = "%" + this.txtBoxVIN.Text + "%";
            string strResult = "%";
            switch (this.cmbBoxResult.SelectedIndex) {
            case 1:
                // 合格
                strResult = "1";
                break;
            case 2:
                // 不合格
                strResult = "0";
                break;
            }
            string strTable = "TemperResult";
            string strSQL = string.Format("select * from {3} where SN like '{0}' and VIN like '{1}' and Result like '{2}'", strSN, strVIN, strResult, strTable);
            int pageNum = decimal.ToInt32(this.numUDPage.Value);
            if (pageNum > 0) {
                strSQL += " order by ID offset " + ((pageNum - 1) * QtyPerPage).ToString() + " rows fetch next " + QtyPerPage.ToString() + " rows only";
            }
            DataTable dtResult = new DataTable(strTable);
            try {
                m_db.Query(strSQL, dtResult);
            } catch (ApplicationException ex) {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } catch (Exception ex) {
                m_log.TraceError(ex.Message);
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            for (int iRow = 0; iRow < dtResult.Rows.Count; iRow++) {
                string[] strSNs = dtResult.Rows[iRow]["SN"].ToString().Split('-');
                DataRow dr = m_dtShow.NewRow();
                dr["测温站ID"] = strSNs[0];
                dr["检测日期"] = string.Format("{0}-{1}-{2}", strSNs[1].Substring(0, 4), strSNs[1].Substring(4, 2), strSNs[1].Substring(6, 2));
                dr["检测时间"] = string.Format("{0}:{1}:{2}", strSNs[2].Substring(0, 2), strSNs[2].Substring(2, 2), strSNs[2].Substring(4, 2));
                dr["车辆VIN号"] = dtResult.Rows[iRow]["VIN"];
                dr["检测结果"] = dtResult.Rows[iRow]["Result"].ToString() == "1" ? "合格" : "不合格";
                m_dtShow.Rows.Add(dr);
            }
        }

        private void SelectedIndexOrValueChanged(object sender, EventArgs e) {
            if (sender is ComboBox cmbBox && cmbBox.Name == "cmbBoxTime") {
                SetStatistics();
            }
            SetShowData();
        }

        private void TxtBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Enter) {
                if (sender is TextBox txtBox && txtBox.Name == "txtBoxStation") {
                    SetStatistics();
                }
                SetShowData();
            }
        }
    }
}
