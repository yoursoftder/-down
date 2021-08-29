namespace LinkHome
{
    partial class DlgConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTTL = new System.Windows.Forms.NumericUpDown();
            this.txtInterval = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAccessKeyID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAccessKeySecret = new System.Windows.Forms.TextBox();
            this.chkHideForm = new System.Windows.Forms.CheckBox();
            this.btnHowToGetAccessKey = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtTTL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(247, 411);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(185, 42);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(452, 411);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(185, 42);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 180);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "TTL";
            // 
            // txtTTL
            // 
            this.txtTTL.Location = new System.Drawing.Point(205, 179);
            this.txtTTL.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTTL.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.txtTTL.Minimum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.txtTTL.Name = "txtTTL";
            this.txtTTL.Size = new System.Drawing.Size(116, 25);
            this.txtTTL.TabIndex = 8;
            this.txtTTL.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            // 
            // txtInterval
            // 
            this.txtInterval.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.txtInterval.Location = new System.Drawing.Point(205, 221);
            this.txtInterval.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtInterval.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.txtInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(116, 25);
            this.txtInterval.TabIndex = 10;
            this.txtInterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 225);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "IP检查频率";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(329, 225);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "秒";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(329, 181);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "秒";
            // 
            // txtAccessKeyID
            // 
            this.txtAccessKeyID.Location = new System.Drawing.Point(205, 84);
            this.txtAccessKeyID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAccessKeyID.Name = "txtAccessKeyID";
            this.txtAccessKeyID.Size = new System.Drawing.Size(435, 25);
            this.txtAccessKeyID.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 88);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "AccessKey ID";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 135);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 15);
            this.label6.TabIndex = 16;
            this.label6.Text = "AccessKey Secret";
            // 
            // txtAccessKeySecret
            // 
            this.txtAccessKeySecret.Location = new System.Drawing.Point(205, 131);
            this.txtAccessKeySecret.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAccessKeySecret.Name = "txtAccessKeySecret";
            this.txtAccessKeySecret.Size = new System.Drawing.Size(435, 25);
            this.txtAccessKeySecret.TabIndex = 15;
            // 
            // chkHideForm
            // 
            this.chkHideForm.AutoSize = true;
            this.chkHideForm.Location = new System.Drawing.Point(51, 434);
            this.chkHideForm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkHideForm.Name = "chkHideForm";
            this.chkHideForm.Size = new System.Drawing.Size(149, 19);
            this.chkHideForm.TabIndex = 17;
            this.chkHideForm.Text = "启动时隐藏主窗口";
            this.chkHideForm.UseVisualStyleBackColor = true;
            this.chkHideForm.CheckedChanged += new System.EventHandler(this.chkHideForm_CheckedChanged);
            // 
            // btnHowToGetAccessKey
            // 
            this.btnHowToGetAccessKey.AutoSize = true;
            this.btnHowToGetAccessKey.Location = new System.Drawing.Point(339, 45);
            this.btnHowToGetAccessKey.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnHowToGetAccessKey.Name = "btnHowToGetAccessKey";
            this.btnHowToGetAccessKey.Size = new System.Drawing.Size(298, 15);
            this.btnHowToGetAccessKey.TabIndex = 18;
            this.btnHowToGetAccessKey.TabStop = true;
            this.btnHowToGetAccessKey.Text = "如何获取AccessKeyId和AccessKeySecret?";
            this.btnHowToGetAccessKey.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnHowToGetAccessKey_LinkClicked);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(48, 266);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 15);
            this.label7.TabIndex = 9;
            this.label7.Text = "启动后自动刷新";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "是",
            "否"});
            this.comboBox1.Location = new System.Drawing.Point(205, 266);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(47, 23);
            this.comboBox1.TabIndex = 20;
            this.comboBox1.Text = "是";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(48, 305);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 9;
            this.label8.Text = "服务端口";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "80",
            "88",
            "888",
            "8888",
            "8000",
            "8080",
            "8810",
            "其他请自己输入"});
            this.comboBox2.Location = new System.Drawing.Point(205, 305);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(90, 23);
            this.comboBox2.TabIndex = 21;
            this.comboBox2.Text = "33333";
            // 
            // DlgConfig
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(680, 466);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnHowToGetAccessKey);
            this.Controls.Add(this.chkHideForm);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAccessKeySecret);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtAccessKeyID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTTL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgConfig";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.DlgConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtTTL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtTTL;
        private System.Windows.Forms.NumericUpDown txtInterval;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAccessKeyID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAccessKeySecret;
        private System.Windows.Forms.CheckBox chkHideForm;
        private System.Windows.Forms.LinkLabel btnHowToGetAccessKey;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}