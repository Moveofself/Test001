using System;
using System.Windows.Forms;

namespace Grid
{
    //双缓冲
    class DoubleBufferDataGridView : DataGridView
    {
        public DoubleBufferDataGridView()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }
    }

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.doubleBufferDataGridView1 = new Grid.DoubleBufferDataGridView();
            this.button7 = new System.Windows.Forms.Button();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtColor = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleBufferDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(446, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(58, 34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(69, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(167, 34);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(316, 34);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(569, 35);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(124, 21);
            this.textBox3.TabIndex = 5;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(758, 30);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(108, 28);
            this.button3.TabIndex = 6;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 94);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1012, 222);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1183, 29);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(46, 25);
            this.button4.TabIndex = 9;
            this.button4.Text = "set";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(1250, 33);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(111, 21);
            this.textBox4.TabIndex = 11;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(900, 34);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(259, 21);
            this.textBox5.TabIndex = 12;
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new System.Drawing.Point(1138, 483);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(158, 102);
            this.listBox1.TabIndex = 13;
            this.listBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseClick);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1046, 418);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(42, 32);
            this.button5.TabIndex = 14;
            this.button5.Text = "放大";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(900, 61);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(255, 21);
            this.textBox6.TabIndex = 15;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1046, 481);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(53, 32);
            this.button6.TabIndex = 16;
            this.button6.Text = "缩小";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // doubleBufferDataGridView1
            // 
            this.doubleBufferDataGridView1.AllowUserToResizeColumns = false;
            this.doubleBufferDataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.doubleBufferDataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.doubleBufferDataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.doubleBufferDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.doubleBufferDataGridView1.ColumnHeadersHeight = 20;
            this.doubleBufferDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.doubleBufferDataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.doubleBufferDataGridView1.Location = new System.Drawing.Point(12, 322);
            this.doubleBufferDataGridView1.MultiSelect = false;
            this.doubleBufferDataGridView1.Name = "doubleBufferDataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.doubleBufferDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.doubleBufferDataGridView1.RowHeadersWidth = 28;
            this.doubleBufferDataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.doubleBufferDataGridView1.RowTemplate.Height = 23;
            this.doubleBufferDataGridView1.Size = new System.Drawing.Size(1012, 311);
            this.doubleBufferDataGridView1.TabIndex = 10;
            this.doubleBufferDataGridView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GridView_MouseMove);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(1143, 110);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(107, 40);
            this.button7.TabIndex = 17;
            this.button7.Text = "button7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(1162, 184);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(76, 21);
            this.txtX.TabIndex = 18;
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(1161, 232);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(76, 21);
            this.txtY.TabIndex = 19;
            // 
            // txtColor
            // 
            this.txtColor.Location = new System.Drawing.Point(1167, 285);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(82, 21);
            this.txtColor.TabIndex = 20;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1589, 645);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.doubleBufferDataGridView1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleBufferDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button4;
        private Grid.DoubleBufferDataGridView doubleBufferDataGridView1;
        private TextBox textBox4;
        private TextBox textBox5;
        private ListBox listBox1;
        private Button button5;
        private TextBox textBox6;
        private Button button6;
        private Button button7;
        private TextBox txtX;
        private TextBox txtY;
        private TextBox txtColor;
    }
}

