using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;

namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void btnClick_Click(object sender, EventArgs e)
        {
            try
            {
                txtShow.Text = "";
                ConfigurationManager.RefreshSection("appSettings");
                txtShow.Text = ConfigurationManager.AppSettings["Test"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {


                ConfigurationManager.RefreshSection("appSettings");
                int numThreads = int.Parse(ConfigurationManager.AppSettings["Number"]);

                txtShow.Text = "线程数：" + numThreads.ToString();

                //Thread[] t = new Thread[numThreads];
                for (int i = 0; i < numThreads; i++)
                {
                    GetDataFromDB();
                    //t[i] = new Thread(new ThreadStart(GetDataFromDB));
                    //t[i].Name = new String(Convert.ToChar(i + 65), 1);
                    //t[i].Start();
                    System.Threading.Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void GetDataFromDB()
        {
            DateTime NowDt = DateTime.Now;

            txtLog.Text += "WebService Start:" + DateTime.Now.ToString() + "\r\n";
            WebReference.AutomationWebService FA = new WebReference.AutomationWebService();
            TimeSpan Span = DateTime.Now - NowDt;
            txtLog.Text += " Total Time:" + Span.TotalMilliseconds + "\r\n";

            string sSql = " SELECT 'A' FROM DUAL";
            string sProNo = FA.Perform_FA_QueryTableToString(sSql);
            Span = DateTime.Now - NowDt;

            txtLog.Text += "WebService End!Result:" + sProNo + DateTime.Now.ToString() + " Total Time:" + Span.TotalMilliseconds + "\r\n";
        }


    }
}
