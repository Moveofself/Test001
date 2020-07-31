using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;

namespace ESEC2008
{
    public class EqpCommForEsec2008 : EqpCommBase, IMessageProcessor
    {
        private BoxState boxState = BoxState.Reset;
        private Object handleMessageSyncObj = new object();
        private BoxRs232Driver boxRs232Driver;
        private IbarCodeReader barCodeScanerRs232Driver;

        //public override void SetEqpActionHandler(IEqpActionHandler eqpActionHandler)
        //{
        //    currentEqpActionHandler = eqpActionHandler;
        //}

        public override void Init()
        {
            base.Init();
            InitBoxComm();
            InitBarCodeScanerComm();
        }

        //protected override void HandleEventReport(SecsTransaction trans)
        //{
        //    Event eqEvent = ParseEvent<Event>(trans);


        //    if (eqEvent == Event.EXECUTING)
        //    {
        //        String waferId = GetCurrentProcessWaferId();
        //        //currentEqpActionHandler.HandleEqpMessage(waferId, LogLevel.Info, MessageType.WaferID);

        //    }
        //    else if (eqEvent == Event.ClearData)
        //    {
        //        currentEqpActionHandler.ClearAllData();
        //    }
        //}

        public override void Start()
        {
        
            base.Start();
            boxRs232Driver.MessageProcessor = this;
            boxRs232Driver.Start();
            barCodeScanerRs232Driver.Start();
        }


        public override void Stop()
        {
            base.Stop();

            boxRs232Driver.Stop();

            barCodeScanerRs232Driver.Close();
        }

        private void InitBoxComm()
        {
            try
            {
                boxRs232Driver = new BoxRs232Driver();

                boxRs232Driver.ReceiveMessageStartFlag = "#";
                boxRs232Driver.ReceiveMessageEndFlag = "$";

                boxRs232Driver.Init(connectionSetupCfg.Esec2008BoxRs232);
            }
            catch (Exception ex)
            {
                Log.Logger.Error("InitBoxComm error, details: " + ex);
            }
        }

        /// <summary>
        /// Esec2008 donn't execute the stop Commonad
        /// </summary>
        /// <param name="StopAnyWay"></param>
        /// <returns></returns>
        //public override StopFlag StopEqp(bool StopAnyWay)
        //{
        //    return StopFlag.Success;
        //}


        private void InitBarCodeScanerComm()
        {
            try
            {

                barCodeScanerRs232Driver = new BarCodeScanerRs232Driver("\r");

                //barCodeScanerRs232Driver.ReceiveMessageEndFlag = "\r";

                barCodeScanerRs232Driver.Initial(connectionSetupCfg.Esec2008BarCodeScanerRs232);

            }
            catch (Exception ex)
            {
                Log.Logger.Error("InitBarCodeScanerComm error, details: " + ex);
            }
        }

        public override String ReadStripId()
        {

            String stripId = barCodeScanerRs232Driver.ReadStripAndGetStripId();
            return stripId;
        }

        private bool isManualReadingStripId = false;


        public override void ManualReadStripId()
        {
            if (!isManualReadingStripId)
            {
                isManualReadingStripId = true;

                Thread t = new Thread(() =>
                {
                    string stripId = barCodeScanerRs232Driver.ReadStripAndGetStripId();
                    //base.OnReadStripId(stripId);

                    isManualReadingStripId = false;
                });

                t.IsBackground = true;

                t.Start();
            }
        }

        //private void ReadStripIdAction()
        //{
        //    String stripId = String.Empty;

        //    try
        //    {
        //        if (barCodeScanerRs232Driver.Write("LON" + "\r", out stripId))
        //        {
        //            barCodeScanerRs232Driver.Write("LOFF\r");
        //        }
        //        else
        //        {
        //            barCodeScanerRs232Driver.Write("LOFF\r");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //    }

        //    stripId = stripId.Replace("\r", "").Replace("\n", "");

        //    if (!string.IsNullOrEmpty(stripId))
        //    {
        //        stripId = stripId.Trim().ToUpper();
        //    }

        //    OnReadStripId(stripId);

        //    isManualReadingStripId = false;
        //}

        public override String GetCurrentProcessWaferId()
        {
            String waferId = String.Empty;

            try
            {
                //waferId = base.S1F3(SVID.SVID_WaferID, SecsItem.ItemType.U4);

                if (waferId.Length > 16)
                {
                    //waferId = waferId + base.S1F3(SVID.SVID_WaferIDExtension, SecsItem.ItemType.U4);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex);
            }

            return waferId.Trim().ToUpper();
        }

        public override void StopBox()
        {
            try
            {
                if (connectionSetupCfg.DebugMode == true)
                {
                    boxRs232Driver.Write("stop\r");
                }
                else
                {
                    boxRs232Driver.Write("stop"); //20180408
                }

                boxState = BoxState.Stop;
                //currentEqpActionHandler.HandleEqpMessage("Stop Box成功", LogLevel.Info, MessageType.Other);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex);
            }
        }

        /// <summary>
        /// False-->Stop Machine
        /// </summary>
        public override bool ResetBox()
        {

            try
            {
                string respone = string.Empty;
                if (boxState != BoxState.Reset)
                {
                    if (connectionSetupCfg.DebugMode == true)
                    {
                        boxRs232Driver.Write("reset\r", out respone);
                    }
                    else
                    {
                        boxRs232Driver.Write("reset", out respone);
                    }


                    boxState = BoxState.Reset;
                    //currentEqpActionHandler.HandleEqpMessage("Reset Box成功", LogLevel.Info, MessageType.Other);
                }

                if (respone.Contains("reset-ok"))
                {
                    // isStopping = true;

                    return true;
                }


                return false;


            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex);
                return false;
            }
        }

        public override void LockBox()
        {
            try
            {
                if (connectionSetupCfg.DebugMode == true)
                {
                    boxRs232Driver.Write("lock\r"); //20180408
                }
                else
                {
                    boxRs232Driver.Write("lock"); //20180408
                }

                boxState = BoxState.Lock;
                //currentEqpActionHandler.HandleEqpMessage("Lock Box成功", LogLevel.Warn, MessageType.Other);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex);
            }
        }

        public override bool ResetBoxWithReply(out String response)
        {
            bool result = false;

            response = String.Empty;

            try
            {
                if (connectionSetupCfg.DebugMode == true)
                {
                    result = boxRs232Driver.Write("reset\r", out response);
                }
                else
                {
                    result = boxRs232Driver.Write("reset", out response);
                }

            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex);
            }

            return result;
        }

        public bool HandleMessage(String oneMessage)
        {
            lock (handleMessageSyncObj)
            {
                try
                {
                    //EqpActionHandleResult result = new EqpActionHandleResult();
                    bool handleResult = false;

                    if (oneMessage.Equals("#1000000$"))    //S1 Signal
                    {
                        StopBox();
                        String stripId = String.Empty;
                        ReadStripId();
                        //result = currentEqpActionHandler.HandleEsec2008StripIdRead(stripId);

                    }
                    else if (oneMessage.Equals("#0110000$")) //S2 Signal
                    {
                        //UP
                        if (connectionSetupCfg.ESEC2008DirectionForASEN)
                        {
                            //result = currentEqpActionHandler.HandleEsec2008ReportStripBondDriection(OriginLocation.LowerRight);

                        }
                        else
                        {
                            //result = currentEqpActionHandler.HandleEsec2008ReportStripBondDriection(OriginLocation.UpperRight);////上海厂盒子信号和苏州的相反
                        }
                    }
                    else if (oneMessage.Equals("#0100000$")) //S2 Signal
                    {   //Down
                        if (connectionSetupCfg.ESEC2008DirectionForASEN)
                        {
                            //result = currentEqpActionHandler.HandleEsec2008ReportStripBondDriection(OriginLocation.UpperRight);

                        }
                        else
                        {
                            //result = currentEqpActionHandler.HandleEsec2008ReportStripBondDriection(OriginLocation.LowerRight); //上海厂盒子信号和苏州的相反
                        }
                    }
                    else if (oneMessage.Equals("#0001000$")) //S3 Signal
                    {
                        //result = currentEqpActionHandler.HandleEsec2008StripOneUnitFinish(UnitGrade.GOOD);
                    }
                    else if (oneMessage.Equals("#0000100$"))  //S4 Signal
                    {
                        //result = currentEqpActionHandler.HandleEsec2008StripOneUnitFinish(UnitGrade.BAD);
                    }

                    //if (result.StopEqp == false && !result.ErrorCode.Equals("200") && isStopping == false && !result.ErrorCode.Equals("230"))
                    //{
                    //  if (boxState != BoxState.Reset)
                    //    {
                            ResetBox();
                    //    }

                    //}
                    //else
                    //{
                    //    if (boxState != BoxState.Lock)
                    //    {
                    //        LockBox();
                    //    }
                    //}                

                    return handleResult;
                }

                catch (Exception e)
                {
                    Log.Logger.Error(e);
                    return false;
                }
            }



        }

        public enum BoxState
        {
            Lock,
            Stop,
            Reset,
        }

        //public override string GetDevicesOnLeadframe()
        //{
        //    String devices = String.Empty;

        //    try
        //    {
        //        devices = base.S1F3(SVID.SVID_DevicesOnLeadframe, SecsItem.ItemType.U4);

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Logger.Error(ex);
        //    }

        //    return devices;
        //}

    }
}
