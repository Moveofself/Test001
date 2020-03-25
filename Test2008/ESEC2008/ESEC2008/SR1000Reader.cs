using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ESEC2008
{
  public  class SR1000Reader:IbarCodeReader
    {
        private  string curStripId = string.Empty;
        public  string CurStripId
        {
            get { return curStripId; }
            set
            {
                curStripId = value;

            }
        }

       
        private  object lockHelper = new object();

        //public  TCPConnector tcpconnector;

        /// <summary>
        /// 连接重试次数
        /// </summary>
        public  int Retries { get; set; }


        /// <summary>
        /// 连接重试间隔
        /// </summary>
      //  public static int RetryInterval { get; set; }

        protected  System.Timers.Timer _ReceiveTimer = null;

        private  ManualResetEvent threadMre = new ManualResetEvent(true);


        /// <summary>
        /// mre.WaitOne(); will not be effective if only false
        /// mre.Reset():  true ---> false, mre.WaitOne() will waiting
        /// mre.Set(): false--->true, mre.WaitOne() will be skipped
        /// </summary>
        public  ManualResetEvent ThreadMre
        {
            get
            {
                return threadMre;
            }
            set
            {
                threadMre = value;
            }
        }


        public  void Clear()
        {
            CurStripId = string.Empty;
        }



        public  bool ReceiveStripIDInital(string ComSetting)
        {
            try
            {
                string ip;
                int port = 0;
                if (!Regex.IsMatch(ComSetting, ","))
                {
                    Log.Logger.Info("[ME]:SR1000连接设定异常，请设定[IP,port]!");
                    return false;
                }
                else
                {
                    ip = Regex.Split(ComSetting, ",")[0];
                    int.TryParse(Regex.Split(ComSetting, ",")[1], out port);

                    if (port == 0)
                    {
                        Log.Logger.Info("[ME]:SR1000连接端口设定异常!");
                        return false;
                    }
                }

                //tcpconnector = new TCPConnector(ip, port);
                //tcpconnector.DataReceivedEvent += new EventHandler<ReceivedTcpServerDataEventArgs>(tcpconnector_DataReceivedEvent);
                //tcpconnector.ConnectServerEvent += new EventHandler<ConnectTCPServerEventArgs>(tcpconnector_ConnectServerEvent);
                _ReceiveTimer = new System.Timers.Timer(3000);
                _ReceiveTimer.Elapsed += new System.Timers.ElapsedEventHandler(_ReceiveTimer_Elapsed);
                //RetryInterval = 5;

                Retries = 2;

                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.Info("OuputReader初始化失败，请检查配置!"+ex.Message);
                //LogShown.RecordLog("OuputReader初始化失败，请检查配置", LogLevel.Error);
            }
            return false;
        }

        //void tcpconnector_ConnectServerEvent(object sender, ConnectTCPServerEventArgs e)
        //{
        //    LogShown.RecordLog("Reader连接成功!", LogLevel.Error);
        //}

        void _ReceiveTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ThreadMre.Set();
        }



        //void tcpconnector_DataReceivedEvent(object sender, ReceivedTcpServerDataEventArgs e)
        //{
        //    try
        //    {
        //        CurStripId = e.ReceivedDataStr.Trim().ToUpper();
        //        ThreadMre.Set();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Logger.Error(ex);
        //    }
        //}


        /// <summary>
        /// 读取MagzineID，重试2次，如果没有读到返回空
        /// </summary>
        public  string ReadStripIdReader()
        {
            try
            {
                for (int i = 0; i < Retries; i++)
                {
                    ReadStripId();
                    if (!string.IsNullOrEmpty(CurStripId))
                    {

                        return CurStripId;
                    }
                    // Log.Logger.Debug(string.Format(CultureInfo.InvariantCulture, "Waiting {0} seconds before retrying to read Output Maganize ID.", RetryInterval));
                    // Thread.Sleep(TimeSpan.FromSeconds(RetryInterval));

                }


                return string.Empty;

            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex);
                return string.Empty;
            }
        }


        /// <summary>
        /// 发送读取命令给Reader，读取reader读到的字符串
        /// </summary>
        /// <returns></returns>
        private  string ReadStripId()
        {
            try
            {
                //if (tcpconnector.IsConnected)
                //{
                //    tcpconnector.SendMessageToTCPServer("LON\r");
                //    _ReceiveTimer.Start();
                //    ThreadMre.Reset();
                //    ThreadMre.WaitOne();
                //}
                //else
                //{
                //    LogShown.RecordLog("Reader未连接不能读码!", LogLevel.Error);
                //}
            }
            catch (Exception e)
            {
                Log.Logger.Error(e);

            }

            return null;
        }
        
      

        public  void Close()
        {
            try
            {
                //if (tcpconnector != null)
                //    tcpconnector.Dispose();
            }
            catch (Exception e)
            {
                Log.Logger.Error(e);
            }

        }

        public string ReadStripAndGetStripId()
        {
            ReadStripId();
            //LogShown.RecordLog("SR1000读取StripId:" + CurStripId, LogLevel.Info);
            return CurStripId;
        }

        public void Start()
        {
           
        }

        public void Initial(string Config)
        {
            try
            {

                string ip;
                int port = 0;
                if (!Regex.IsMatch(Config, ","))
                {
                    //LogShown.RecordLog("[ME]:SR1000连接设定异常，请设定[IP,port]", LogLevel.Error);
                    return ;
                }
                else
                {
                    ip = Regex.Split(Config, ",")[0];
                    int.TryParse(Regex.Split(Config, ",")[1], out port);

                    if (port == 0)
                    {
                        //LogShown.RecordLog("[ME]:SR1000连接端口设定异常", LogLevel.Error);
                        return ;
                    }
                }
                //tcpconnector = new TCPConnector(ip, port);
                //tcpconnector.DataReceivedEvent += new EventHandler<ReceivedTcpServerDataEventArgs>(tcpconnector_DataReceivedEvent);
                //tcpconnector.ConnectServerEvent += new EventHandler<ConnectTCPServerEventArgs>(tcpconnector_ConnectServerEvent);
                _ReceiveTimer = new System.Timers.Timer(3000);
                _ReceiveTimer.Elapsed += new System.Timers.ElapsedEventHandler(_ReceiveTimer_Elapsed);
                

                Retries = 2;

               
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex);
                //LogShown.RecordLog("OuputReader初始化失败，请检查配置", LogLevel.Error);
            }
        
        }
    }
}
