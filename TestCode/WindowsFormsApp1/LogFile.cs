using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows;
using System.Configuration;

/// <summary>   
/// 日志类   
/// </summary>   
namespace TestForm
{
    class LogFile
    {
        private static int iMaxSize = int.Parse(ConfigurationManager.AppSettings["LogHelpMaxSize"]);

        private static string appLogPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + ConfigurationManager.AppSettings["LogPath"];


        #region WriteLogTime
        /// <summary>
        /// 
        /// </summary>
        /// <param name="smessage"></param>
        public static void WriteLogTime(string smessage)
        {
            string filename = appLogPath + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            FileInfo fileinfo = new FileInfo(filename);
            try
            {
                if (!Directory.Exists(appLogPath))
                {
                    Directory.CreateDirectory(appLogPath);
                }

                if (!File.Exists(filename))
                {
                    using (File.Create(filename)) { }
                }

                if (fileinfo.Length > iMaxSize * 1024 * 1024)
                {
                    File.Move(filename, appLogPath + "\\" + string.Format("{0}.log", DateTime.Now.ToString("yyyyMMddHHmmss")));
                }

                File.AppendAllText(filename, "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "] " + smessage + "\r\n");

                //StringBuffer.Append
            }
            catch (Exception)
            {
            }
            finally
            {
                filename = string.Empty;
                fileinfo = null;
            }
        }
        #endregion



        #region WriteLog
        /// <summary>
        /// 
        /// </summary>
        /// <param name="smessage"></param>
        public static void WriteLog(string smessage)
        {
            string filename = appLogPath + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            FileInfo fileinfo = new FileInfo(filename);
            try
            {
                if (!Directory.Exists(appLogPath))
                {
                    Directory.CreateDirectory(appLogPath);
                }



                if (!File.Exists(filename))
                {
                    using (File.Create(filename)) { }
                }



                if (fileinfo.Length > iMaxSize * 1024 * 1024)
                {
                    File.Move(filename, appLogPath + "\\" + string.Format("{0}.log", DateTime.Now.ToString("yyyyMMddHHmmss")));
                }

                File.AppendAllText(filename, smessage);

            }
            catch (Exception)
            {

            }
            finally
            {
                filename = string.Empty;
                fileinfo = null;
            }
        }
        #endregion



        #region MyRegion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Log"></param>
        public static void Write(string Title, string Log)
        {
            try
            {
                //日志目录是否存在 不存在创建
                if (!Directory.Exists(appLogPath))
                {
                    Directory.CreateDirectory(appLogPath);
                }

                StringBuilder logInfo = new StringBuilder("");
                string currentTime = System.DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss.fff]");

                logInfo.Append(Title.ToString() + ":" + Log.ToString() + "\n");
                System.IO.File.AppendAllText(appLogPath + DateTime.Now.ToString("yyyyMMdd") + ".log", logInfo.ToString());
            }
            catch (Exception ex)
            {
                WriteLogTime("Error: " + ex.Message.ToString());
            }
        }
        #endregion



        #region DeleteFile 删除指定路径下，超出指定时间以外的文件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="saveDay"></param>
        public static void DeleteFile(string filePath, int saveDay)
        {
            try
            {
                filePath = string.IsNullOrEmpty(filePath) ? appLogPath : filePath;
                DateTime dtNow = DateTime.Now;
                DirectoryInfo root = new DirectoryInfo(filePath);
                FileInfo[] dics = root.GetFiles();//获取文件夹

                FileAttributes attr = File.GetAttributes(filePath);
                if (attr == FileAttributes.Directory)//判断是不是文件夹
                {
                    foreach (FileInfo file in dics)//遍历文件夹
                    {
                        TimeSpan t = dtNow - file.CreationTime;  //当前时间  减去 文件创建时间
                        int day = t.Days;
                        if (day > saveDay)   //保存的时间 ；  单位：天
                        {
                            File.Delete(file.FullName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogTime("DeleteFile Error:" + ex.Message.ToString());
                throw ex;
            }
        }
        #endregion



        #region WriteLog  Only one thread can execute at the same time
        public static object lockObject = new object();


        public static void WriteLog_Lock(string sMessage)
        {
            try
            {

                lock (lockObject)
                {

                    string strLogPath = appLogPath + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

                    if (!Directory.Exists(appLogPath))
                    {
                        Directory.CreateDirectory(appLogPath);
                    }

                    if (!File.Exists(strLogPath))
                    {
                        File.Create(strLogPath).Close();
                    }

                    FileStream fs = new FileStream(strLogPath, FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

                    if (fs.Length > iMaxSize * 1024 * 1024)
                    {
                        File.Move(strLogPath, appLogPath + "\\" + string.Format("{0}.log", DateTime.Now.ToString("yyyyMMddHHmmss")));
                    }

                    sw.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "] " + sMessage + "\r\n");

                    sw.Close();
                    fs.Close();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion


    }
}
