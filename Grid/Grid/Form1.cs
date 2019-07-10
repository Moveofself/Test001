using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Grid
{
    public partial class Form1 : Form
    {

        public bool MouseIsDown;
        public Graphics graphics;
        public Bitmap bmp;
        public int iX;
        public int iY;
        public float fX;
        public float fY;
        public float fPen;
        public int a;
        public int b;

        public int scale;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseWheel += new MouseEventHandler(pictureBox1_MouseWheel);
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //设点
        private void button1_Click(object sender, EventArgs e)
        {

        }

        //画图
        private void button2_Click(object sender, EventArgs e)
        {



            bmp = new Bitmap(pictureBox1.Width - 6, pictureBox1.Height - 6);
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            graphics = Graphics.FromImage(bmp);
            pictureBox1.Focus();

            graphics.Clear(Color.White);

            iX = int.Parse(textBox1.Text);
            iY = int.Parse(textBox2.Text);

            fX = (float)(bmp.Width) / iX;
            fY = (float)(bmp.Height) / iY;

            pictureBox1.Image = bmp;
            fPen = 2;
            //创建绘图对象Graphics

            Pen pen = new Pen(Color.Black, fPen);

            //竖线
            for (int i = 0; i <= iX; i++)
            {
                graphics.DrawLine(pen, fX * i, 0, fX * i, bmp.Height);
            }

            //横线
            for (int j = 0; j <= iY; j++)
            {
                graphics.DrawLine(pen, 0 + fPen / 2, fY * j, bmp.Width, fY * j);
            }
            //graphics.DrawRectangle(pen, 50, 50, x*padding, y*padding);
            pictureBox1.Refresh();


            dataGridView1.RowCount = iY;
            dataGridView1.ColumnCount = iX;
            dataGridView1.Visible = true;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ClearSelection();
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToResizeColumns = false;

            dataGridView1.Columns[0].Width = (int)fX;
        }

        void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            int mouseRow = e.Y;//确定鼠标移动到的行
            int mouseCol = e.X;
            if (MouseIsDown)
            {
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int aX = e.X;
            int aY = e.Y;



            Brush brush = new SolidBrush(Color.Blue);
            graphics.FillRectangle(brush, (int)((e.X / fX)) * fX + (fPen / 2), (int)(e.Y / fY) * fY + (fPen / 2), fX - fPen, fY - fPen);
            pictureBox1.Refresh();
            //lastRow = e.Y / BlockHeight - 1 + startRow;//确定行的位置
            //lastCol = e.X / BlockWidth - 1;
            //if (lastCol >= 0 && lastCol <= 7 && lastRow >= 0 && lastRow <= 63)
            //{
            //    int index = 8 * (lastRow) + lastCol;//确定信号的startbit
            //    if (list.Contains(index))//此处判断是否按下了信号
            //    {
            //        startBit = list.Min();//若是，则获取按下的信号
            //        MouseIsDown = true;
            //    }
            //}

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int aX = (int)(e.X / fX);
            int aY = (int)(e.Y / fY);
            textBox3.Text = "X轴：" + aX + "Y轴：" + aY;


        }

        private void button3_Click(object sender, EventArgs e)
        {

            //for (int i = 0; i < iX; i++)
            //{
            //    for (int j = 0; j < iX; j++)
            //    {
            //        if ((i + j) % 2 == 0)
            //        {
            //            graphics.FillRectangle(new SolidBrush(Color.Green), i * fX + (fPen / 2), j * fY + (fPen / 2), fX - (fPen / 2), fY - (fPen / 2));
            //        }
            //        else
            //        {
            //            graphics.FillRectangle(new SolidBrush(Color.Red), i * fX + (fPen / 2), j * fY + (fPen / 2), fX - (fPen / 2), fY - (fPen / 2));
            //        }
            //    }
            //}

            Timer sTimer = null;
            sTimer = new Timer();
            sTimer.Tick += new EventHandler(OnTimedEvent);
            sTimer.Interval = 100;
            sTimer.Start();

        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            int a2, b2;
            //aphics.Clear(Color.White);

            Random ro = new Random(10);
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));

            int R = ran.Next(255);
            int G = ran.Next(255);
            int B = ran.Next(255);
            B = (R + G > 400) ? R + G - 400 : B;//0 : 380 - R - G;
            B = (B > 255) ? 255 : B;
            a2 = a;
            b2 = b;
            if (a2>=iX)
            {
                a2 = 0;
                b2 = b2 + 1;
            }
            if (b2>=iY)
            {
                a2 = 0;
                b2 = 0;
            }
            //dataGridView1.Rows[b2].Cells[a2].Style.BackColor = Color.FromArgb(R, G, B);
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(R, G, B)), a * fX + (fPen / 2), b * fY + (fPen / 2), fX - (fPen / 2), fY - (fPen / 2));
            pictureBox1.Refresh();
            a = a + 1;
            if (a > iX)
            {
                a = 0;
                b = b + 1;
            }
            if (b > iY)
            {
                a = 0;
                b = 0;
                button2_Click(sender, e);
                //aphics.Clear(Color.White);
            }

            #region Test
            //a = a + 1;

            //if (a + 18> iX)
            //{
            //    a = 0;
            //}



            //if (iX >= 5 && iY >= 6)
            //{
            //    for (int i = 0; i < iX; i++)
            //    {
            //        for (int j = 0; j < iX; j++)
            //        {
            //            if ((i == 0 + a || i == 1 + a || i == 2 + a || i == 3 + a) && (j == 0 || j == 2) || (i == 0 + a) && (j == 1 || j == 3 || j == 4 || j == 5)
            //                || (i == 5 + a ) && (j == 0 || j ==1 || j == 2 || j == 3 || j == 4) || (i == 8 + a) && (j == 0 || j == 1 || j == 2 || j == 3 || j == 4) || (i == 6 + a || i == 7 + a) && (j == 5)
            //                || (i == 11 + a || i == 12 + a || i == 13 + a) && (j == 0 ||  j == 5) || (i == 10 + a) && (j == 1 || j == 2 || j == 3 || j == 4)
            //                || (i == 15 + a) && (j == 0 || j == 1 || j == 2 || j == 3 || j == 4 || j == 5) || (i == 16 + a) && (j == 2 || j == 3)
            //                || (i == 17 + a) && (j == 1 || j == 4)
            //                || (i == 18 + a) && (j == 0 || j == 5)
            //                )
            //            {
            //                graphics.FillRectangle(new SolidBrush(Color.FromArgb(R, G, B)), i * fX + (fPen / 2), j * fY + (fPen / 2), fX - (fPen / 2), fY - (fPen / 2));
            //            }
            //            else
            //            {
            //                //graphics.FillRectangle(new SolidBrush(Color.Red), i * fX + (fPen / 2), j * fY + (fPen / 2), fX - (fPen / 2), fY - (fPen / 2));
            //            }
            //        }
            //    }
            //}
            #endregion
        }

        private void pictureBox1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            int newWidth, newHeight;
            //判断上滑还是下滑
            if (e.Delta < 0)
            {
                //计算缩放大小
                newWidth = bmp.Width * 9 / 10;
                newHeight = bmp.Height * 9 / 10;

            }
            else
            {
                newWidth = bmp.Width * 11 / 10;
                newHeight = this.pictureBox1.Height * 11 / 10;
            }

            Bitmap b = new Bitmap(newWidth, newHeight);
            Graphics g = Graphics.FromImage(b);

            // 插值算法的质量
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(bmp, 0, 0, newWidth, newHeight);
            //g.Dispose();
            pictureBox1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Random randomX = new Random();
            Random randomY = new Random();
            int aX = randomX.Next(0,iX);
            int aY = randomY.Next(0, iY);
            dataGridView1.Rows[aX].Cells[aY].Style.BackColor = Color.Red;

        }
    }
}
