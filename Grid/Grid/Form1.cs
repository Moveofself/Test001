using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

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
        public int aa;
        public int b;
        Timer sTimer = null;
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


            
            doubleBufferDataGridView1.Visible = true;
            doubleBufferDataGridView1.ReadOnly = true;
            doubleBufferDataGridView1.RowHeadersVisible = false;
            doubleBufferDataGridView1.AllowUserToAddRows = false;
            doubleBufferDataGridView1.ColumnHeadersVisible = false;
            doubleBufferDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //doubleBufferDataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            doubleBufferDataGridView1.ClearSelection();
            doubleBufferDataGridView1.AllowUserToResizeRows = false;
            doubleBufferDataGridView1.AllowUserToResizeColumns = false;
            doubleBufferDataGridView1.RowCount = iY;
            doubleBufferDataGridView1.ColumnCount = iX;
            doubleBufferDataGridView1.Columns[0].Width = (int)fX;
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
        private void GridView_MouseMove(object sender, MouseEventArgs e)
        {
            int aX = this.doubleBufferDataGridView1.HitTest(e.X, e.Y).RowIndex; //行
            int aY = this.doubleBufferDataGridView1.HitTest(e.X, e.Y).ColumnIndex; //列
  
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

            
            sTimer = new Timer();
            sTimer.Tick += new EventHandler(OnTimedEvent);
            sTimer.Interval = 100;
            sTimer.Start();

        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            int a2, b2;
            graphics.Clear(Color.White);

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
            if (a2 >= iX)
            {
                a2 = 0;
                b2 = b2 + 1;
            }
            if (b2 >= iY)
            {
                a2 = 0;
                b2 = 0;
            }
            this.doubleBufferDataGridView1.ClearSelection();
            doubleBufferDataGridView1.Rows[b2].Cells[a2].Style.BackColor = Color.Red;
            doubleBufferDataGridView1.Rows[b2].Cells[a2].Selected = true;

            doubleBufferDataGridView1.CurrentCell = doubleBufferDataGridView1.Rows[b2].Cells[a2];
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(R, G, B)), a * fX + (fPen / 2), b * fY + (fPen / 2), fX - fPen, fY - fPen);
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
                sTimer.Stop();
                //button2_Click(sender, e);
                //aphics.Clear(Color.White);
            }

            #region Test
            //aa = aa + 1;

            //if (aa + 18 > iX)
            //{
            //    aa = 0;
            //}



            //if (iX >= 5 && iY >= 6)
            //{
            //    for (int i = 0; i < iX; i++)
            //    {
            //        for (int j = 0; j < iX; j++)
            //        {
            //            if ((i == 0 + aa || i == 1 + aa || i == 2 + aa || i == 3 + aa) && (j == 0 || j == 2) || (i == 0 + aa) && (j == 1 || j == 3 || j == 4 || j == 5)
            //                || (i == 5 + aa) && (j == 0 || j == 1 || j == 2 || j == 3 || j == 4) || (i == 8 + aa) && (j == 0 || j == 1 || j == 2 || j == 3 || j == 4) || (i == 6 + aa || i == 7 + aa) && (j == 5)
            //                || (i == 11 + aa || i == 12 + aa || i == 13 + aa) && (j == 0 || j == 5) || (i == 10 + aa) && (j == 1 || j == 2 || j == 3 || j == 4)
            //                || (i == 15 + aa) && (j == 0 || j == 1 || j == 2 || j == 3 || j == 4 || j == 5) || (i == 16 + aa) && (j == 2 || j == 3)
            //                || (i == 17 + aa) && (j == 1 || j == 4)
            //                || (i == 18 + aa) && (j == 0 || j == 5)
            //                )
            //            {
            //                graphics.FillRectangle(new SolidBrush(Color.FromArgb(R, G, B)), i * fX + (fPen / 2), j * fY + (fPen / 2), fX - fPen, fY -fPen);
            //                pictureBox1.Refresh();
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

            int k = 0;
            int kk = ++k + k++ + ++k + k + k++;
            textBox4.Text = kk.ToString();

            ConvertDataSetToXMLFile(doubleBufferDataGridView1);
            //Random randomX = new Random();
            //Random randomY = new Random();
            //int aX = randomX.Next(0,iX);
            //int aY = randomY.Next(0, iY);
            //doubleBufferDataGridView1.Rows[aX].Cells[aY].Style.BackColor = Color.Red;

        }

        private void ConvertDataSetToXMLFile(DoubleBufferDataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Style.BackColor.Name);
                }
                dt.Rows.Add(dr);
            }

            try
            {
                dt.TableName = "TEST";
              string sFileName = "Data";

                dt.WriteXml("D:/ "+sFileName +".xml");
                MessageBox.Show("数据成功保存到" + "D:/ " + sFileName + ".xml", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
        }

    }
}
