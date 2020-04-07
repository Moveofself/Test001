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
using DBUnity.OraDbHelper;
using System.Xml.Linq;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;
using System.IO;
using System.Data.Odbc;
using System.Text.RegularExpressions;
using SevenZip;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using MES.Proxy.MessageExchangeCenter;

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
                txtShow.Text = Guid.NewGuid().ToString();

                //timer1.Interval = 60000;
                //timer1.Start();


                OraDbHelper _OraHelper = new OraDbHelper(null);

                _OraHelper.ConnectionString = "Data Source=KS_QAS_AY;Persist Security Info=True;User ID=FA;Password=FA;Unicode=True";

                string sSql = "";
                sSql = " SELECT STRIPID FROM FA.MARKOUT WHERE LOT=  'ENEB08N001.031' AND STEPNAME ='STD_CuMold_Step' ORDER BY CDT  ";

                DataTable SSS = _OraHelper.ExecuteDataTable(sSql);

                string a = ConvertDataTableToXML(SSS);

                DataTable aaa = ConvertXMLToDataTable(a);

                if (aaa.Rows.Count > 0)
                {
                    txtShow.Text = aaa.Rows[0][0].ToString();
                }
                else
                {
                    MessageBox.Show("没有资料");
                }


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


                //sss();

                //sss1();

                //txtShow.Text = "线程数：" + numThreads.ToString();

                ////Thread[] t = new Thread[numThreads];
                //for (int i = 0; i < numThreads; i++)
                //{
                //    GetDataFromDB();
                //    //t[i] = new Thread(new ThreadStart(GetDataFromDB));
                //    //t[i].Name = new String(Convert.ToChar(i + 65), 1);
                //    //t[i].Start();
                //    System.Threading.Thread.Sleep(100);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void GetDataFromDB()
        {
            DateTime NowDt = DateTime.Now;
            string sLog = "";
            sLog += "WebService Start:" + DateTime.Now.ToString() + "\r\n";
            WebReference.AutomationWebService FA = new WebReference.AutomationWebService();
            TimeSpan Span = DateTime.Now - NowDt;
            sLog += " Total Time:" + Span.TotalMilliseconds + "\r\n";

            string sSql = " SELECT 'A' FROM DUAL";
            string sProNo = FA.Perform_FA_QueryTableToString(sSql);
            Span = DateTime.Now - NowDt;

            sLog += "WebService End!Result:" + sProNo + DateTime.Now.ToString() + " Total Time:" + Span.TotalMilliseconds + "\r\n";

            WriteLogTime(sLog);
        }

        #region  WriteLogTime 记录Log并显示,显示时间
        private void WriteLogTime(string sMsg)
        {
            LogFile.WriteLogTime(sMsg);
            Application.DoEvents();
        }
        #endregion



        #region  WriteLogTime 记录Log并显示,显示时间 ,For 线程池
        private void WriteLogTime(Object oMsg)
        {
            LogFile.WriteLog_Lock(oMsg.ToString());
            Application.DoEvents();
        }
        #endregion

        public void sss()
        {
            try
            {
                WebReference.AutomationWebService FA = new WebReference.AutomationWebService();

                string sSql = "";
                sSql = " INSERT INTO FA.JOBLOG_HIS (SP_NAME, TIMESTAMP, ERRORCODE, ERRORDESC, STATUS) ";
                sSql += " VALUES('OLP_STATUS_MAIL', '20190520 172605', 'sss',:pb, 'START');";


                WebReference.OracleParameter[] param = new WebReference.OracleParameter[1]
                 {
                   new WebReference.OracleParameter()
                };

                param[0].ParameterName = ":pb";
                param[0].OracleDbType = WebReference.OracleDbType.Varchar2;
                param[0].Value = "TEST";

                int I = FA.Perform_FA_ExecuteNonQuery(sSql, WebReference.CommandType.Text, param);



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        public void sss1()
        {
            try
            {
                OraDbHelper _OraHelper = new OraDbHelper(null);

                _OraHelper.ConnectionString = "Data Source=KS_PRD_AY;Persist Security Info=True;User ID=FA;Password=FA;Unicode=True";

                string sSql = "";
                sSql = " INSERT INTO FA.JOBLOG_HIS (SP_NAME, TIMESTAMP, ERRORCODE, ERRORDESC, STATUS) ";
                sSql += " VALUES('OLP_STATUS_MAIL', '20190520 172605', 'sss1',?pb, 'START');";


                System.Data.OracleClient.OracleParameter[] param = new System.Data.OracleClient.OracleParameter[]
                 {
                   new System.Data.OracleClient.OracleParameter("?pb",OracleDbType.Varchar2)
                };

                param[0].Value = "TEST";

                _OraHelper.ExecuteNonQuery(sSql, CommandType.Text, param);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetDataFromDB();
        }

        public static string ConvertDataTableToXML(DataTable dt)
        {
            return ConvertDataTableToXML(dt, string.Empty);
        }
        private static string ConvertDataTableToXML(DataTable dt, string aaa)
        {
            StringWriter sw = null;
            try
            {
                if (dt.TableName == string.Empty)
                    dt.TableName = "table1";
                sw = new StringWriter();
                dt.WriteXml(sw, XmlWriteMode.WriteSchema);
                return sw.ToString();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }

        public static DataTable ConvertXMLToDataTable(string xmlData)
        {
            TextReader sr = null;
            try
            {
                DataTable dt = new DataTable();
                sr = new StringReader(xmlData);
                dt.ReadXml(sr);
                return dt;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            FileStream fs = null;
            fs = new FileStream("D:\\CM700PP.txt", FileMode.OpenOrCreate, FileAccess.Read);
            FileStream fs1 = new FileStream("D:\\67CMXK00001-0-000NU(WM)DM(JH)", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader sr = null;
            StreamWriter sw = new StreamWriter(fs1);
            BinaryWriter bw = new BinaryWriter(fs1);
            try
            {
                String content = String.Empty;
                sr = new StreamReader(fs);
                string sss = sr.ReadToEnd().ToString();

                //string b=HexStringToASCII(sss);
                //string b = HexStringTo1(sss);
                byte[] temp = HexStringToBinary(sss);
                bw.Write(temp);
                fs1.Close();
                //解压缩
                //UnZip("D:\\BinFile.gzip", "D:\\Recipe", "");
                UnzipTgz("D:\\Recipe\\BinFile", @"D:\Recipe\EE");

                if (File.Exists("D:\\Recipe\\EE\\ppbody.csv"))
                {
                    FileStream fs2 = null;
                    fs2 = new FileStream("D:\\Recipe\\EE\\ppbody.csv", FileMode.OpenOrCreate, FileAccess.Read);
                    StreamReader sr1 = new StreamReader(fs2, System.Text.Encoding.Default);
                    string body = sr1.ReadToEnd();
                    string sd = "352387072";
                    Regex reg = new Regex(sd + @"\,.*");
                    Match match = reg.Match(body);

                    string[] tmpary = match.ToString().Trim().Split(',');
                    byte[] buff = new byte[tmpary.Length];
                    for (int i = 0; i < buff.Length; i++)
                    {
                        txtLog.Text += StringFormat(tmpary[i]) + "\r\n";
                    }
                    fs2.Close();
                }





                //UnZip(@"D:\Recipe\BinFile.tar", @"D:\Recipe", "");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
                if (fs1 != null)
                {
                    fs1.Close();
                }
            }

        }

        public static string HexStringTo1(string hexstring)
        {

            byte[] bt = HexStringToBinary(hexstring);
            string lin = "";
            string sTemp = "";
            for (int i = 0; i < bt.Length; i++)
            {
                sTemp += ((char)bt[i]).ToString();
            }
            return sTemp;
        }


        public static string HexStringToASCII(string hexstring)
        {
            byte[] bt = HexStringToBinary(hexstring);
            string lin = "";
            for (int i = 0; i < bt.Length; i++)
            {
                lin = lin + bt[i] + " ";
            }


            string[] ss = lin.Trim().Split(new char[] { ' ' });
            char[] c = new char[ss.Length];
            int a;
            for (int i = 0; i < c.Length; i++)
            {
                a = Convert.ToInt32(ss[i]);
                c[i] = Convert.ToChar(a);
            }

            string b = new string(c);
            return b;
        }

        public static byte[] HexStringToBinary(string hexstring)
        {

            string[] tmpary = hexstring.Trim().Split(' ');
            byte[] buff = new byte[tmpary.Length];
            for (int i = 0; i < buff.Length; i++)
            {
                buff[i] = Convert.ToByte(tmpary[i], 16);
            }
            return buff;
        }

        public static void InitMesMessage(bool IsDB01)
        {
            try
            {
                _MessageExchangeServiceClient = new MessageExchangeServiceClient("NetTcpBinding_IMessageExchangeService");
                EAPOutput output;

                LotQueryInput lotQueryInput = new LotQueryInput();

                EAPInput input = new EAPInput();
 

                output = _MessageExchangeServiceClient.EAPRequest(input);

 

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }

        }






        public static MessageExchangeServiceClient _MessageExchangeServiceClient { get; set; }
        private void button2_Click(object sender, EventArgs e)
        {
            InitMesMessage(true);
            //FileStream fs = null;
            //fs = new FileStream("D:\\CM700PP.txt", FileMode.OpenOrCreate, FileAccess.Read);
            //FileStream fs1 = new FileStream("D:\\Recipe\\BinFile", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //StreamReader sr = null;
            //StreamWriter sw = new StreamWriter(fs1);
            //BinaryWriter bw = new BinaryWriter(fs1);
            try
            {

                //String content = String.Empty;
                //sr = new StreamReader(fs);
                //string sss = sr.ReadToEnd().ToString();

                ////string b=HexStringToASCII(sss);
                ////string b = HexStringTo1(sss);
                //byte[] temp = HexStringToBinary(sss);
                //bw.Write(temp);
                //fs1.Close();
                //解压缩
                //UnZip("D:\\BinFile.gzip", "D:\\Recipe", "");
                //UnzipTgz("D:\\007332", @"D:\Recipe\007332");
                //txtLog.Text += "Sucess!";
            }
            catch (Exception ex)
            {
                txtLog.Text += ex.Message;
            }
            finally
            {
                //if (fs != null)
                //{
                //    fs.Close();
                //}
                //if (fs1 != null)
                //{
                //    fs1.Close();
                //}
            }

        }

        //解压缩
        /// <summary>
        /// 解压功能(解压压缩文件到指定目录) 
        /// </summary>
        /// <param name="fileToUnZip">待解压的文件</param>
        /// <param name="zipedFolder">指定解压目标目录</param>
        /// <param name="password">密码</param>
        /// <returns>解压结果</returns>
        public static void UnZip(string fileToUnZip, string zipedFolder, string password)
        {
            bool result = true;
            FileStream fs = null;
            ZipInputStream zipStream = null;
            ZipEntry ent = null;
            string fileName;

            if (!File.Exists(fileToUnZip))


                if (!Directory.Exists(zipedFolder))
                    Directory.CreateDirectory(zipedFolder);

            try
            {
                zipStream = new ZipInputStream(File.OpenRead(fileToUnZip));
                if (!string.IsNullOrEmpty(password)) zipStream.Password = password;
                while ((ent = zipStream.GetNextEntry()) != null)
                {
                    if (!string.IsNullOrEmpty(ent.Name))
                    {
                        fileName = Path.Combine(zipedFolder, ent.Name);
                        fileName = fileName.Replace('/', '\\');//change by Mr.HopeGi   

                        if (fileName.EndsWith("\\"))
                        {
                            Directory.CreateDirectory(fileName);
                            continue;
                        }

                        fs = File.Create(fileName);
                        int size = 2048;
                        byte[] data = new byte[size];
                        while (true)
                        {
                            size = fs.Read(data, 0, data.Length);
                            if (size > 0)
                                fs.Write(data, 0, data.Length);
                            else
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (zipStream != null)
                {
                    zipStream.Close();
                    zipStream.Dispose();
                }
                if (ent != null)
                {
                    ent = null;
                }
                GC.Collect();
                GC.Collect(1);
            }

        }

        public void ungzip(string path, string decomPath, bool overwrite)
        {
            //for overwriting purposes
            //if (!Directory.Exists(decomPath))
            // Directory.CreateDirectory(decomPath);

            //create our file streams
            GZipStream stream = new GZipStream(new FileStream(path, FileMode.Open, FileAccess.ReadWrite), System.IO.Compression.CompressionMode.Decompress);
            FileStream decompressedFile = new FileStream(decomPath, FileMode.OpenOrCreate, FileAccess.Write);
            //data represents a byte from the compressed file
            //it's set through each iteration of the while loop
            int data;
            while ((data = stream.ReadByte()) != -1) //iterates over the data of the compressed file and writes the decompressed data
            {
                decompressedFile.WriteByte((byte)data);
            }
            //close our file streams
            decompressedFile.Close();
            stream.Close();
        }

        /// <summary>
        /// 文件解压
        /// </summary>
        /// <param name="zipPath">压缩文件路径</param>
        /// <param name="goalFolder">解压到的目录</param>
        /// <returns></returns>
        public static bool UnzipTgz(string zipPath, string goalFolder)
        {
            Stream inStream = null;
            Stream gzipStream = null;
            TarArchive tarArchive = null;
            try
            {
                using (inStream = File.OpenRead(zipPath))
                {
                    using (gzipStream = new GZipInputStream(inStream))
                    {
                        tarArchive = TarArchive.CreateInputTarArchive(gzipStream);
                        tarArchive.ExtractContents(goalFolder);
                        tarArchive.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("压缩出错！");
                return false;
            }
            finally
            {
                if (null != tarArchive) tarArchive.Close();
                if (null != gzipStream) gzipStream.Close();
                if (null != inStream) inStream.Close();
            }
        }


        /// <summary>  
        /// tar包解压  
        /// </summary>  
        /// <param name="strFilePath">tar包路径</param>  
        /// <param name="strUnpackDir">解压到的目录</param>  
        /// <returns></returns>  
        public static bool UnpackTarFiles(string strFilePath, string strUnpackDir)
        {
            try
            {
                if (!File.Exists(strFilePath))
                {
                    return false;
                }

                strUnpackDir = strUnpackDir.Replace("/", "\\");
                if (!strUnpackDir.EndsWith("\\"))
                {
                    strUnpackDir += "\\";
                }

                if (!Directory.Exists(strUnpackDir))
                {
                    Directory.CreateDirectory(strUnpackDir);
                }

                FileStream fr = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                ICSharpCode.SharpZipLib.Tar.TarInputStream s = new ICSharpCode.SharpZipLib.Tar.TarInputStream(fr);
                ICSharpCode.SharpZipLib.Tar.TarEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    if (directoryName != String.Empty)
                        Directory.CreateDirectory(strUnpackDir + directoryName);

                    if (fileName != String.Empty)
                    {
                        FileStream streamWriter = File.Create(strUnpackDir + theEntry.Name);

                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }

                        streamWriter.Close();
                    }
                }
                s.Close();
                fr.Close();

                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public static string StringFormat(string sInput)
        {
            try
            {
                if (!sInput.Contains("."))
                {
                    return sInput;
                }
                //判断最后一位是否为. 或0
                string sTemp = sInput.Substring(sInput.Length - 1, 1);
                if (sTemp=="0" || sTemp == ".")
                {
                    return StringFormat(sInput.Substring(0, sInput.Length - 1));
                }
                else
                {
                    return sInput;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        private void btnWCF_Click(object sender, EventArgs e)
        {
            EAPOutput output = new EAPOutput();
            string EquipmentId = "11";
            string StripId = "I01855086";
            string WaferId = "11";
            string AoLotId = "WinForm";
            string Source = "AutoTrackInOut";

            LotQueryInput lotQueryInput = new LotQueryInput();

            lotQueryInput.EquipmentId = EquipmentId;
            lotQueryInput.StripId = StripId;
            lotQueryInput.WaferId = WaferId;
            lotQueryInput.LotId = AoLotId;
            lotQueryInput.Source = Source;

            MesMessage.InitMesMessage(true);

            output = MesMessage.TransferData<LotQueryInput>(lotQueryInput);

            MessageBox.Show(output.ErrCode + output.ENErrMsg);

            //output.OutputMessage.ToString();

        }
    }
}
