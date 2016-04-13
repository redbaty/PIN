namespace PIN.Core.Forms
{
    partial class iCreator
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtX86 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtX64 = new System.Windows.Forms.TextBox();
            this.btnFindpath = new System.Windows.Forms.Button();
            this.btnFindpath2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPrior = new System.Windows.Forms.NumericUpDown();
            this.saveBtn = new System.Windows.Forms.Button();
            this.XXaq = new System.Windows.Forms.Label();
            this.txtArgs = new System.Windows.Forms.TextBox();
            this.btnURL = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrior)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(88, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(184, 20);
            this.txtName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Package name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Local version";
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(88, 38);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(184, 20);
            this.txtVersion.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Update url";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(88, 90);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(113, 20);
            this.txtUrl.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "X86 Executable";
            // 
            // txtX86
            // 
            this.txtX86.Location = new System.Drawing.Point(88, 116);
            this.txtX86.Name = "txtX86";
            this.txtX86.Size = new System.Drawing.Size(152, 20);
            this.txtX86.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "X64 Executable";
            // 
            // txtX64
            // 
            this.txtX64.Location = new System.Drawing.Point(88, 142);
            this.txtX64.Name = "txtX64";
            this.txtX64.Size = new System.Drawing.Size(152, 20);
            this.txtX64.TabIndex = 8;
            // 
            // btnFindpath
            // 
            this.btnFindpath.Location = new System.Drawing.Point(246, 116);
            this.btnFindpath.Name = "btnFindpath";
            this.btnFindpath.Size = new System.Drawing.Size(26, 20);
            this.btnFindpath.TabIndex = 10;
            this.btnFindpath.Text = "...";
            this.btnFindpath.UseVisualStyleBackColor = true;
            this.btnFindpath.Click += new System.EventHandler(this.btnFindpath_Click);
            // 
            // btnFindpath2
            // 
            this.btnFindpath2.Location = new System.Drawing.Point(246, 142);
            this.btnFindpath2.Name = "btnFindpath2";
            this.btnFindpath2.Size = new System.Drawing.Size(26, 20);
            this.btnFindpath2.TabIndex = 11;
            this.btnFindpath2.Text = "...";
            this.btnFindpath2.UseVisualStyleBackColor = true;
            this.btnFindpath2.Click += new System.EventHandler(this.btnFindpath2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Installation Priority";
            // 
            // txtPrior
            // 
            this.txtPrior.Location = new System.Drawing.Point(101, 168);
            this.txtPrior.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.txtPrior.Name = "txtPrior";
            this.txtPrior.Size = new System.Drawing.Size(54, 20);
            this.txtPrior.TabIndex = 14;
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(197, 165);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 15;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // XXaq
            // 
            this.XXaq.AutoSize = true;
            this.XXaq.Location = new System.Drawing.Point(3, 67);
            this.XXaq.Name = "XXaq";
            this.XXaq.Size = new System.Drawing.Size(57, 13);
            this.XXaq.TabIndex = 17;
            this.XXaq.Text = "Arguments";
            // 
            // txtArgs
            // 
            this.txtArgs.Location = new System.Drawing.Point(88, 64);
            this.txtArgs.Name = "txtArgs";
            this.txtArgs.Size = new System.Drawing.Size(184, 20);
            this.txtArgs.TabIndex = 16;
            // 
            // btnURL
            // 
            this.btnURL.Location = new System.Drawing.Point(207, 89);
            this.btnURL.Name = "btnURL";
            this.btnURL.Size = new System.Drawing.Size(65, 20);
            this.btnURL.TabIndex = 18;
            this.btnURL.Text = "Download";
            this.btnURL.UseVisualStyleBackColor = true;
            this.btnURL.Click += new System.EventHandler(this.btnURL_Click);
            // 
            // iCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 200);
            this.Controls.Add(this.btnURL);
            this.Controls.Add(this.XXaq);
            this.Controls.Add(this.txtArgs);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.txtPrior);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnFindpath2);
            this.Controls.Add(this.btnFindpath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtX64);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtX86);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Name = "iCreator";
            this.Text = "iCreator";
            ((System.ComponentModel.ISupportInitialize)(this.txtPrior)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtX86;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtX64;
        private System.Windows.Forms.Button btnFindpath;
        private System.Windows.Forms.Button btnFindpath2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown txtPrior;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Label XXaq;
        private System.Windows.Forms.TextBox txtArgs;
        private System.Windows.Forms.Button btnURL;
    }
}