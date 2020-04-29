namespace Temperature_Sensor {
    partial class FormStatistics {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStatistics));
            this.grpBoxTime = new System.Windows.Forms.GroupBox();
            this.cmbBoxTime = new System.Windows.Forms.ComboBox();
            this.grpBoxStation = new System.Windows.Forms.GroupBox();
            this.txtBoxStation = new System.Windows.Forms.TextBox();
            this.grpBoxVIN = new System.Windows.Forms.GroupBox();
            this.txtBoxVIN = new System.Windows.Forms.TextBox();
            this.grpBoxResult = new System.Windows.Forms.GroupBox();
            this.cmbBoxResult = new System.Windows.Forms.ComboBox();
            this.grpBoxQTY = new System.Windows.Forms.GroupBox();
            this.lblQTY = new System.Windows.Forms.Label();
            this.grpBoxRate = new System.Windows.Forms.GroupBox();
            this.lblRate = new System.Windows.Forms.Label();
            this.dataGVResult = new System.Windows.Forms.DataGridView();
            this.tblLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblLayoutTop = new System.Windows.Forms.TableLayoutPanel();
            this.grpBoxPage = new System.Windows.Forms.GroupBox();
            this.lblTotalPage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numUDPage = new System.Windows.Forms.NumericUpDown();
            this.grpBoxTime.SuspendLayout();
            this.grpBoxStation.SuspendLayout();
            this.grpBoxVIN.SuspendLayout();
            this.grpBoxResult.SuspendLayout();
            this.grpBoxQTY.SuspendLayout();
            this.grpBoxRate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVResult)).BeginInit();
            this.tblLayoutMain.SuspendLayout();
            this.tblLayoutTop.SuspendLayout();
            this.grpBoxPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDPage)).BeginInit();
            this.SuspendLayout();
            // 
            // grpBoxTime
            // 
            this.grpBoxTime.Controls.Add(this.cmbBoxTime);
            this.grpBoxTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoxTime.Location = new System.Drawing.Point(3, 3);
            this.grpBoxTime.Name = "grpBoxTime";
            this.grpBoxTime.Size = new System.Drawing.Size(83, 38);
            this.grpBoxTime.TabIndex = 0;
            this.grpBoxTime.TabStop = false;
            this.grpBoxTime.Text = "统计时间";
            // 
            // cmbBoxTime
            // 
            this.cmbBoxTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbBoxTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxTime.FormattingEnabled = true;
            this.cmbBoxTime.Location = new System.Drawing.Point(3, 17);
            this.cmbBoxTime.Name = "cmbBoxTime";
            this.cmbBoxTime.Size = new System.Drawing.Size(77, 20);
            this.cmbBoxTime.TabIndex = 0;
            this.cmbBoxTime.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexOrValueChanged);
            // 
            // grpBoxStation
            // 
            this.grpBoxStation.Controls.Add(this.txtBoxStation);
            this.grpBoxStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoxStation.Location = new System.Drawing.Point(92, 3);
            this.grpBoxStation.Name = "grpBoxStation";
            this.grpBoxStation.Size = new System.Drawing.Size(83, 38);
            this.grpBoxStation.TabIndex = 1;
            this.grpBoxStation.TabStop = false;
            this.grpBoxStation.Text = "测温站ID";
            // 
            // txtBoxStation
            // 
            this.txtBoxStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxStation.Location = new System.Drawing.Point(3, 17);
            this.txtBoxStation.Name = "txtBoxStation";
            this.txtBoxStation.Size = new System.Drawing.Size(77, 21);
            this.txtBoxStation.TabIndex = 1;
            this.txtBoxStation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBox_KeyPress);
            // 
            // grpBoxVIN
            // 
            this.grpBoxVIN.Controls.Add(this.txtBoxVIN);
            this.grpBoxVIN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoxVIN.Location = new System.Drawing.Point(181, 3);
            this.grpBoxVIN.Name = "grpBoxVIN";
            this.grpBoxVIN.Size = new System.Drawing.Size(143, 38);
            this.grpBoxVIN.TabIndex = 2;
            this.grpBoxVIN.TabStop = false;
            this.grpBoxVIN.Text = "筛选车辆VIN号";
            // 
            // txtBoxVIN
            // 
            this.txtBoxVIN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxVIN.Location = new System.Drawing.Point(3, 17);
            this.txtBoxVIN.Name = "txtBoxVIN";
            this.txtBoxVIN.Size = new System.Drawing.Size(137, 21);
            this.txtBoxVIN.TabIndex = 0;
            this.txtBoxVIN.Text = "12345678901234567";
            this.txtBoxVIN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBox_KeyPress);
            // 
            // grpBoxResult
            // 
            this.grpBoxResult.Controls.Add(this.cmbBoxResult);
            this.grpBoxResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoxResult.Location = new System.Drawing.Point(330, 3);
            this.grpBoxResult.Name = "grpBoxResult";
            this.grpBoxResult.Size = new System.Drawing.Size(83, 38);
            this.grpBoxResult.TabIndex = 3;
            this.grpBoxResult.TabStop = false;
            this.grpBoxResult.Text = "筛选结果";
            // 
            // cmbBoxResult
            // 
            this.cmbBoxResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbBoxResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxResult.FormattingEnabled = true;
            this.cmbBoxResult.Location = new System.Drawing.Point(3, 17);
            this.cmbBoxResult.Name = "cmbBoxResult";
            this.cmbBoxResult.Size = new System.Drawing.Size(77, 20);
            this.cmbBoxResult.TabIndex = 1;
            this.cmbBoxResult.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexOrValueChanged);
            // 
            // grpBoxQTY
            // 
            this.grpBoxQTY.Controls.Add(this.lblQTY);
            this.grpBoxQTY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoxQTY.Location = new System.Drawing.Point(419, 3);
            this.grpBoxQTY.Name = "grpBoxQTY";
            this.grpBoxQTY.Size = new System.Drawing.Size(101, 38);
            this.grpBoxQTY.TabIndex = 4;
            this.grpBoxQTY.TabStop = false;
            this.grpBoxQTY.Text = "已检车次";
            // 
            // lblQTY
            // 
            this.lblQTY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblQTY.Location = new System.Drawing.Point(3, 17);
            this.lblQTY.Name = "lblQTY";
            this.lblQTY.Size = new System.Drawing.Size(95, 18);
            this.lblQTY.TabIndex = 0;
            this.lblQTY.Text = "1234567890";
            this.lblQTY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpBoxRate
            // 
            this.grpBoxRate.Controls.Add(this.lblRate);
            this.grpBoxRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoxRate.Location = new System.Drawing.Point(526, 3);
            this.grpBoxRate.Name = "grpBoxRate";
            this.grpBoxRate.Size = new System.Drawing.Size(65, 38);
            this.grpBoxRate.TabIndex = 5;
            this.grpBoxRate.TabStop = false;
            this.grpBoxRate.Text = "合格率";
            // 
            // lblRate
            // 
            this.lblRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRate.Location = new System.Drawing.Point(3, 17);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(59, 18);
            this.lblRate.TabIndex = 0;
            this.lblRate.Text = "100%";
            this.lblRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGVResult
            // 
            this.dataGVResult.AllowUserToAddRows = false;
            this.dataGVResult.AllowUserToDeleteRows = false;
            this.dataGVResult.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGVResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGVResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGVResult.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGVResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGVResult.Location = new System.Drawing.Point(3, 53);
            this.dataGVResult.Name = "dataGVResult";
            this.dataGVResult.ReadOnly = true;
            this.dataGVResult.RowHeadersVisible = false;
            this.dataGVResult.RowTemplate.Height = 23;
            this.dataGVResult.Size = new System.Drawing.Size(778, 505);
            this.dataGVResult.TabIndex = 6;
            // 
            // tblLayoutMain
            // 
            this.tblLayoutMain.ColumnCount = 1;
            this.tblLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayoutMain.Controls.Add(this.dataGVResult, 0, 1);
            this.tblLayoutMain.Controls.Add(this.tblLayoutTop, 0, 0);
            this.tblLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutMain.Location = new System.Drawing.Point(0, 0);
            this.tblLayoutMain.Name = "tblLayoutMain";
            this.tblLayoutMain.RowCount = 2;
            this.tblLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayoutMain.Size = new System.Drawing.Size(784, 561);
            this.tblLayoutMain.TabIndex = 7;
            // 
            // tblLayoutTop
            // 
            this.tblLayoutTop.ColumnCount = 7;
            this.tblLayoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblLayoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblLayoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLayoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblLayoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblLayoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblLayoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tblLayoutTop.Controls.Add(this.grpBoxPage, 6, 0);
            this.tblLayoutTop.Controls.Add(this.grpBoxRate, 5, 0);
            this.tblLayoutTop.Controls.Add(this.grpBoxQTY, 4, 0);
            this.tblLayoutTop.Controls.Add(this.grpBoxTime, 0, 0);
            this.tblLayoutTop.Controls.Add(this.grpBoxStation, 1, 0);
            this.tblLayoutTop.Controls.Add(this.grpBoxVIN, 2, 0);
            this.tblLayoutTop.Controls.Add(this.grpBoxResult, 3, 0);
            this.tblLayoutTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutTop.Location = new System.Drawing.Point(3, 3);
            this.tblLayoutTop.Name = "tblLayoutTop";
            this.tblLayoutTop.RowCount = 1;
            this.tblLayoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayoutTop.Size = new System.Drawing.Size(778, 44);
            this.tblLayoutTop.TabIndex = 7;
            // 
            // grpBoxPage
            // 
            this.grpBoxPage.Controls.Add(this.lblTotalPage);
            this.grpBoxPage.Controls.Add(this.label1);
            this.grpBoxPage.Controls.Add(this.numUDPage);
            this.grpBoxPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoxPage.Location = new System.Drawing.Point(597, 3);
            this.grpBoxPage.Name = "grpBoxPage";
            this.grpBoxPage.Size = new System.Drawing.Size(178, 38);
            this.grpBoxPage.TabIndex = 6;
            this.grpBoxPage.TabStop = false;
            this.grpBoxPage.Text = "分页";
            // 
            // lblTotalPage
            // 
            this.lblTotalPage.AutoSize = true;
            this.lblTotalPage.Location = new System.Drawing.Point(121, 17);
            this.lblTotalPage.Name = "lblTotalPage";
            this.lblTotalPage.Size = new System.Drawing.Size(47, 12);
            this.lblTotalPage.TabIndex = 2;
            this.lblTotalPage.Text = "共999页";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "当前页：";
            // 
            // numUDPage
            // 
            this.numUDPage.Location = new System.Drawing.Point(65, 12);
            this.numUDPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUDPage.Name = "numUDPage";
            this.numUDPage.Size = new System.Drawing.Size(50, 21);
            this.numUDPage.TabIndex = 0;
            this.numUDPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUDPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUDPage.ValueChanged += new System.EventHandler(this.SelectedIndexOrValueChanged);
            // 
            // FormStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tblLayoutMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormStatistics";
            this.Text = "统计信息";
            this.Load += new System.EventHandler(this.FormStatistics_Load);
            this.grpBoxTime.ResumeLayout(false);
            this.grpBoxStation.ResumeLayout(false);
            this.grpBoxStation.PerformLayout();
            this.grpBoxVIN.ResumeLayout(false);
            this.grpBoxVIN.PerformLayout();
            this.grpBoxResult.ResumeLayout(false);
            this.grpBoxQTY.ResumeLayout(false);
            this.grpBoxRate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGVResult)).EndInit();
            this.tblLayoutMain.ResumeLayout(false);
            this.tblLayoutTop.ResumeLayout(false);
            this.grpBoxPage.ResumeLayout(false);
            this.grpBoxPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDPage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxTime;
        private System.Windows.Forms.ComboBox cmbBoxTime;
        private System.Windows.Forms.GroupBox grpBoxStation;
        private System.Windows.Forms.TextBox txtBoxStation;
        private System.Windows.Forms.GroupBox grpBoxVIN;
        private System.Windows.Forms.TextBox txtBoxVIN;
        private System.Windows.Forms.GroupBox grpBoxResult;
        private System.Windows.Forms.ComboBox cmbBoxResult;
        private System.Windows.Forms.GroupBox grpBoxQTY;
        private System.Windows.Forms.GroupBox grpBoxRate;
        private System.Windows.Forms.DataGridView dataGVResult;
        private System.Windows.Forms.TableLayoutPanel tblLayoutMain;
        private System.Windows.Forms.TableLayoutPanel tblLayoutTop;
        private System.Windows.Forms.GroupBox grpBoxPage;
        private System.Windows.Forms.Label lblTotalPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numUDPage;
        private System.Windows.Forms.Label lblQTY;
        private System.Windows.Forms.Label lblRate;
    }
}