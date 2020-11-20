namespace Temperature_Sensor {
    partial class FormSetup {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetup));
            this.grpBoxPwd = new System.Windows.Forms.GroupBox();
            this.btnPwd = new System.Windows.Forms.Button();
            this.txtBoxNewPwd2 = new System.Windows.Forms.TextBox();
            this.txtBoxNewPwd1 = new System.Windows.Forms.TextBox();
            this.txtBoxOriginalPwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.grpBoxSetup = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtBoxSuccessiveValue = new System.Windows.Forms.TextBox();
            this.grpBoxExample = new System.Windows.Forms.GroupBox();
            this.txtBoxSetupTemper = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBoxEnvTemper = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbBoxRule = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbBoxTemper = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbBoxCooling = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBoxSetupValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grpBoxPwd.SuspendLayout();
            this.grpBoxSetup.SuspendLayout();
            this.grpBoxExample.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxPwd
            // 
            this.grpBoxPwd.Controls.Add(this.btnPwd);
            this.grpBoxPwd.Controls.Add(this.txtBoxNewPwd2);
            this.grpBoxPwd.Controls.Add(this.txtBoxNewPwd1);
            this.grpBoxPwd.Controls.Add(this.txtBoxOriginalPwd);
            this.grpBoxPwd.Controls.Add(this.label3);
            this.grpBoxPwd.Controls.Add(this.label2);
            this.grpBoxPwd.Controls.Add(this.label1);
            this.grpBoxPwd.Location = new System.Drawing.Point(13, 13);
            this.grpBoxPwd.Name = "grpBoxPwd";
            this.grpBoxPwd.Size = new System.Drawing.Size(360, 138);
            this.grpBoxPwd.TabIndex = 0;
            this.grpBoxPwd.TabStop = false;
            this.grpBoxPwd.Text = "修改管理员密码";
            // 
            // btnPwd
            // 
            this.btnPwd.Location = new System.Drawing.Point(264, 102);
            this.btnPwd.Name = "btnPwd";
            this.btnPwd.Size = new System.Drawing.Size(75, 23);
            this.btnPwd.TabIndex = 3;
            this.btnPwd.Text = "确定";
            this.btnPwd.UseVisualStyleBackColor = true;
            this.btnPwd.Click += new System.EventHandler(this.BtnPwd_Click);
            // 
            // txtBoxNewPwd2
            // 
            this.txtBoxNewPwd2.Location = new System.Drawing.Point(140, 75);
            this.txtBoxNewPwd2.Name = "txtBoxNewPwd2";
            this.txtBoxNewPwd2.Size = new System.Drawing.Size(200, 21);
            this.txtBoxNewPwd2.TabIndex = 2;
            // 
            // txtBoxNewPwd1
            // 
            this.txtBoxNewPwd1.Location = new System.Drawing.Point(140, 48);
            this.txtBoxNewPwd1.Name = "txtBoxNewPwd1";
            this.txtBoxNewPwd1.Size = new System.Drawing.Size(200, 21);
            this.txtBoxNewPwd1.TabIndex = 1;
            // 
            // txtBoxOriginalPwd
            // 
            this.txtBoxOriginalPwd.Location = new System.Drawing.Point(140, 21);
            this.txtBoxOriginalPwd.Name = "txtBoxOriginalPwd";
            this.txtBoxOriginalPwd.Size = new System.Drawing.Size(200, 21);
            this.txtBoxOriginalPwd.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "再次输入新密码：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "新密码：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "原密码：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(197, 219);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // grpBoxSetup
            // 
            this.grpBoxSetup.Controls.Add(this.btnCancel);
            this.grpBoxSetup.Controls.Add(this.txtBoxSuccessiveValue);
            this.grpBoxSetup.Controls.Add(this.btnOK);
            this.grpBoxSetup.Controls.Add(this.grpBoxExample);
            this.grpBoxSetup.Controls.Add(this.label8);
            this.grpBoxSetup.Controls.Add(this.cmbBoxRule);
            this.grpBoxSetup.Controls.Add(this.label7);
            this.grpBoxSetup.Controls.Add(this.cmbBoxTemper);
            this.grpBoxSetup.Controls.Add(this.label6);
            this.grpBoxSetup.Controls.Add(this.cmbBoxCooling);
            this.grpBoxSetup.Controls.Add(this.label5);
            this.grpBoxSetup.Controls.Add(this.txtBoxSetupValue);
            this.grpBoxSetup.Controls.Add(this.label4);
            this.grpBoxSetup.Location = new System.Drawing.Point(12, 157);
            this.grpBoxSetup.Name = "grpBoxSetup";
            this.grpBoxSetup.Size = new System.Drawing.Size(359, 252);
            this.grpBoxSetup.TabIndex = 1;
            this.grpBoxSetup.TabStop = false;
            this.grpBoxSetup.Text = "修改温度设定";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(278, 219);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtBoxSuccessiveValue
            // 
            this.txtBoxSuccessiveValue.Location = new System.Drawing.Point(140, 126);
            this.txtBoxSuccessiveValue.Name = "txtBoxSuccessiveValue";
            this.txtBoxSuccessiveValue.Size = new System.Drawing.Size(200, 21);
            this.txtBoxSuccessiveValue.TabIndex = 4;
            this.txtBoxSuccessiveValue.TextChanged += new System.EventHandler(this.TxtBoxSuccessiveValue_TextChanged);
            this.txtBoxSuccessiveValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // grpBoxExample
            // 
            this.grpBoxExample.Controls.Add(this.txtBoxSetupTemper);
            this.grpBoxExample.Controls.Add(this.label10);
            this.grpBoxExample.Controls.Add(this.txtBoxEnvTemper);
            this.grpBoxExample.Controls.Add(this.label9);
            this.grpBoxExample.Location = new System.Drawing.Point(6, 155);
            this.grpBoxExample.Name = "grpBoxExample";
            this.grpBoxExample.Size = new System.Drawing.Size(347, 58);
            this.grpBoxExample.TabIndex = 9;
            this.grpBoxExample.TabStop = false;
            this.grpBoxExample.Text = "举例";
            // 
            // txtBoxSetupTemper
            // 
            this.txtBoxSetupTemper.Location = new System.Drawing.Point(270, 20);
            this.txtBoxSetupTemper.Name = "txtBoxSetupTemper";
            this.txtBoxSetupTemper.ReadOnly = true;
            this.txtBoxSetupTemper.Size = new System.Drawing.Size(65, 21);
            this.txtBoxSetupTemper.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(175, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "设定温度(℃)：";
            // 
            // txtBoxEnvTemper
            // 
            this.txtBoxEnvTemper.Location = new System.Drawing.Point(105, 20);
            this.txtBoxEnvTemper.Name = "txtBoxEnvTemper";
            this.txtBoxEnvTemper.ReadOnly = true;
            this.txtBoxEnvTemper.Size = new System.Drawing.Size(65, 21);
            this.txtBoxEnvTemper.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "环境温度(℃)：";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(14, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "连续采样设定系数：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbBoxRule
            // 
            this.cmbBoxRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxRule.FormattingEnabled = true;
            this.cmbBoxRule.Location = new System.Drawing.Point(140, 100);
            this.cmbBoxRule.Name = "cmbBoxRule";
            this.cmbBoxRule.Size = new System.Drawing.Size(200, 20);
            this.cmbBoxRule.TabIndex = 3;
            this.cmbBoxRule.SelectedIndexChanged += new System.EventHandler(this.CmbBoxRule_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(14, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "合格判定依据：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbBoxTemper
            // 
            this.cmbBoxTemper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxTemper.FormattingEnabled = true;
            this.cmbBoxTemper.Location = new System.Drawing.Point(140, 74);
            this.cmbBoxTemper.Name = "cmbBoxTemper";
            this.cmbBoxTemper.Size = new System.Drawing.Size(200, 20);
            this.cmbBoxTemper.TabIndex = 2;
            this.cmbBoxTemper.SelectedIndexChanged += new System.EventHandler(this.CmbBoxTemper_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(14, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "使用温度点：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbBoxCooling
            // 
            this.cmbBoxCooling.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxCooling.FormattingEnabled = true;
            this.cmbBoxCooling.Location = new System.Drawing.Point(140, 48);
            this.cmbBoxCooling.Name = "cmbBoxCooling";
            this.cmbBoxCooling.Size = new System.Drawing.Size(200, 20);
            this.cmbBoxCooling.TabIndex = 1;
            this.cmbBoxCooling.SelectedIndexChanged += new System.EventHandler(this.CmbBoxCooling_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(14, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "空调工作模式：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBoxSetupValue
            // 
            this.txtBoxSetupValue.Location = new System.Drawing.Point(140, 20);
            this.txtBoxSetupValue.Name = "txtBoxSetupValue";
            this.txtBoxSetupValue.Size = new System.Drawing.Size(200, 21);
            this.txtBoxSetupValue.TabIndex = 0;
            this.txtBoxSetupValue.TextChanged += new System.EventHandler(this.TxtBoxSetupValue_TextChanged);
            this.txtBoxSetupValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(14, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "温度设定值/系数：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FormSetup
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 421);
            this.Controls.Add(this.grpBoxSetup);
            this.Controls.Add(this.grpBoxPwd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "温度设定";
            this.Load += new System.EventHandler(this.FormSetup_Load);
            this.grpBoxPwd.ResumeLayout(false);
            this.grpBoxPwd.PerformLayout();
            this.grpBoxSetup.ResumeLayout(false);
            this.grpBoxSetup.PerformLayout();
            this.grpBoxExample.ResumeLayout(false);
            this.grpBoxExample.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxPwd;
        private System.Windows.Forms.TextBox txtBoxNewPwd2;
        private System.Windows.Forms.TextBox txtBoxNewPwd1;
        private System.Windows.Forms.TextBox txtBoxOriginalPwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpBoxSetup;
        private System.Windows.Forms.TextBox txtBoxSetupValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpBoxExample;
        private System.Windows.Forms.TextBox txtBoxSetupTemper;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBoxEnvTemper;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbBoxRule;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbBoxTemper;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbBoxCooling;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtBoxSuccessiveValue;
        private System.Windows.Forms.Button btnPwd;
    }
}