using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace ESEC2008
{
    class BoxRs232Driver : RS232Driver
    {
        private object _ReceiveMessageQueueSyncObj = new object();
        private Queue<String> _ReceiveMessageQueue = new Queue<string>();
        private String receiveStr = String.Empty;
        private Boolean _IsHandleMessage = true;

        private IMessageProcessor _MessageProcessor;

        internal IMessageProcessor MessageProcessor
        {
            get { return _MessageProcessor; }
            set { _MessageProcessor = value; }
        }


     
        public BoxRs232Driver()
            : base()
        {
            _ReceiveTimer.Interval = 2 * 1000;
        }

        public override bool Start()
        {
            bool result = false;

            result = base.Start();

            if (result)
            {
                _IsHandleMessage = true;
                _ReceiveMessageQueue.Clear();

                Thread handleMessageThread = new Thread(HandleMessageThread);
                handleMessageThread.IsBackground = true;
                handleMessageThread.Start();

                result = true;
            }

            return result;
        }

        public override bool Stop()
        {
            bool result = false;

            result = base.Stop();

            if (result)
            {
                _IsHandleMessage = false;
                _ReceiveMessageQueue.Clear();

                result = true;
            }

            return result;
        }

        private void HandleMessageThread()
        {
            while (_IsHandleMessage)
            {
                String oneMessage = GetOneMessage();

                if (!String.IsNullOrEmpty(oneMessage))
                {
                    Log.Logger.InfoFormat("{0}:Receive One Message:{1} ",_SerialPort.PortName, oneMessage);

                    if (MessageProcessor != null)
                    {
                        MessageProcessor.HandleMessage(oneMessage);
                    }
                }

                Thread.Sleep(10);
            }
        }

        private void AddOneMessage(String oneMessage)
        {
            lock (_ReceiveMessageQueueSyncObj)
            {
                _ReceiveMessageQueue.Enqueue(oneMessage);
            }
        }

        private String GetOneMessage()
        {
            String result = String.Empty;

            lock (_ReceiveMessageQueueSyncObj)
            {
                if (_ReceiveMessageQueue.Count > 0)
                {
                    result = _ReceiveMessageQueue.Dequeue();
                }
            }

            return result;
        }

        protected override void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            lock (_SyncReceiveMessageObj)
            {
                SerialPort sp = sender as SerialPort;
                String incomingData = sp.ReadExisting();

                receiveStr += incomingData;

                Log.Logger.DebugFormat("{0}->{1}", sp.PortName, incomingData);

                if (receiveStr.Contains("reset-ok"))
                {
                    _FullReplyMessage = "reset-ok";
                    _IsHandleCompleted = true;
                    receiveStr = receiveStr.Replace("reset-ok", String.Empty);
                    Log.Logger.InfoFormat("{0}->{1}", sp.PortName, incomingData);
                }

                while (!String.IsNullOrEmpty(receiveStr))
                {
                    int startIndex = receiveStr.IndexOf(ReceiveMessageStartFlag);
                    int endIndex = receiveStr.IndexOf(ReceiveMessageEndFlag);

                    if (startIndex != -1 && endIndex != -1)
                    {
                        String oneMessage = receiveStr.Substring(startIndex, endIndex - startIndex + 1);
                        AddOneMessage(oneMessage);
                        receiveStr = receiveStr.Substring(endIndex + 1, receiveStr.Length - endIndex - 1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
   
    }
}
