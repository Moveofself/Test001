using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MES.Proxy.MessageExchangeCenter;
using System.Xml;
using CMF.ATS.DB.MesComm.Inputs;
using System.IO;
using System.Xml.Linq;
using CMF.ATSDB.Common;
using System.Windows;
using System.ServiceModel;
using System.ServiceModel.Channels;



namespace CMF.ATS.DB.MesComm
{
    public class MesMessage
    {
        private static String EndpointConfigurationNameForDB01 = "NetTcpBinding_IMessageExchangeServiceForDB01";
       
        private  static readonly object SyncObj = new object();

        private static String EndpointConfigurationNameForDB02 = "NetTcpBinding_IMessageExchangeServiceForDB02";

        /// <summary>
        /// DB01->true;DB02->false
        /// </summary>
        /// <param name="IsDB01"></param>
        public static void InitMesMessage(bool IsDB01)
        {
            try
            {
                if (IsDB01)
                {
                   //var  binding=new NetTcpBinding();
                   // binding.OpenTimeout=new TimeSpan(0,0,15);
                   // binding.CloseTimeout=new TimeSpan(0,0,15);
                   //  binding.ReceiveTimeout=new TimeSpan(0,0,15);
                   //  binding.OpenTimeout=new TimeSpan(0,0,15);
                   // binding.TransactionFlow=false;
                   // binding.TransferMode=TransferMode.Streamed;

                   //binding.ReaderQuotas =new XmlDictionaryReaderQuotas();
                   // binding.ReaderQuotas.MaxDepth=536870912;
                   // binding.ReaderQuotas.MaxStringContentLength=2147483647 ;
                   // binding.ReaderQuotas.MaxArrayLength=2147483647 ;
                   // binding.ReaderQuotas.MaxBytesPerRead=536870912 ;
                   // binding.ReaderQuotas.MaxNameTableCharCount=2147483647;
                   // binding.ReliableSession=new OptionalReliableSession();            
                   // binding.ReliableSession.Enabled=false;
                   // binding.Security=new NetTcpSecurity();
                   // binding.Security.Mode=SecurityMode.None;
                    

                    _MessageExchangeServiceClient = new MessageExchangeServiceClient(EndpointConfigurationNameForDB01);
                  //  _MessageExchangeServiceClient = new MessageExchangeServiceClient(binding, new EndpointAddress(@"net.tcp://10.65.4.118:8899/MessageExchangeCenter/MessageExchangeService"));

                }
                else
                {
                    _MessageExchangeServiceClient = new MessageExchangeServiceClient(EndpointConfigurationNameForDB02);

                }
            }
            catch (Exception e)
            {
                LogShown.RecordLog(string.Format("连接MES服务器初始化异常;{0}",e), LogLevel.Warn);
            }
           
        }

        public static MessageExchangeServiceClient _MessageExchangeServiceClient { get; set; }
        
        public static EAPOutput Send<T>(CommonInput inputObj) where T : CommonInput
        {
            EAPOutput output = new EAPOutput();

            try
            {

                lock (SyncObj)
                {

                    EAPInput input = new EAPInput();

                    input.InputMessage = inputObj.GetInputMessage<T>((T)inputObj);

                    Log.Logger.Info(String.Format("[ATS Request]:{0}", input.InputMessage));

                    output = _MessageExchangeServiceClient.EAPRequest(input);
                    XDocument xdoc;
                    try
                    {
                        Log.Logger.DebugFormat("[MES Reply]::ErrorCode: {0}, CnErrorText: {1}, EnErrorText: {2}", output.ErrCode, output.CNErrMsg, output.ENErrMsg);

                        if (String.IsNullOrEmpty(output.OutputMessage)) throw new MessageIsEmptyException();

                         xdoc = XDocument.Parse(output.OutputMessage);
                        var formattedXml = (xdoc.Declaration != null ? xdoc.Declaration + "\r\n" : "") + xdoc.ToString();
                        Log.Logger.InfoFormat("[MES Reply]:Message:{0}", formattedXml.ToString());
                    }
                    catch (Exception e)
                    {
                        Log.Logger.DebugFormat("[MES Reply]: OutputMessage: {0}",  output.OutputMessage);

                        throw e;
                    }

                    String equipmentId = xdoc.Root.Element("EquipmentId").Value;
                    String transactionId = xdoc.Root.Element("TransactionId").Value;

                    if (xdoc.Root.Name.Equals(inputObj.GetReplyRootName)) throw new MesReplyMisMatchException();
                    if (!inputObj.EquipmentId.Equals(equipmentId)) throw new EquipmentIdMisMatchException();
                    if (!inputObj.TransactionId.Equals(transactionId)) throw new TransactionIdMisMatchException();

                    return output;
                }
            }
            catch (MessageIsEmptyException ex)
            {
                Log.Logger.Error(ex.Message);
                LogShown.RecordLog((String)Application.Current.FindResource("WariningMessage_TrackInToMesOutputMessageIsEmpty"), LogLevel.Warn);
                output.ErrCode = "2000";
                output.ENErrMsg = (String)CommonParameter.EnLangResourceDictionary["WariningMessage_TrackInToMesOutputMessageIsEmpty"];
            }
            catch (EquipmentIdMisMatchException ex)
            {
                Log.Logger.Error(ex.Message);
                LogShown.RecordLog((String)Application.Current.FindResource("WariningMessage_TrackInToMesEquipmentIdMisMatch"), LogLevel.Warn);
                output.ErrCode = "2001";
                output.ENErrMsg = (String)CommonParameter.EnLangResourceDictionary["WariningMessage_TrackInToMesEquipmentIdMisMatch"];
            }
            catch (TransactionIdMisMatchException ex)
            {
                Log.Logger.Error(ex.Message);
                LogShown.RecordLog((String)Application.Current.FindResource("WariningMessage_TrackInToMesTransactionIdMisMatch"), LogLevel.Warn);
                output.ErrCode = "2002";
                output.ENErrMsg = (String)CommonParameter.EnLangResourceDictionary["WariningMessage_TrackInToMesTransactionIdMisMatch"];
            }
            catch (EndpointNotFoundException ex)
            {
                Log.Logger.Error(ex.Message);
                LogShown.RecordLog((String)Application.Current.FindResource("WariningMessage_TrackInToMesEndpointNotFound"), LogLevel.Warn);
                output.ErrCode = "2003";
                output.ENErrMsg = (String)CommonParameter.EnLangResourceDictionary["WariningMessage_TrackInToMesEndpointNotFound"];
            }
            catch (TimeoutException ex)
            {
                Log.Logger.Error(ex.Message);
                LogShown.RecordLog((String)Application.Current.FindResource("WariningMessage_TrackInToMesTimeout"), LogLevel.Warn);
                output.ErrCode = "2004";
                output.ENErrMsg = (String)CommonParameter.EnLangResourceDictionary["WariningMessage_TrackInToMesTimeout"];
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex);
                LogShown.RecordLog(String.Format((String)Application.Current.FindResource("ErrorMessage_TrackInToMesUnknownError"), ex.Message), LogLevel.Error);
                output.ErrCode = "2999";
                output.ENErrMsg = "[ATS] TrackIn to MES Unkown Error";
            }
            return output;
        }

        public static EAPOutput TransferData<T>(CommonInput inputObj) where T : CommonInput
        {
             EAPOutput output =new EAPOutput();
            try
            {
                lock (SyncObj)
                {
                    EAPInput input = new EAPInput();

                    input.InputMessage = inputObj.GetInputMessage<T>((T)inputObj);

                    Log.Logger.InfoFormat("[ATS Request]: {0}", input.InputMessage);

                    output = _MessageExchangeServiceClient.TransferData(input);
                    XDocument xdoc;
                    try
                    {
                        Log.Logger.InfoFormat("[MES Reply]:ErrorCode: {0}, CnErrorText: {1}, EnErrorText: {2}", output.ErrCode, output.CNErrMsg, output.ENErrMsg);

                         xdoc = XDocument.Parse(output.OutputMessage);
                        var formattedXml = (xdoc.Declaration != null ? xdoc.Declaration + "\r\n" : "") + xdoc.ToString();

                        Log.Logger.InfoFormat("[MES Reply]:Message:{0}", formattedXml.ToString());
                    }
                    catch (Exception e)
                    {
                        Log.Logger.InfoFormat("[MES Reply]: OutputMessage: {0}",  output.OutputMessage);
                        throw e;
                     
                    }
                    String equipmentId = xdoc.Root.Element("EquipmentId").Value;
                    String transactionId = xdoc.Root.Element("TransactionId").Value;

                    if (xdoc.Root.Name.Equals(inputObj.GetReplyRootName)) throw new MesReplyMisMatchException();

                    if (!inputObj.EquipmentId.Equals(equipmentId)) throw new EquipmentIdMisMatchException();
                    if (!inputObj.TransactionId.Equals(transactionId)) throw new TransactionIdMisMatchException();

                    return output;
                }
            }
            catch (MessageIsEmptyException ex)
            {
                Log.Logger.Error(ex.Message);
                LogShown.RecordLog((String)Application.Current.FindResource("WariningMessage_TrackInToMesOutputMessageIsEmpty"), LogLevel.Warn);
                output.ErrCode = "2000";
                output.ENErrMsg = (String)CommonParameter.EnLangResourceDictionary["WariningMessage_TrackInToMesOutputMessageIsEmpty"];
            }
            catch (EquipmentIdMisMatchException ex)
            {
                Log.Logger.Error(ex.Message);
                LogShown.RecordLog((String)Application.Current.FindResource("WariningMessage_TrackInToMesEquipmentIdMisMatch"), LogLevel.Warn);
                output.ErrCode = "2001";
                output.ENErrMsg = (String)CommonParameter.EnLangResourceDictionary["WariningMessage_TrackInToMesEquipmentIdMisMatch"];
            }
            catch (TransactionIdMisMatchException ex)
            {
                Log.Logger.Error(ex.Message);
                LogShown.RecordLog((String)Application.Current.FindResource("WariningMessage_TrackInToMesTransactionIdMisMatch"), LogLevel.Warn);
                output.ErrCode = "2002";
                output.ENErrMsg = (String)CommonParameter.EnLangResourceDictionary["WariningMessage_TrackInToMesTransactionIdMisMatch"];
            }
            catch (EndpointNotFoundException ex)
            {
                Log.Logger.Error(ex.Message);
                LogShown.RecordLog((String)Application.Current.FindResource("WariningMessage_TrackInToMesEndpointNotFound"), LogLevel.Warn);
                output.ErrCode = "2003";
                output.ENErrMsg = (String)CommonParameter.EnLangResourceDictionary["WariningMessage_TrackInToMesEndpointNotFound"];
            }
            catch (TimeoutException ex)
            {
                Log.Logger.Error(ex.Message);
                LogShown.RecordLog((String)Application.Current.FindResource("WariningMessage_TrackInToMesTimeout"), LogLevel.Warn);
                output.ErrCode = "2004";
                output.ENErrMsg = (String)CommonParameter.EnLangResourceDictionary["WariningMessage_TrackInToMesTimeout"];
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex);
                LogShown.RecordLog(String.Format((String)Application.Current.FindResource("ErrorMessage_TrackInToMesUnknownError"), ex.Message), LogLevel.Error);
                output.ErrCode = "2999";
                output.ENErrMsg = "[ATS] TrackIn to MES Unkown Error";
            }

            return output;
        }
  
    }

        

}
