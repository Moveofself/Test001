namespace TestForm
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnClick = new System.Windows.Forms.Button();
            this.txtShow = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnWCF = new System.Windows.Forms.Button();
            this.lblStrip = new System.Windows.Forms.Label();
            this.txtStrip = new System.Windows.Forms.TextBox();
            this.txtLot = new System.Windows.Forms.TextBox();
            this.txtEqp = new System.Windows.Forms.TextBox();
            this.lblLot = new System.Windows.Forms.Label();
            this.lblEqp = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnClick
            // 
            this.btnClick.BackColor = System.Drawing.SystemColors.Control;
            this.btnClick.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnClick.Location = new System.Drawing.Point(47, 21);
            this.btnClick.Name = "btnClick";
            this.btnClick.Size = new System.Drawing.Size(92, 37);
            this.btnClick.TabIndex = 0;
            this.btnClick.Text = "Click";
            this.btnClick.UseVisualStyleBackColor = false;
            this.btnClick.Click += new System.EventHandler(this.btnClick_Click);
            // 
            // txtShow
            // 
            this.txtShow.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtShow.Location = new System.Drawing.Point(180, 26);
            this.txtShow.Name = "txtShow";
            this.txtShow.Size = new System.Drawing.Size(205, 26);
            this.txtShow.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(47, 91);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(78, 40);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(180, 73);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(644, 331);
            this.txtLog.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(863, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 48);
            this.button1.TabIndex = 4;
            this.button1.Text = "二进制文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(863, 164);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(131, 58);
            this.button2.TabIndex = 5;
            this.button2.Text = "CSV";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnWCF
            // 
            this.btnWCF.Location = new System.Drawing.Point(47, 440);
            this.btnWCF.Name = "btnWCF";
            this.btnWCF.Size = new System.Drawing.Size(117, 71);
            this.btnWCF.TabIndex = 6;
            this.btnWCF.Text = "WCF";
            this.btnWCF.UseVisualStyleBackColor = true;
            this.btnWCF.Click += new System.EventHandler(this.btnWCF_Click);
            // 
            // lblStrip
            // 
            this.lblStrip.AutoSize = true;
            this.lblStrip.Location = new System.Drawing.Point(206, 433);
            this.lblStrip.Name = "lblStrip";
            this.lblStrip.Size = new System.Drawing.Size(35, 12);
            this.lblStrip.TabIndex = 7;
            this.lblStrip.Text = "Strip";
            // 
            // txtStrip
            // 
            this.txtStrip.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStrip.Location = new System.Drawing.Point(262, 426);
            this.txtStrip.Name = "txtStrip";
            this.txtStrip.Size = new System.Drawing.Size(205, 26);
            this.txtStrip.TabIndex = 8;
            // 
            // txtLot
            // 
            this.txtLot.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLot.Location = new System.Drawing.Point(262, 462);
            this.txtLot.Name = "txtLot";
            this.txtLot.Size = new System.Drawing.Size(205, 26);
            this.txtLot.TabIndex = 9;
            // 
            // txtEqp
            // 
            this.txtEqp.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEqp.Location = new System.Drawing.Point(262, 499);
            this.txtEqp.Name = "txtEqp";
            this.txtEqp.Size = new System.Drawing.Size(205, 26);
            this.txtEqp.TabIndex = 10;
            // 
            // lblLot
            // 
            this.lblLot.AutoSize = true;
            this.lblLot.Location = new System.Drawing.Point(206, 469);
            this.lblLot.Name = "lblLot";
            this.lblLot.Size = new System.Drawing.Size(23, 12);
            this.lblLot.TabIndex = 11;
            this.lblLot.Text = "Lot";
            // 
            // lblEqp
            // 
            this.lblEqp.AutoSize = true;
            this.lblEqp.Location = new System.Drawing.Point(206, 506);
            this.lblEqp.Name = "lblEqp";
            this.lblEqp.Size = new System.Drawing.Size(23, 12);
            this.lblEqp.TabIndex = 12;
            this.lblEqp.Text = "Eqp";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(854, 255);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(116, 56);
            this.button3.TabIndex = 13;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtUser
            // 
            this.txtUser.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUser.Location = new System.Drawing.Point(830, 326);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(205, 26);
            this.txtUser.TabIndex = 14;
            // 
            // txtPWD
            // 
            this.txtPWD.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPWD.Location = new System.Drawing.Point(829, 369);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.PasswordChar = '*';
            this.txtPWD.Size = new System.Drawing.Size(205, 26);
            this.txtPWD.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 538);
            this.Controls.Add(this.txtPWD);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lblEqp);
            this.Controls.Add(this.lblLot);
            this.Controls.Add(this.txtEqp);
            this.Controls.Add(this.txtLot);
            this.Controls.Add(this.txtStrip);
            this.Controls.Add(this.lblStrip);
            this.Controls.Add(this.btnWCF);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtShow);
            this.Controls.Add(this.btnClick);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClick;
        private System.Windows.Forms.TextBox txtShow;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnWCF;
        private System.Windows.Forms.Label lblStrip;
        private System.Windows.Forms.TextBox txtStrip;
        private System.Windows.Forms.TextBox txtLot;
        private System.Windows.Forms.TextBox txtEqp;
        private System.Windows.Forms.Label lblLot;
        private System.Windows.Forms.Label lblEqp;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPWD;
    }
}

