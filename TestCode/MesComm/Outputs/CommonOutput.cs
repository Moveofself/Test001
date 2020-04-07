using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using CMF.ATSDB.Common;

namespace CMF.ATS.DB.MesComm.Outputs
{
    public class CommonOutput
    {
        public String TransactionId;

        public String EquipmentId;

        public static T GetOutputObject<T>(String outputMessage) where T : CommonOutput
        {
            return XmlHelper.XmlDeserialize<T>(outputMessage);
        }
    }

    public class Alarm
    {
        public String AlarmCode;

        public String CnAlarmMessage;

        public String EnAlarmMessage;
    }
}
