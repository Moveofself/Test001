using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using log4net;
using System.Threading;

namespace ESEC2008
{
    public class RS232Driver
    {

        protected object _SyncObj = new object();
        protected SerialPort _SerialPort = null;
        protected object _SyncReceiveMessageObj = new object();

        protected System.Timers.Timer _ReceiveTimer = null;
        protected Boolean _IsHandleCompleted = false;
        protected Boolean _IsTimeout = false;
        protected String _FullReplyMessage = String.Empty;


        private String _ReceiveMessageStartFlag = "#";

        public String ReceiveMessageStartFlag
        {
            get { return _ReceiveMessageStartFlag; }
            set { _ReceiveMessageStartFlag = value; }
        }

        private String _ReceiveMessageEndFlag = "$";

        public String ReceiveMessageEndFlag
        {
            get { return _ReceiveMessageEndFlag; }
            set { _ReceiveMessageEndFlag = value; }
        }

        public RS232Driver()
        {
            _SerialPort = new SerialPort();
            _SerialPort.Encoding = new ASCIIEncoding();

            _ReceiveTimer = new System.Timers.Timer();
            _ReceiveTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnWriteTimeout);
            _ReceiveTimer.AutoReset = false;
        }

        private string portName;
        private int baudRate;
        private Parity parity;
        private StopBits stopBits;
        private int dataBits;

        public virtual void Init(String rs232Config)
        {
            portName = rs232Config.Split(',')[0];
            baudRate = int.Parse(rs232Config.Split(',')[1]);
            dataBits = int.Parse(rs232Config.Split(',')[2]);
            parity = (Parity)Enum.Parse(typeof(Parity), rs232Config.Split(',')[3]);
            stopBits = (StopBits)Enum.Parse(typeof(StopBits), rs232Config.Split(',')[4]);
        }

        public virtual bool Start()
        {
            bool result = false;

            try
            {
                if (_SerialPort.IsOpen == true)
                {
                    _SerialPort.Close();
                }

                _SerialPort.BaudRate = baudRate;
                _SerialPort.DataBits = dataBits;
                _SerialPort.StopBits = stopBits;
                _SerialPort.Parity = parity;
                _SerialPort.PortName = portName;
                _SerialPort.Open();
                _SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
                result = true;
                Log.Logger.InfoFormat("Port opened at {0}", portName);
            }
            catch (Exception ex)
            {
                Log.Logger.ErrorFormat("{0}:{1}", _SerialPort.PortName, ex);
            }

            return result;
        }

        public virtual bool Stop()
        {
            bool result = false;

            try
            {
                if (_SerialPort.IsOpen == true)
                {
                    _SerialPort.Close();
                    result = true;
                }
                else
                {
                    result = true;
                }

                Log.Logger.InfoFormat("Close Port at {0}", portName);
            }
            catch (Exception ex)
            {
                Log.Logger.ErrorFormat("{0}:{1}", _SerialPort.PortName, ex);
            }

            return result;
        }

        public bool Write(String request)
        {
            bool result = false;

            lock (_SyncObj)
            {
                try
                {
                    if (_SerialPort.IsOpen == false)
                    {
                        Log.Logger.WarnFormat("Port: {0}  is closed. Try to open.", _SerialPort.PortName);
                        _SerialPort.Open();
                    }

                    try
                    {
                        Log.Logger.InfoFormat("{0}:Send Data-->{1}", _SerialPort.PortName, request);
                        _SerialPort.Write(request);

                        result = true;
                    }
                    catch (Exception ex)
                    {
                        Log.Logger.ErrorFormat("{0}:{1}", _SerialPort.PortName, ex);
                    }
                }
                catch (Exception ex)
                {
                    Log.Logger.ErrorFormat("{0}:{1}", _SerialPort.PortName, ex);
                }
            }

            return result;
        }

        public bool Write(String request, out String response)
        {
            lock (_SyncObj)
            {
                bool result = false;
                response = String.Empty;

                try
                {
                    if (_SerialPort.IsOpen == false)
                    {
                        Log.Logger.WarnFormat("Port: {0}  is closed. Try to open.", _SerialPort.PortName);
                        _SerialPort.Open();
                    }

                    _FullReplyMessage = String.Empty;
                    _IsHandleCompleted = false;

                    try
                    {
                        Log.Logger.InfoFormat("{0}:Send Data-->{1}", _SerialPort.PortName, request);
                        _SerialPort.Write(request);

                        result = true;
                    }
                    catch (Exception ex)
                    {
                        Log.Logger.ErrorFormat("{0}:{1}", _SerialPort.PortName, ex);
                        Log.Logger.InfoFormat("Port:{0}, will be reopen.", _SerialPort.PortName);

                        if (_SerialPort.IsOpen == true)
                        {
                            _SerialPort.Close();
                        }

                        _SerialPort.Open();
                        Log.Logger.InfoFormat("Port opened at {0}", _SerialPort.PortName);
                    }

                    _IsTimeout = false;
                    _ReceiveTimer.Start();

                    while (!_IsHandleCompleted)
                    {
                        if (_IsTimeout)
                        {
                            return result;
                        }
                        else
                        {
                            Thread.Sleep(1);
                        }
                    }

                    _ReceiveTimer.Stop();
                    response = String.Copy(_FullReplyMessage);
                    _FullReplyMessage = String.Empty;
                    _IsHandleCompleted = false;
                }
                catch (Exception ex)
                {
                    Log.Logger.ErrorFormat("{0}:{1}", _SerialPort.PortName, ex);
                }

                return result;
            }
        }

        protected virtual void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }

        private void OnWriteTimeout(object source, System.Timers.ElapsedEventArgs e)
        {
            _IsTimeout = true;
        }

    }

    interface IMessageProcessor
    {
        bool HandleMessage(String oneMessage);
    }
}
