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
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;
using System.Diagnostics;
using DBUnity;
using DBUnity.OraDbHelper;
using System.Configuration;
using System.Threading;
using System.Runtime.InteropServices;
using System.Threading.Tasks;


namespace Grid
{
    //Test
    public partial class Form1 : Form
    {
        long test = 0;
        public bool MouseIsDown;
        public Graphics graphics;
        public Bitmap bmp;
        public int iX;
        public int iY;
        public float fX;
        public float fY;

        public float fSize;
        public float fPen;
        public int a;
        public int aa;
        public int b;
        System.Windows.Forms.Timer sTimer = null;
        public int count = 0;

        public int width;
        public int height;

        static int LogCount = 20000;
        static int WritedCount = 0;
        static int FailedCount = 0;


        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();

        AutoSizeForm asc = new AutoSizeForm();
        AutoSizeFormClass asfc = new AutoSizeFormClass();

        private static string appLogPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\" + "Log";
        string FAQASDBConnectionString = ConfigurationManager.AppSettings["FAQASDBConnectionString"];

        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseWheel += new MouseEventHandler(pictureBox1_MouseWheel);
            width = doubleBufferDataGridView1.Width;
            height = doubleBufferDataGridView1.Height;
            textBox6.Text = "宽：" + doubleBufferDataGridView1.Width + "高：" + doubleBufferDataGridView1.Height;

            ThreadPool.SetMinThreads(5, 5); // 设置线程池最小线程数量为5
            ThreadPool.SetMaxThreads(50, 50); // 设置线程池最大线程数量为20

            if (!serialPort1.IsOpen)
            {
                serialPort1.Open();
            }

            DateTime currentDt = DateTime.Now;
            //Parallel.For(0, LogCount, e =>
            //{
            //    //WriteLogTime("1");
            //    WriteLog();
            //});

            TimeSpan Span = DateTime.Now - currentDt;

            Console.WriteLine(string.Format("\r\nLog Count:{0}.\t\tWrited Count:{1}.\tFailed Count:{2}. {3}", LogCount.ToString(), WritedCount.ToString(), FailedCount.ToString(), Span.TotalMilliseconds.ToString()));
            Console.Read();


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //设点
        private void button1_Click(object sender, EventArgs e)
        {

        }

        public static object lockObject = new object();

        private void WriteLog()
        {
            lock (lockObject)
            {
                Console.WriteLine("Write Log start... ThreadID:{0}", Thread.CurrentThread.ManagedThreadId);

                string strLogPath = string.Format("D:\\Log.txt", DateTime.Now.ToString("yyyy-MM-dd"));

                if (!File.Exists(strLogPath))
                {
                    File.Create(strLogPath).Close();
                }

                FileStream fs = new FileStream(strLogPath, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

                sw.WriteLine(string.Format("tid:{0}  {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));

                sw.Close();
                fs.Close();

                Console.WriteLine("Write Log End   ThreadID:{0}", Thread.CurrentThread.ManagedThreadId);
                WritedCount++;
            }
        }


        //画图
        private void button2_Click(object sender, EventArgs e)
        {

            pictureBox1.Image = null;
            pictureBox1.Refresh();

            bmp = new Bitmap(pictureBox1.Width - 6, pictureBox1.Height - 6);
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            graphics = Graphics.FromImage(bmp);
            pictureBox1.Focus();

            graphics.Clear(Color.White);

            iX = int.Parse(textBox1.Text);
            iY = int.Parse(textBox2.Text);

            fX = (float)(bmp.Width) / (iY + 1);
            fY = (float)(bmp.Height) / (iX + 1);

            fSize = fX > fY ? fY : fX;

            pictureBox1.Image = bmp;
            fPen = 2;
            //创建绘图对象Graphics

            Pen pen = new Pen(Color.Gray, fPen);

            Font drawFont = new Font("Arial", fSize - 6);

            SolidBrush drawBrush = new SolidBrush(Color.Black);


            //竖线
            for (int i = 0; i <= iY + 1; i++)
            {

                // Create point for upper-left corner of drawing.



                if (i == 0)
                {
                }
                else
                {
                    if (i <= iY)
                    {


                        graphics.DrawString((iY - i + 1).ToString(), drawFont, drawBrush, fSize * i, 0);

                    }
                    graphics.DrawLine(pen, fSize * i, fSize - (fPen / 2), fSize * i, fSize * (iX + 1) + (fPen / 2));
                }
            }

            //横线
            for (int j = 0; j <= iX + 1; j++)
            {

                // Create point for upper-left corner of drawing.



                if (j == 0)
                {

                }
                else
                {
                    if (j <= iX)
                    {
                        graphics.DrawString(j.ToString(), drawFont, drawBrush, 0, fSize * j);
                    }
                    graphics.DrawLine(pen, fSize + fPen / 2, fSize * j, fSize * (iY + 1), fSize * j);
                }
            }


            //显示行列数



            //graphics.DrawRectangle(pen, 50, 50, x*padding, y*padding);
            pictureBox1.Refresh();

            InitialGrid(iX, iY);

            //count = listBox1.Items.Count;

            //count++;

            //listBox1.Items.Add(count);

            //listBox1.SelectedIndex = listBox1.Items.Count - 1;
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

            if (aX <= fSize * (iY + 1) && aX >= fSize && aY <= fSize * (iX + 1) && aY >= fSize)
            {



                Brush brush = new SolidBrush(Color.Green);
                graphics.FillRectangle(brush, (int)((e.X / fSize)) * fSize + (fPen / 2), (int)(e.Y / fSize) * fSize + (fPen / 2), fSize - fPen, fSize - fPen);
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
            if (aY >= 0 && aX >= 0)
            {
                textBox5.Text = "X轴：" + aX + "Y轴：" + aY + "宽度：" + this.doubleBufferDataGridView1.Columns[aY].Width + "高度：" + this.doubleBufferDataGridView1.Rows[aX].Height;
            }
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


            var bw = new BackgroundWorker(); //创建对象
            bw.RunWorkerAsync(); //开始异步操作，可传递一个object参数

            bw.DoWork += (obj, ee) =>
            {
                OnTimedEvent(obj, ee);
            };
            //动态绑

            sTimer = new System.Windows.Forms.Timer();
            sTimer.Tick += new EventHandler(OnTimedEvent);
            sTimer.Interval = 100;
            sTimer.Start();

        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            int a2, b2, iRow, iCol, iResult;

            //graphics.Clear(Color.White);
            System.Threading.Thread.Sleep(10);

            Random ro = new Random(10);
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));

            int R = ran.Next(255);
            int G = ran.Next(255);
            int B = ran.Next(255);
            B = (R + G > 400) ? R + G - 400 : B;
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
            doubleBufferDataGridView1.Rows[a2].Cells[b2].Style.BackColor = Color.Red;
            doubleBufferDataGridView1.Rows[a2].Cells[b2].Selected = true;

            doubleBufferDataGridView1.CurrentCell = doubleBufferDataGridView1.Rows[a2].Cells[a2];




            //textBox5.Text = "X轴：" + b2 + "Y轴：" + a2;
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(R, G, B)), b * fX + (fPen / 2), a * fY + (fPen / 2), fX - fPen, fY - fPen);
            pictureBox1.Refresh();
            b = b + 1;
            if (b > iY)
            {
                b = 0;
                a = a + 1;
            }
            if (a > iX)
            {
                a = 0;
                b = 0;
                //sTimer.Stop();
                //button2_Click(sender, e);
                //aphics.Clear(Color.White);
            }
            test = test + 1;

            ThreadPool.QueueUserWorkItem(o => WriteLogTime("X轴位置:" + a + " Y轴位置:" + b + "X轴位置:" + a + " Y轴位置:" + b + "X轴位置:" + a + " Y轴位置:" + b + "--" + test + "--" + "\r\n"));


            //ThreadPool.QueueUserWorkItem(new WaitCallback(WriteLogTime), "X轴位置:" + a + " Y轴位置:" + b + "X轴位置:" + a + " Y轴位置:" + b + "X轴位置:" + a + " Y轴位置:" + b + "--" + test + "--" + "\r\n");

            //ParameterizedThreadStart method2 = o => WriteLogTime("X轴位置:" + a + " Y轴位置:" + b + "X轴位置:" + a + " Y轴位置:" + b + "X轴位置:" + a + " Y轴位置:" + b + "--" + test + "--" + "\r\n");
            //Thread thread2 = new Thread(method2);
            //thread2.Start();

            //解决中途重启问题


            iRow = a;
            iCol = b;
            iResult = a;

            //实时显示行列数
            txtX.Text = iRow.ToString();
            txtY.Text = iCol.ToString();
            txtColor.BackColor = iResult == 0 ? Color.Yellow : Color.Red;

            //回复,考虑可以不回复
            byte[] aaa = new byte[] { 0 };





            //OraDbHelper OraHelp = new OraDbHelper(null);
            //OraHelp.ConnectionString = FAQASDBConnectionString;
            //string sSql = " SELECT 'A' FROM DUAL";
            //DataTable dt = OraHelp.ExecuteDataTable(sSql);
            //dt.Dispose();





            //WriteLogTime("X轴位置:" + a + " Y轴位置:" + b + "X轴位置:" + a + " Y轴位置:" + b + "X轴位置:" + a + " Y轴位置:" + b +"--"+ test + "--"+ "\r\n");





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


        private void WriteLogTime(Object n)
        {
            try
            {


                LogFile.WriteLogTime(n.ToString());
                Application.DoEvents();
                WritedCount++;
            }
            catch (Exception ex)
            {
                FailedCount++;
            }
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

            OracleToXML();
            //将XML导入到Oracle
            //OracleXML("ASM001");

            //从Oracle取出XML



            //int k = 0;
            //int kk = ++k + k++ + ++k + k + k++;
            //textBox4.Text = kk.ToString();

            //ConvertDataSetToXMLFile(doubleBufferDataGridView1);
            //Random randomX = new Random();
            //Random randomY = new Random();
            //int aX = randomX.Next(0,iX);
            //int aY = randomY.Next(0, iY);
            //doubleBufferDataGridView1.Rows[aX].Cells[aY].Style.BackColor = Color.Red;

        }

        public string ConvertXmlToString(XmlDocument xmlDoc)
        {
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            writer.Formatting = Formatting.Indented; xmlDoc.Save(writer);
            StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            string xmlString = sr.ReadToEnd();
            sr.Close();
            stream.Close();
            return xmlString;
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
                string sFileName = listBox1.SelectedItem.ToString();

                dt.WriteXml("D:/" + sFileName + ".xml");
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

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int index = listBox1.IndexFromPoint(e.X, e.Y);
            listBox1.SelectedIndex = index;
            if (index >= 0)
            {
                ReadXML(listBox1.SelectedItem.ToString());
            }

        }

        private void ReadXML(string sName)
        {
            try
            {
                string sFilePath = "";
                //判读是否有StripID
                if (string.IsNullOrEmpty(sName))
                {

                    return;
                }
                else
                {
                    sFilePath = "D:/" + sName + ".xml";
                }

                //判断文件是否存在

                if (System.IO.File.Exists(sFilePath))
                {

                }
                else
                {

                    return;
                }

                XmlDocument xe = new XmlDocument();
                xe.Load(sFilePath);//加载XML文件
                XmlElement root = xe.DocumentElement;

                //初始化
                InitialGrid(root.ChildNodes.Count, (root.ChildNodes[0].ChildNodes.Count));

                int i = 0, j = 0;

                foreach (XmlNode item in root.ChildNodes)
                {
                    foreach (XmlNode Nodes in item.ChildNodes)
                    {
                        doubleBufferDataGridView1.ClearSelection();
                        doubleBufferDataGridView1.Rows[i].Cells[j].Style.BackColor = ColorTranslator.FromHtml(Nodes.InnerText == "0" ? "White" : Nodes.InnerText);
                        j++;
                    }
                    j = 0;
                    i++;
                }

                doubleBufferDataGridView1.ClearSelection();

            }
            catch (Exception ex)
            {
            }
        }

        private void ReadXML1(string sName)
        {
            try
            {
                sName = "ASM001";
                string sFilePath = "";
                string GoodBin = "0000";
                int start = 0, i = 0;


                //判读是否有StripID
                if (string.IsNullOrEmpty(sName))
                {
                    return;
                }
                else
                {
                    sFilePath = "D:/" + sName + ".xml";
                }


                //判断文件是否存在
                if (System.IO.File.Exists(sFilePath))
                {

                }
                else
                {
                    return;
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(sFilePath);//加载XML文件
                xmlDoc.LoadXml("");
                XmlElement root = xmlDoc.DocumentElement;

                XmlNode Layouts;
                string iblock, iX = "", iY = "";
                Layouts = xmlDoc["MapData"]["Layouts"];
                foreach (XmlNode Layout in Layouts)
                {
                    switch (Layout.Attributes["LayoutId"].InnerText)
                    {
                        case "StripMap":
                        case "SubMatrix":
                            iblock = Layout["Dimension"].Attributes["X"].InnerText;
                            break;
                        case "DieMatrix":
                        case "Device":
                            iX = Layout["Dimension"].Attributes["X"].InnerText;
                            iY = Layout["Dimension"].Attributes["Y"].InnerText;
                            break;
                        default:
                            break;
                    }
                }


                //初始化
                InitialGrid(int.Parse(iY), int.Parse(iX));


                XmlNode BinCodeMap = xmlDoc["MapData"]["SubstrateMaps"]["SubstrateMap"];
                //XmlNodeList Bincodes;
                int BinCodeMap_Count = BinCodeMap.ChildNodes.Count;
                //分為兩個格式去抓(還沒確定)
                if (BinCodeMap_Count == 1)
                {
                }
                else
                {
                    foreach (XmlNode tmp in xmlDoc.GetElementsByTagName("Overlay"))
                    {
                        if (tmp.Attributes["MapName"].InnerText == "BinCodeMap")
                        {
                            BinCodeMap = tmp["BinCodeMap"];

                            i = 0;

                            foreach (XmlNode tmp2 in BinCodeMap)
                            {

                                if (tmp2.Name == "BinCode")
                                {

                                    start = 0;
                                    for (int j = 0; j < tmp2.InnerText.Length / 4; j++) // 
                                    {

                                        doubleBufferDataGridView1.Rows[i].Cells[j].Style.BackColor = ColorTranslator.FromHtml(tmp2.InnerText.Substring(start, 4) == GoodBin ? "Green" : "Red");

                                        start = start + 4;
                                    }
                                    i++;
                                }
                            }
                        }
                    }
                }

                doubleBufferDataGridView1.ClearSelection();

            }
            catch (Exception ex)
            {
            }
        }


        private void OracleXML(string sName,string sID="")
        {
            try
            {
                string sFilePath = "";
                //判读是否有StripID
                if (string.IsNullOrEmpty(sName))
                {

                    return;
                }
                else
                {
                    sFilePath = "D:/Map/" + sName + ".xml";
                }

                //判断文件是否存在

                if (System.IO.File.Exists(sFilePath))
                {

                }
                else
                {

                    return;
                }

                XmlDocument xe = new XmlDocument();
                xe.Load(sFilePath);//加载XML文件

                OracleConnection conn = new OracleConnection("Data Source = KS_PRD_AY; User Id = FA; Password = FA");

                conn.Open();
                OracleXmlType cxml = new OracleXmlType(conn, xe);
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO MARKOUT_STRIPMAP VALUES ('"+ sID + "','" + sName + "',:pb)";
                cmd.Parameters.Add("pb", OracleDbType.XmlType, 1).Value = cxml;
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }


        private void OracleToXML()
        {
            try
            {
                string sFilePath = "D:";

                OracleConnection conn = new OracleConnection("Data Source = KS_QAS_AY; User Id = fa; Password = fa");

                conn.Open();
                string SQL = "SELECT KEYVALUE, XMLCOLUMN FROM  XMLCONTENT WHERE KEYVALUE='15'";


                using (OracleCommand cmd = new OracleCommand(SQL, conn))
                {

                    using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        //while (reader.Read())
                        //{
                        //    //读取 XML 类型
                        //    string sName= reader["KEYVALUE"].ToString();

                        //    string XML = reader["XMLCOLUMN"].ToString();

                        //    StringReader Reader = new StringReader(XML);

                        //    XmlDocument xmlDoc = new XmlDocument();

                        //    xmlDoc.Load(Reader);

                        //    xmlDoc.Save(sFilePath + "\\" + sName + ".xml");

                        //}

                    }
                }
                conn.Close();

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }

        private void InitialGrid(int iRow, int iColumn)
        {
            int iWidth = 0, iHeight = 0;
            if (iRow <= 0 && iColumn <= 0)
            {
                iRow = 5;
                iColumn = 5;
            }

            //清空
            doubleBufferDataGridView1.Rows.Clear();
            doubleBufferDataGridView1.Columns.Clear();
            doubleBufferDataGridView1.Visible = true;
            doubleBufferDataGridView1.ReadOnly = true;
            doubleBufferDataGridView1.AllowUserToAddRows = false;
            doubleBufferDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            doubleBufferDataGridView1.AllowUserToResizeRows = false;
            doubleBufferDataGridView1.AllowUserToResizeColumns = false;
            doubleBufferDataGridView1.RowCount = iRow;
            doubleBufferDataGridView1.ColumnCount = iColumn;



            //自动调整单元格宽度
            int width1 = doubleBufferDataGridView1.Width / iColumn;
            int height1 = doubleBufferDataGridView1.Height / iRow;

            int iSize = width1 > height1 ? height1 : width1;

            if (iSize < 28)
            {
                iSize = 28;
            }

            for (int i = 0; i < iColumn; i++)
            {
                doubleBufferDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                doubleBufferDataGridView1.Columns[i].Width = iSize;
                if ((iColumn - i) % 5 == 0)
                {
                    doubleBufferDataGridView1.Columns[i].HeaderCell.Value = (iColumn - i).ToString();
                }

                doubleBufferDataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            for (int j = 0; j < iRow; j++)
            {

                doubleBufferDataGridView1.Rows[j].Height = iSize;
                doubleBufferDataGridView1.Rows[j].HeaderCell.Value = (j + 1).ToString();
            }
            doubleBufferDataGridView1.RowHeadersWidth = 50;
            //doubleBufferDataGridView1.RowHeadersWidth = iSize+12;
            //doubleBufferDataGridView1.ColumnHeadersHeight = iSize+5;

            //doubleBufferDataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 6, FontStyle.Regular);

            iWidth = doubleBufferDataGridView1.Columns[0].Width * (iColumn) + 28;
            iHeight = doubleBufferDataGridView1.Rows[0].Height * (iRow) + 20;

            //doubleBufferDataGridView1.Width = iWidth ;

            //doubleBufferDataGridView1.Height = iHeight ;

            textBox6.Text = "宽：" + doubleBufferDataGridView1.Width + "高：" + doubleBufferDataGridView1.Height;
            doubleBufferDataGridView1.ClearSelection();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
            //asfc.Initialize(this);

            this.WindowState = FormWindowState.Maximized;

            foreach (string fname in System.IO.Directory.GetFiles("D:\\", "*.xml*"))   // read all xml
            {

                string A = Path.GetFileNameWithoutExtension(fname);
                listBox1.Items.Add(A);
            }
            listBox1.SelectedIndex = listBox1.Items.Count - 1;

            width = doubleBufferDataGridView1.Width;
            height = doubleBufferDataGridView1.Height;
            textBox6.Text = "宽：" + doubleBufferDataGridView1.Width + "高：" + doubleBufferDataGridView1.Height;
            AllocConsole();

            //System.Threading.Timer RunTimer = new System.Threading.Timer(new TimerCallback(Print1), null, 0, 1000);

        }


        private void Print1(object o)
        {
            Console.WriteLine("每秒执行一次的定时任务,当前线程Id:{0}", Thread.CurrentThread.ManagedThreadId);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < doubleBufferDataGridView1.ColumnCount; i++)
            {
                //doubleBufferDataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                int widthCol = doubleBufferDataGridView1.Columns[i].Width;
                doubleBufferDataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                doubleBufferDataGridView1.Columns[i].Width = widthCol + 5;
            }
            for (int j = 0; j < doubleBufferDataGridView1.RowCount; j++)
            {
                //doubleBufferDataGridView1.Rows[0].a = DataGridViewAutoSizeColumnMode.DisplayedCells;
                int heightRow = doubleBufferDataGridView1.Rows[j].Height;
                //doubleBufferDataGridView1.Rows[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                doubleBufferDataGridView1.Rows[j].Height = heightRow + 5;
            }

            int aaaa = doubleBufferDataGridView1.Columns[0].Width * (doubleBufferDataGridView1.ColumnCount) + 22;
            int bbbb = doubleBufferDataGridView1.Rows[0].Height * (doubleBufferDataGridView1.RowCount) + 12;

            doubleBufferDataGridView1.Width = aaaa >= width ? width : aaaa;

            doubleBufferDataGridView1.Height = bbbb >= height ? height : bbbb;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            int width1 = doubleBufferDataGridView1.Columns[0].Width;

            int height1 = doubleBufferDataGridView1.Rows[0].Height;

            if (width1 <= 10 || height1 <= 10)
            {
                return;
            }

            for (int i = 0; i < doubleBufferDataGridView1.ColumnCount; i++)
            {
                //doubleBufferDataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                int widthCol = doubleBufferDataGridView1.Columns[i].Width;
                doubleBufferDataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                doubleBufferDataGridView1.Columns[i].Width = widthCol - 5;
            }
            for (int j = 0; j < doubleBufferDataGridView1.RowCount; j++)
            {
                //doubleBufferDataGridView1.Rows[0].a = DataGridViewAutoSizeColumnMode.DisplayedCells;
                int heightRow = doubleBufferDataGridView1.Rows[j].Height;
                //doubleBufferDataGridView1.Rows[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                doubleBufferDataGridView1.Rows[j].Height = heightRow - 5;
            }

            int aaaa = doubleBufferDataGridView1.Columns[0].Width * (doubleBufferDataGridView1.ColumnCount) + 22;
            int bbbb = doubleBufferDataGridView1.Rows[0].Height * (doubleBufferDataGridView1.RowCount) + 12;

            doubleBufferDataGridView1.Width = aaaa >= width ? width : aaaa;

            doubleBufferDataGridView1.Height = bbbb >= height ? height : bbbb;
        }



        #region WinForm 禁止最大化、最小化、双击标题栏、双击图标等操作
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x112)
            {
                switch ((int)m.WParam)
                {
                    //禁止双击标题栏关闭窗体
                    //case 0xF063:
                    //case 0xF093:
                    //    m.WParam = IntPtr.Zero;
                    //    break;

                    //禁止拖拽标题栏还原窗体
                    case 0xF012:
                    case 0xF010:
                        m.WParam = IntPtr.Zero;
                        break;

                    //禁止双击标题栏
                    case 0xf122:
                        m.WParam = IntPtr.Zero;
                        break;

                        //禁止关闭按钮
                        //case 0xF060:
                        //    m.WParam = IntPtr.Zero;
                        //    break;

                        //禁止最大化按钮
                        //case 0xf020:
                        //    m.WParam = IntPtr.Zero;
                        //    break;

                        //禁止最小化按钮
                        //case 0xf030:
                        //    m.WParam = IntPtr.Zero;
                        //    break;

                        //禁止还原按钮
                        //case 0xf120:
                        //    m.WParam = IntPtr.Zero;
                        //    break;
                }
            }
            base.WndProc(ref m);
        }
        #endregion



        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //asfc.ReSize(this);
            asc.controlAutoSize(this);
            width = doubleBufferDataGridView1.Width;
            height = doubleBufferDataGridView1.Height;
            textBox6.Text = "宽：" + doubleBufferDataGridView1.Width + "高：" + doubleBufferDataGridView1.Height;
        }



        private void button7_Click(object sender, EventArgs e)
        {
            Process CurrentProcess = Process.GetCurrentProcess();
            CurrentProcess.WorkingSet64.ToString();
        }



        public void QUERY()
        {
            OracleConnection conn = new OracleConnection("Data Source=KS_QAS_AY;User ID=FWASSY;Password=FWASSY");

            conn.Open();
            string SQL = "SELECT 'A' FROM DUAL";


            using (OracleCommand cmd = new OracleCommand(SQL, conn))
            {

                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    label1.Text = dt.Rows[0][0].ToString();
                }
            }
            conn.Close();
        }

        private void doubleBufferDataGridView1_MouseHover(object sender, EventArgs e)
        {
            //int aX = this.doubleBufferDataGridView1.HitTest(e, e.Y).RowIndex; //行
            //int aY = this.doubleBufferDataGridView1.HitTest(e.X, e.Y).ColumnIndex; //列
            //if (aY >= 0 && aX >= 0)
            //{
            //    textBox5.Text = "X轴：" + aX + "Y轴：" + aY + "宽度：" + this.doubleBufferDataGridView1.Columns[aY].Width + "高度：" + this.doubleBufferDataGridView1.Rows[aX].Height;
            //}
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OraDbHelper a = new OraDbHelper(null);
            a.ConnectionString = "Data Source=KS_QAS_AY;User Id=FWASSY;Password=FWASSY;Connection Lifetime=120;";
            string sSql = "";
            sSql = " SELECT 'a' from dual ";

            DataTable dtStrip = a.ExecuteDataTable(sSql);
            a = new OraDbHelper(null);

            return;
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {

                int DataLength = serialPort1.BytesToRead;
                byte[] ds = new byte[DataLength];
                int bytecount = serialPort1.Read(ds, 0, DataLength);

                string SerialIn = System.Text.Encoding.ASCII.GetString(ds);
                //    String result = bytesToHexString(ds);


                this.BeginInvoke(new System.Threading.ThreadStart(delegate ()
                {
                    textBox7.Text = SerialIn;         //对控件进行赋值
                }));



                ////因为要访问UI资源，所以需要使用invoke方式同步ui
                //byte[] data = Convert.FromBase64String(serialPort1.ReadLine());
                //if (!string.IsNullOrEmpty(Encoding.Unicode.GetString(data)))
                //{
                //    textBox7.Text += Encoding.Unicode.GetString(data);
                //}
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }


        }




        String bytesToHexString(byte[] bArr)
        {
            string result = string.Empty;
            for (int i = 0; i < 13; i++)//逐字节变为16进制字符，以%隔开
            {
                result += Convert.ToString(bArr[i], 16).ToUpper().PadLeft(2, '0') + " ";
            }
            return result;
        }

        private void btnUploadXml_Click(object sender, EventArgs e)
        {
            OracleXML("B07787241", "A9869199.2D5454F1.E054001A.4B086AD0.00");
        }


        //private DateTime dt = DateTime.Now;
        //private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    DateTime dtTemp = DateTime.Now;        //保存按键按下时刻的时间点           

        //    TimeSpan ts = dtTemp.Subtract(dt);     //获取时间间隔           

        //    if (ts.Milliseconds > 50)              //判断时间间隔，如果时间间隔大于50毫秒，则将TextBox清空 
        //    {
        //        textBox1.Text = "";
        //    }

        //    dt = dtTemp;
        //}
    }
}
