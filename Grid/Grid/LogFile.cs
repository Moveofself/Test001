using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows;
using System.Configuration;

/// <summary>   
/// 日志类   
/// </summary>   
namespace Grid
{
    class LogFile
    {
 
        private static int iMaxSize = int.Parse(ConfigurationManager.AppSettings["LogHelpMaxSize"]);

        private static string appLogPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\" + ConfigurationManager.AppSettings["LogPath"];

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
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                filename = string.Empty;
                fileinfo = null;
            }
        }

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
                filename = null;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                filename = string.Empty;
                fileinfo = null;
            }
        }

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

    }
}
