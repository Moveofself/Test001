using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace ESEC2008
{
   public class BarCodeScanerRs232Driver : RS232Driver, IbarCodeReader
    {
        public BarCodeScanerRs232Driver(string recieveFlag)
            : base()
        {
            _ReceiveTimer.Interval = 20 * 1000;
            ReceiveMessageEndFlag = recieveFlag;
        }

         void IbarCodeReader.Start()
        {
            base.Start();
        }

      
        protected override void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            lock (_SyncReceiveMessageObj)
            {
                SerialPort sp = sender as SerialPort;
                String incomingData = sp.ReadExisting();
                _FullReplyMessage += incomingData;

                if (_FullReplyMessage.Contains(ReceiveMessageEndFlag))
                {
                    Log.Logger.InfoFormat("{0}:{1}",_SerialPort.PortName,_FullReplyMessage);

                    _IsHandleCompleted = true;
                    _ReceiveTimer.Stop();
                }
                else
                {
                    Log.Logger.InfoFormat("{0}:{1}", _SerialPort.PortName, _FullReplyMessage);
                }
            }
        }

        void IbarCodeReader.Close()
        {
            base.Stop();
        }

      string IbarCodeReader.ReadStripAndGetStripId()
        {
            String stripId = String.Empty;

            try
            {

                if (Write("+", out stripId))
                {
                    Write("-");
                }
                else
                {
                    Write("-");
                }


                //if (Write("LON" + "\r", out stripId))
                //{
                //    Write("LOFF\r");
                //}
                //else
                //{
                //    Write("LOFF\r");
                //}

            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex);
            }

            stripId = stripId.Replace("\r", "").Replace("\n", "");

            if (!string.IsNullOrEmpty(stripId))
            {
                stripId = stripId.Trim().ToUpper();
            }

            return stripId;

        }

        public void Initial(string Config)
        {
            base.Init(Config);
        }
    }
}
