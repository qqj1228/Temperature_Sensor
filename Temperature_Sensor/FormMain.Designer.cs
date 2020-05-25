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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tblLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblLogo = new System.Windows.Forms.Label();
            this.tblLayoutStatus = new System.Windows.Forms.TableLayoutPanel();
            this.lblStatus1 = new System.Windows.Forms.Label();
            this.lblStatus2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxVIN = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblInfo = new System.Windows.Forms.Label();
            this.tblLayoutValue = new System.Windows.Forms.TableLayoutPanel();
            this.grpBoxMode = new System.Windows.Forms.GroupBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.grpBoxTemper2 = new System.Windows.Forms.GroupBox();
            this.lblTemper2 = new System.Windows.Forms.Label();
            this.grpBoxTemper1 = new System.Windows.Forms.GroupBox();
            this.lblTemper1 = new System.Windows.Forms.Label();
            this.grpBoxSetup = new System.Windows.Forms.GroupBox();
            this.lblSetup = new System.Windows.Forms.Label();
            this.grpBoxSurrounding = new System.Windows.Forms.GroupBox();
            this.lblSurrounding = new System.Windows.Forms.Label();
            this.grpBoxRPM = new System.Windows.Forms.GroupBox();
            this.tblLayoutRPM = new System.Windows.Forms.TableLayoutPanel();
            this.lblRPM = new System.Windows.Forms.Label();
            this.pgrBarRPM = new Temperature_Sensor.VerticalProgressBar();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemStatistics = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemLoop = new System.Windows.Forms.ToolStripMenuItem();
            this.tblLayoutMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tblLayoutStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tblLayoutValue.SuspendLayout();
            this.grpBoxMode.SuspendLayout();
            this.grpBoxTemper2.SuspendLayout();
            this.grpBoxTemper1.SuspendLayout();
            this.grpBoxSetup.SuspendLayout();
            this.grpBoxSurrounding.SuspendLayout();
            this.grpBoxRPM.SuspendLayout();
            this.tblLayoutRPM.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLayoutMain
            // 
            this.tblLayoutMain.ColumnCount = 3;
            this.tblLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tblLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tblLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblLayoutMain.Controls.Add(this.pictureBox1, 0, 0);
            this.tblLayoutMain.Controls.Add(this.lblLogo, 1, 0);
            this.tblLayoutMain.Controls.Add(this.tblLayoutStatus, 2, 0);
            this.tblLayoutMain.Controls.Add(this.label1, 0, 1);
            this.tblLayoutMain.Controls.Add(this.txtBoxVIN, 1, 1);
            this.tblLayoutMain.Controls.Add(this.chart1, 1, 2);
            this.tblLayoutMain.Controls.Add(this.lblInfo, 0, 3);
            this.tblLayoutMain.Controls.Add(this.tblLayoutValue, 0, 2);
            this.tblLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutMain.Location = new System.Drawing.Point(0, 0);
            this.tblLayoutMain.Name = "tblLayoutMain";
            this.tblLayoutMain.RowCount = 4;
            this.tblLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53F));
            this.tblLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLayoutMain.Size = new System.Drawing.Size(784, 561);
            this.tblLayoutMain.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Temperature_Sensor.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(174, 78);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogo.Font = new System.Drawing.Font("宋体", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLogo.Location = new System.Drawing.Point(183, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(464, 84);
            this.lblLogo.TabIndex = 1;
            this.lblLogo.Text = "车辆空调温度检测系统";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblLayoutStatus
            // 
            this.tblLayoutStatus.ColumnCount = 1;
            this.tblLayoutStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayoutStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLayoutStatus.Controls.Add(this.lblStatus1, 0, 0);
            this.tblLayoutStatus.Controls.Add(this.lblStatus2, 0, 1);
            this.tblLayoutStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutStatus.Location = new System.Drawing.Point(653, 3);
            this.tblLayoutStatus.Name = "tblLayoutStatus";
            this.tblLayoutStatus.RowCount = 2;
            this.tblLayoutStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLayoutStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLayoutStatus.Size = new System.Drawing.Size(128, 78);
            this.tblLayoutStatus.TabIndex = 2;
            // 
            // lblStatus1
            // 
            this.lblStatus1.AutoSize = true;
            this.lblStatus1.BackColor = System.Drawing.Color.Red;
            this.lblStatus1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStatus1.Location = new System.Drawing.Point(3, 0);
            this.lblStatus1.Name = "lblStatus1";
            this.lblStatus1.Size = new System.Drawing.Size(122, 39);
            this.lblStatus1.TabIndex = 0;
            this.lblStatus1.Text = "测温站:000";
            this.lblStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus2
            // 
            this.lblStatus2.AutoSize = true;
            this.lblStatus2.BackColor = System.Drawing.Color.Red;
            this.lblStatus2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus2.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStatus2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblStatus2.Location = new System.Drawing.Point(3, 39);
            this.lblStatus2.Name = "lblStatus2";
            this.lblStatus2.Size = new System.Drawing.Size(122, 39);
            this.lblStatus2.TabIndex = 1;
            this.lblStatus2.Text = "连接异常";
            this.lblStatus2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("宋体", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 78);
            this.label1.TabIndex = 0;
            this.label1.Text = "VIN:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBoxVIN
            // 
            this.txtBoxVIN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tblLayoutMain.SetColumnSpan(this.txtBoxVIN, 2);
            this.txtBoxVIN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxVIN.Font = new System.Drawing.Font("宋体", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBoxVIN.Location = new System.Drawing.Point(183, 87);
            this.txtBoxVIN.Name = "txtBoxVIN";
            this.txtBoxVIN.Size = new System.Drawing.Size(598, 68);
            this.txtBoxVIN.TabIndex = 1;
            this.txtBoxVIN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBoxVIN_KeyPress);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.tblLayoutMain.SetColumnSpan(this.chart1, 2);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.BackColor = System.Drawing.SystemColors.Control;
            legend1.DockedToChartArea = "ChartArea1";
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(183, 165);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(598, 291);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.tblLayoutMain.SetColumnSpan(this.lblInfo, 3);
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInfo.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInfo.Location = new System.Drawing.Point(3, 459);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(778, 102);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "等待检测空调温度";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblLayoutValue
            // 
            this.tblLayoutValue.ColumnCount = 2;
            this.tblLayoutValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tblLayoutValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tblLayoutValue.Controls.Add(this.grpBoxMode, 0, 4);
            this.tblLayoutValue.Controls.Add(this.grpBoxTemper2, 0, 3);
            this.tblLayoutValue.Controls.Add(this.grpBoxTemper1, 0, 2);
            this.tblLayoutValue.Controls.Add(this.grpBoxSetup, 0, 1);
            this.tblLayoutValue.Controls.Add(this.grpBoxSurrounding, 0, 0);
            this.tblLayoutValue.Controls.Add(this.grpBoxRPM, 1, 0);
            this.tblLayoutValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutValue.Location = new System.Drawing.Point(3, 165);
            this.tblLayoutValue.Name = "tblLayoutValue";
            this.tblLayoutValue.RowCount = 5;
            this.tblLayoutValue.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLayoutValue.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLayoutValue.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLayoutValue.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLayoutValue.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLayoutValue.Size = new System.Drawing.Size(174, 291);
            this.tblLayoutValue.TabIndex = 3;
            // 
            // grpBoxMode
            // 
            this.grpBoxMode.Controls.Add(this.lblMode);
            this.grpBoxMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoxMode.Location = new System.Drawing.Point(3, 235);
            this.grpBoxMode.Name = "grpBoxMode";
            this.grpBoxMode.Size = new System.Drawing.Size(89, 53);
            this.grpBoxMode.TabIndex = 4;
            this.grpBoxMode.TabStop = false;
            this.grpBoxMode.Text = "空调模式";
            // 
            // lblMode
            // 
            this.lblMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMode.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMode.Location = new System.Drawing.Point(3, 17);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(83, 33);
            this.lblMode.TabIndex = 0;
            this.lblMode.Text = "制冷";
            this.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpBoxTemper2
            // 
            this.grpBoxTemper2.Controls.Add(this.lblTemper2);
            this.grpBoxTemper2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoxTemper2.Location = new System.Drawing.Point(3, 177);
            this.grpBoxTemper2.Name = "grpBoxTemper2";
            this.grpBoxTemper2.Size = new System.Drawing.Size(89, 52);
            this.grpBoxTemper2.TabIndex = 3;
            this.grpBoxTemper2.TabStop = false;
            this.grpBoxTemper2.Text = "空调温度2";
            // 
            // lblTemper2
            // 
            this.lblTemper2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTemper2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTemper2.Location = new System.Drawing.Point(3, 17);
            this.lblTemper2.Name = "lblTemper2";
            this.lblTemper2.Size = new System.Drawing.Size(83, 32);
            this.lblTemper2.TabIndex = 0;
            this.lblTemper2.Text = "20℃";
            this.lblTemper2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpBoxTemper1
            // 
            this.grpBoxTemper1.Controls.Add(this.lblTemper1);
            this.grpBoxTemper1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoxTemper1.Location = new System.Drawing.Point(3, 119);
            this.grpBoxTemper1.Name = "grpBoxTemper1";
            this.grpBoxTemper1.Size = new System.Drawing.Size(89, 52);
            this.grpBoxTemper1.TabIndex = 2;
            this.grpBoxTemper1.TabStop = false;
            this.grpBoxTemper1.Text = "空调温度1";
            // 
            // lblTemper1
            // 
            this.lblTemper1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTemper1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTemper1.Location = new System.Drawing.Point(3, 17);
            this.lblTemper1.Name = "lblTemper1";
            this.lblTemper1.Size = new System.Drawing.Size(83, 32);
            this.lblTemper1.TabIndex = 0;
            this.lblTemper1.Text = "20℃";
            this.lblTemper1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpBoxSetup
            // 
            this.grpBoxSetup.Controls.Add(this.lblSetup);
            this.grpBoxSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoxSetup.Location = new System.Drawing.Point(3, 61);
            this.grpBoxSetup.Name = "grpBoxSetup";
            this.grpBoxSetup.Size = new System.Drawing.Size(89, 52);
            this.grpBoxSetup.TabIndex = 1;
            this.grpBoxSetup.TabStop = false;
            this.grpBoxSetup.Text = "设定温度";
            // 
            // lblSetup
            // 
            this.lblSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSetup.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSetup.Location = new System.Drawing.Point(3, 17);
            this.lblSetup.Name = "lblSetup";
            this.lblSetup.Size = new System.Drawing.Size(83, 32);
            this.lblSetup.TabIndex = 0;
            this.lblSetup.Text = "20℃";
            this.lblSetup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpBoxSurrounding
            // 
            this.grpBoxSurrounding.Controls.Add(this.lblSurrounding);
            this.grpBoxSurrounding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoxSurrounding.Location = new System.Drawing.Point(3, 3);
            this.grpBoxSurrounding.Name = "grpBoxSurrounding";
            this.grpBoxSurrounding.Size = new System.Drawing.Size(89, 52);
            this.grpBoxSurrounding.TabIndex = 0;
            this.grpBoxSurrounding.TabStop = false;
            this.grpBoxSurrounding.Text = "环境温度";
            // 
            // lblSurrounding
            // 
            this.lblSurrounding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSurrounding.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSurrounding.Location = new System.Drawing.Point(3, 17);
            this.lblSurrounding.Name = "lblSurrounding";
            this.lblSurrounding.Size = new System.Drawing.Size(83, 32);
            this.lblSurrounding.TabIndex = 0;
            this.lblSurrounding.Text = "20℃";
            this.lblSurrounding.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpBoxRPM
            // 
            this.grpBoxRPM.Controls.Add(this.tblLayoutRPM);
            this.grpBoxRPM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoxRPM.Location = new System.Drawing.Point(98, 3);
            this.grpBoxRPM.Name = "grpBoxRPM";
            this.tblLayoutValue.SetRowSpan(this.grpBoxRPM, 5);
            this.grpBoxRPM.Size = new System.Drawing.Size(73, 285);
            this.grpBoxRPM.TabIndex = 5;
            this.grpBoxRPM.TabStop = false;
            this.grpBoxRPM.Text = "引擎转速";
            // 
            // tblLayoutRPM
            // 
            this.tblLayoutRPM.ColumnCount = 3;
            this.tblLayoutRPM.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLayoutRPM.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLayoutRPM.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLayoutRPM.Controls.Add(this.lblRPM, 0, 1);
            this.tblLayoutRPM.Controls.Add(this.pgrBarRPM, 1, 0);
            this.tblLayoutRPM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutRPM.Location = new System.Drawing.Point(3, 17);
            this.tblLayoutRPM.Name = "tblLayoutRPM";
            this.tblLayoutRPM.RowCount = 2;
            this.tblLayoutRPM.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92F));
            this.tblLayoutRPM.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblLayoutRPM.Size = new System.Drawing.Size(67, 265);
            this.tblLayoutRPM.TabIndex = 1;
            // 
            // lblRPM
            // 
            this.tblLayoutRPM.SetColumnSpan(this.lblRPM, 3);
            this.lblRPM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRPM.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRPM.Location = new System.Drawing.Point(3, 243);
            this.lblRPM.Name = "lblRPM";
            this.lblRPM.Size = new System.Drawing.Size(61, 22);
            this.lblRPM.TabIndex = 1;
            this.lblRPM.Text = "9999RPM";
            this.lblRPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pgrBarRPM
            // 
            this.pgrBarRPM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgrBarRPM.Location = new System.Drawing.Point(21, 3);
            this.pgrBarRPM.Maximum = 5000;
            this.pgrBarRPM.Name = "pgrBarRPM";
            this.pgrBarRPM.Size = new System.Drawing.Size(24, 237);
            this.pgrBarRPM.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgrBarRPM.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemStatistics,
            this.toolStripSeparator1,
            this.MenuItemLoop});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 54);
            // 
            // MenuItemStatistics
            // 
            this.MenuItemStatistics.Name = "MenuItemStatistics";
            this.MenuItemStatistics.Size = new System.Drawing.Size(148, 22);
            this.MenuItemStatistics.Text = "统计信息...(&S)";
            this.MenuItemStatistics.Click += new System.EventHandler(this.MenuItemStatistics_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // MenuItemLoop
            // 
            this.MenuItemLoop.Name = "MenuItemLoop";
            this.MenuItemLoop.Size = new System.Drawing.Size(148, 22);
            this.MenuItemLoop.Text = "持续测温(&L)";
            this.MenuItemLoop.Click += new System.EventHandler(this.MenuItemLoop_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.tblLayoutMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.tblLayoutMain.ResumeLayout(false);
            this.tblLayoutMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tblLayoutStatus.ResumeLayout(false);
            this.tblLayoutStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tblLayoutValue.ResumeLayout(false);
            this.grpBoxMode.ResumeLayout(false);
            this.grpBoxTemper2.ResumeLayout(false);
            this.grpBoxTemper1.ResumeLayout(false);
            this.grpBoxSetup.ResumeLayout(false);
            this.grpBoxSurrounding.ResumeLayout(false);
            this.grpBoxRPM.ResumeLayout(false);
            this.tblLayoutRPM.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLayoutMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxVIN;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.TableLayoutPanel tblLayoutStatus;
        private System.Windows.Forms.Label lblStatus1;
        private System.Windows.Forms.Label lblStatus2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemStatistics;
        private System.Windows.Forms.TableLayoutPanel tblLayoutValue;
        private System.Windows.Forms.GroupBox grpBoxMode;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.GroupBox grpBoxTemper2;
        private System.Windows.Forms.Label lblTemper2;
        private System.Windows.Forms.GroupBox grpBoxTemper1;
        private System.Windows.Forms.Label lblTemper1;
        private System.Windows.Forms.GroupBox grpBoxSetup;
        private System.Windows.Forms.Label lblSetup;
        private System.Windows.Forms.GroupBox grpBoxSurrounding;
        private System.Windows.Forms.Label lblSurrounding;
        private System.Windows.Forms.GroupBox grpBoxRPM;
        private System.Windows.Forms.TableLayoutPanel tblLayoutRPM;
        private System.Windows.Forms.Label lblRPM;
        private VerticalProgressBar pgrBarRPM;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemLoop;
    }
}

