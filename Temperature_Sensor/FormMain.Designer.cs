namespace Temperature_Sensor {
    partial class Main {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tblLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblLayoutTop = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxVIN = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblInfo = new System.Windows.Forms.Label();
            this.tblLayoutLogo = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblLogo = new System.Windows.Forms.Label();
            this.tblLayoutMain.SuspendLayout();
            this.tblLayoutTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tblLayoutLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tblLayoutMain
            // 
            this.tblLayoutMain.ColumnCount = 1;
            this.tblLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayoutMain.Controls.Add(this.tblLayoutTop, 0, 1);
            this.tblLayoutMain.Controls.Add(this.chart1, 0, 2);
            this.tblLayoutMain.Controls.Add(this.lblInfo, 0, 3);
            this.tblLayoutMain.Controls.Add(this.tblLayoutLogo, 0, 0);
            this.tblLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutMain.Location = new System.Drawing.Point(0, 0);
            this.tblLayoutMain.Name = "tblLayoutMain";
            this.tblLayoutMain.RowCount = 4;
            this.tblLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53F));
            this.tblLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblLayoutMain.Size = new System.Drawing.Size(784, 561);
            this.tblLayoutMain.TabIndex = 0;
            // 
            // tblLayoutTop
            // 
            this.tblLayoutTop.ColumnCount = 2;
            this.tblLayoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tblLayoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77F));
            this.tblLayoutTop.Controls.Add(this.label1, 0, 0);
            this.tblLayoutTop.Controls.Add(this.txtBoxVIN, 1, 0);
            this.tblLayoutTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutTop.Location = new System.Drawing.Point(3, 87);
            this.tblLayoutTop.Name = "tblLayoutTop";
            this.tblLayoutTop.RowCount = 1;
            this.tblLayoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayoutTop.Size = new System.Drawing.Size(778, 89);
            this.tblLayoutTop.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("宋体", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 89);
            this.label1.TabIndex = 0;
            this.label1.Text = "VIN:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBoxVIN
            // 
            this.txtBoxVIN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBoxVIN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxVIN.Font = new System.Drawing.Font("宋体", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBoxVIN.Location = new System.Drawing.Point(181, 3);
            this.txtBoxVIN.Name = "txtBoxVIN";
            this.txtBoxVIN.Size = new System.Drawing.Size(594, 84);
            this.txtBoxVIN.TabIndex = 1;
            this.txtBoxVIN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBoxVIN_KeyPress);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Alignment = System.Drawing.StringAlignment.Far;
            legend1.BackColor = System.Drawing.SystemColors.Control;
            legend1.DockedToChartArea = "ChartArea1";
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(3, 182);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(778, 291);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInfo.Font = new System.Drawing.Font("宋体", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInfo.Location = new System.Drawing.Point(3, 476);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(778, 85);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "等待检测空调温度";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblLayoutLogo
            // 
            this.tblLayoutLogo.ColumnCount = 2;
            this.tblLayoutLogo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tblLayoutLogo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77F));
            this.tblLayoutLogo.Controls.Add(this.pictureBox1, 0, 0);
            this.tblLayoutLogo.Controls.Add(this.lblLogo, 1, 0);
            this.tblLayoutLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutLogo.Location = new System.Drawing.Point(3, 3);
            this.tblLayoutLogo.Name = "tblLayoutLogo";
            this.tblLayoutLogo.RowCount = 1;
            this.tblLayoutLogo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayoutLogo.Size = new System.Drawing.Size(778, 78);
            this.tblLayoutLogo.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Temperature_Sensor.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(172, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogo.Font = new System.Drawing.Font("宋体", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLogo.Location = new System.Drawing.Point(181, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(594, 78);
            this.lblLogo.TabIndex = 1;
            this.lblLogo.Text = "车辆空调温度检测系统";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tblLayoutMain);
            this.Name = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.tblLayoutMain.ResumeLayout(false);
            this.tblLayoutMain.PerformLayout();
            this.tblLayoutTop.ResumeLayout(false);
            this.tblLayoutTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tblLayoutLogo.ResumeLayout(false);
            this.tblLayoutLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLayoutMain;
        private System.Windows.Forms.TableLayoutPanel tblLayoutTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxVIN;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TableLayoutPanel tblLayoutLogo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblLogo;
    }
}

