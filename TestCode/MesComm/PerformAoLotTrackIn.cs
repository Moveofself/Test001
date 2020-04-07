using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMF.ATS.Base.ViewModels;
using CMF.ATS.DB.MessageProcessor;
using System.Windows;
using CMF.ATS.DB.MesComm.Inputs;
using MES.Proxy.MessageExchangeCenter;
using CMF.ATS.DB.MesComm;
using System.ServiceModel;
using CMF.ATSDB.Common;

namespace CMF.ATS.MesComm
{
    public class PerformAoLotTrackIn
    {
      //  static log4net.ILog Logger = log4net.LogManager.GetLogger("TrackSystem");

      //  public MesMessage CurrentMesMessage;
        public LogViewModel CurrentLogViewModel;
      // public Dictionary<String, String> CurrentEnLangResourceDictionary;

        public String EquipmentId = String.Empty;
        public String AoLotId;
        public String Step;

        public ExecuteResult Execute()
        {
            ExecuteResult result = new ExecuteResult();

            try
            {
                CurrentLogViewModel.AppendLineToUI(String.Format((String)Application.Current.FindResource("InfoMessage_TrackInToMes"), AoLotId), LogLevel.Info);

                TrackInInput trackInInput = new TrackInInput();

                trackInInput.EquipmentId = EquipmentId;
                trackInInput.LotId = AoLotId;
                trackInInput.MESCurrentStep = Step;

                EAPOutput output = null;



                output = MesMessage.Send<TrackInInput>(trackInInput);
   
                result.TransactionId = trackInInput.TransactionId;
                result.Data.Add("OutputMessage", output.OutputMessage);

                if (!output.ErrCode.Equals("0"))
                {
                    String errorMessage = String.Empty;

                    Object langResource = Application.Current.TryFindResource("Language");

                    if (langResource != null && langResource.ToString().Equals("zh-CN"))
                    {
                        errorMessage = String.Format((String)Application.Current.FindResource("WariningMessage_TrackInToMesReturnError"), output.ErrCode, output.CNErrMsg);
                    }
                    else
                    {
                        errorMessage = String.Format((String)Application.Current.FindResource("WariningMessage_TrackInToMesReturnError"), output.ErrCode, output.ENErrMsg);
                    }

                    CurrentLogViewModel.AppendLineToUI(errorMessage, LogLevel.Warn);

                    result.ErrorCode = output.ErrCode.ToString();
                    result.ErrorText = String.Format((String)CommonParameter.EnLangResourceDictionary["WariningMessage_TrackInToMesReturnError"], output.ErrCode, output.ENErrMsg);
                }
                else
                {
                    CurrentLogViewModel.AppendLineToUI((String)Application.Current.FindResource("InfoMessage_TrackInToMesSuccess"), LogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex);
                CurrentLogViewModel.AppendLineToUI(String.Format((String)Application.Current.FindResource("ErrorMessage_TrackInToMesUnknownError"), ex.Message), LogLevel.Error);
                result.ErrorCode = "2999";
                result.ErrorText = "[ATS] TrackIn to MES Unkown Error";
            }

            return result;
        }
    }
}
