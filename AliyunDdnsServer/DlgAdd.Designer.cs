namespace LinkHome
{
    partial class DlgAdd
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
            this.txtRR = new System.Windows.Forms.TextBox();
            this.cboDomain = new System.Windows.Forms.ComboBox();
            this.chkDdns = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtRR
            // 
            this.txtRR.Location = new System.Drawing.Point(68, 66);
            this.txtRR.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRR.Name = "txtRR";
            this.txtRR.Size = new System.Drawing.Size(112, 25);
            this.txtRR.TabIndex = 0;
            // 
            // cboDomain
            // 
            this.cboDomain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDomain.FormattingEnabled = true;
            this.cboDomain.Location = new System.Drawing.Point(209, 66);
            this.cboDomain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboDomain.Name = "cboDomain";
            this.cboDomain.Size = new System.Drawing.Size(336, 23);
            this.cboDomain.TabIndex = 1;
            this.cboDomain.SelectedIndexChanged += new System.EventHandler(this.cboDomain_SelectedIndexChanged);
            // 
            // chkDdns
            // 
            this.chkDdns.AutoSize = true;
            this.chkDdns.Checked = true;
            this.chkDdns.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDdns.Location = new System.Drawing.Point(68, 132);
            this.chkDdns.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkDdns.Name = "chkDdns";
            this.chkDdns.Size = new System.Drawing.Size(188, 19);
            this.chkDdns.TabIndex = 2;
            this.chkDdns.Text = "启用动态解析到本地(&D)";
            this.chkDdns.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(156, 208);
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
            this.btnCancel.Location = new System.Drawing.Point(361, 208);
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
            this.label1.Location = new System.Drawing.Point(189, 71);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = ".";
            // 
            // DlgAdd
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(627, 296);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkDdns);
            this.Controls.Add(this.cboDomain);
            this.Controls.Add(this.txtRR);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgAdd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加";
            this.Load += new System.EventHandler(this.DlgAdd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRR;
        private System.Windows.Forms.ComboBox cboDomain;
        private System.Windows.Forms.CheckBox chkDdns;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
    }
}