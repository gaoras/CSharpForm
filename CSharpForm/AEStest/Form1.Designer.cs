namespace AEStest
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.TB_FileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RB_enc = new System.Windows.Forms.RadioButton();
            this.RB_dec = new System.Windows.Forms.RadioButton();
            this.GB_proc = new System.Windows.Forms.GroupBox();
            this.BT_exe = new System.Windows.Forms.Button();
            this.TB_password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GB_proc.SuspendLayout();
            this.SuspendLayout();
            // 
            // TB_FileName
            // 
            this.TB_FileName.Location = new System.Drawing.Point(26, 45);
            this.TB_FileName.Name = "TB_FileName";
            this.TB_FileName.Size = new System.Drawing.Size(194, 19);
            this.TB_FileName.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "ファイル名";
            // 
            // RB_enc
            // 
            this.RB_enc.AutoSize = true;
            this.RB_enc.Location = new System.Drawing.Point(18, 28);
            this.RB_enc.Name = "RB_enc";
            this.RB_enc.Size = new System.Drawing.Size(59, 16);
            this.RB_enc.TabIndex = 6;
            this.RB_enc.TabStop = true;
            this.RB_enc.Text = "暗号化";
            this.RB_enc.UseVisualStyleBackColor = true;
            // 
            // RB_dec
            // 
            this.RB_dec.AutoSize = true;
            this.RB_dec.Location = new System.Drawing.Point(107, 28);
            this.RB_dec.Name = "RB_dec";
            this.RB_dec.Size = new System.Drawing.Size(59, 16);
            this.RB_dec.TabIndex = 7;
            this.RB_dec.TabStop = true;
            this.RB_dec.Text = "復号化";
            this.RB_dec.UseVisualStyleBackColor = true;
            // 
            // GB_proc
            // 
            this.GB_proc.Controls.Add(this.RB_dec);
            this.GB_proc.Controls.Add(this.RB_enc);
            this.GB_proc.Location = new System.Drawing.Point(37, 76);
            this.GB_proc.Name = "GB_proc";
            this.GB_proc.Size = new System.Drawing.Size(182, 61);
            this.GB_proc.TabIndex = 8;
            this.GB_proc.TabStop = false;
            this.GB_proc.Text = "処理内容";
            // 
            // BT_exe
            // 
            this.BT_exe.Location = new System.Drawing.Point(145, 221);
            this.BT_exe.Name = "BT_exe";
            this.BT_exe.Size = new System.Drawing.Size(75, 23);
            this.BT_exe.TabIndex = 11;
            this.BT_exe.Text = "実行";
            this.BT_exe.UseVisualStyleBackColor = true;
            this.BT_exe.Click += new System.EventHandler(this.BT_exe_Click);
            // 
            // TB_password
            // 
            this.TB_password.Location = new System.Drawing.Point(26, 178);
            this.TB_password.Name = "TB_password";
            this.TB_password.Size = new System.Drawing.Size(100, 19);
            this.TB_password.TabIndex = 9;
            this.TB_password.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "パスワード";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 269);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TB_password);
            this.Controls.Add(this.BT_exe);
            this.Controls.Add(this.GB_proc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_FileName);
            this.Name = "Form1";
            this.Text = "Encryption";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.GB_proc.ResumeLayout(false);
            this.GB_proc.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TB_FileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton RB_enc;
        private System.Windows.Forms.RadioButton RB_dec;
        private System.Windows.Forms.GroupBox GB_proc;
        private System.Windows.Forms.Button BT_exe;
        private System.Windows.Forms.TextBox TB_password;
        private System.Windows.Forms.Label label2;
    }
}

