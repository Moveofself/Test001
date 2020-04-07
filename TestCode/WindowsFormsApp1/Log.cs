using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TestForm
{
    public class Log
    {
        public static log4net.ILog Logger = log4net.LogManager.GetLogger("TrackSystem");
        public static log4net.ILog LoggerCOM = log4net.LogManager.GetLogger("Rs232System");
    }


    public class LogMessageEventArgs : EventArgs
    {
        public string message;

        public LogLevel level;
    }
        

      public class LogShown
    {

        private static LogShown instance;

        private static object lockHelper = new object();



        /// <summary>
        /// 单件模式，防止多线程-同步实例化
        /// </summary>
        /// <returns></returns>
        public static LogShown InstanceSingleTon()
        {
            if (instance == null)
            {
                lock (lockHelper)
                {
                    if (instance == null)
                    {
                        instance = new LogShown();
                    }
                }
            }
            return instance;
        }


        public static event EventHandler<LogMessageEventArgs> LogMessageEvent;


        public static void RecordLog(string message, LogLevel level)
        {
            InstanceSingleTon();
            if (LogMessageEvent != null)
                LogMessageEvent(null, new LogMessageEventArgs() { message = message, level = level });
        }
        public static void RecordLogFormat(LogLevel level, params string[] nums)
        {
            InstanceSingleTon();

            string message = string.Empty;

            
            message = string.Format("{0}", string.Join("", nums));
            if (LogMessageEvent != null)
                LogMessageEvent(null, new LogMessageEventArgs() { message = message, level = level });
        }

    }

}
