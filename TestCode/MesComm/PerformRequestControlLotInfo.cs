﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMF.ATS.DB.MessageProcessor;
using MES.Proxy.MessageExchangeCenter;
using CMF.ATS.DB.MesComm.Outputs;
using CMF.ATS.DB.MesComm.Inputs;
using System.Windows;
using CMF.ATS.Base.ViewModels;
using CMF.ATS.DB.MesComm;
using System.ServiceModel;
using CMF.ATSDB.Common;

namespace CMF.ATS.MesComm
{
    public class PerformRequestControlLotInfo
    {
        //static log4net.ILog Logger = log4net.LogManager.GetLogger("TrackSystem");

       // public MesMessage CurrentMesMessage;
        public LogViewModel CurrentLogViewModel;
       // public Dictionary<String, String> CurrentEnLangResourceDictionary;

        public String EquipmentId = String.Empty;
        public String WaferId = String.Empty;
        public String ControlLotId = String.Empty;
        public String Source = "AutoTrackInOut";

        
        public PerformRequestControlLotInfo( )
        {

        }

       

        public ExecuteResult Execute()
        {
            ExecuteResult result = new ExecuteResult();

            try
            {
                CurrentLogViewModel.AppendLineToUI(String.Format((String)Application.Current.FindResource("InfoMessage_LotQueryToMes"), WaferId), LogLevel.Info);

                EAPOutput output;

                ControlLotQueryInput lotQueryInput = new ControlLotQueryInput();

                lotQueryInput.EquipmentId = EquipmentId;
                lotQueryInput.WaferId = WaferId;
                lotQueryInput.LotId = ControlLotId;
                lotQueryInput.Source = Source;

                output = MesMessage.Send<ControlLotQueryInput>(lotQueryInput);

                result.TransactionId = lotQueryInput.TransactionId;

                if (!output.ErrCode.Equals("0"))
                {
                    String errorMessage = String.Empty;
                    Object langResource = Application.Current.TryFindResource("Language");

                    if (langResource != null && langResource.ToString().Equals("zh-CN"))
                    {
                        errorMessage = String.Format((String)Application.Current.FindResource("WariningMessage_LotQueryToMesReturnError"), output.ErrCode, output.CNErrMsg);
                    }
                    else
                    {
                        errorMessage = String.Format((String)Application.Current.FindResource("WariningMessage_LotQueryToMesReturnError"), output.ErrCode, output.ENErrMsg);
                    }

                    CurrentLogViewModel.AppendLineToUI(errorMessage, LogLevel.Warn);

                    result.ErrorCode = output.ErrCode.ToString();
                    result.ErrorText = String.Format((String)CommonParameter.EnLangResourceDictionary["WariningMessage_LotQueryToMesReturnError"], output.ErrCode, output.ENErrMsg);
                }
                else
                {
                    CurrentLogViewModel.AppendLineToUI((String)Application.Current.FindResource("InfoMessage_LotQueryToMesSuccess"), LogLevel.Info);
                    result.Data.Add("OutputMessage", output.OutputMessage);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex);
                CurrentLogViewModel.AppendLineToUI(String.Format((String)Application.Current.FindResource("ErrorMessage_LotQueryToMesUnknownError"), ex.Message), LogLevel.Error);
                result.ErrorCode = "1999";
                result.ErrorText = "[ATS] LotQuery to MES Unkown Error";
            }

            return result;
        }
    }
}
