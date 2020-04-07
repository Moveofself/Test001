using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using CMF.ATSDB.Common;

namespace CMF.ATS.DB.MesComm.Inputs
{
    [XmlRootAttribute("PerformControlLotTrackIn")]
    public class ControlLotTrackInInput : CommonInput
    {
        public String LotId;

        public String MESCurrentStep;

        public String GetInputMessage()
        {
            return XmlHelper.SerializeToXmlStr<ControlLotTrackInInput>(this, true);
        }

        public override String GetReplyRootName
        {
            get
            {
                return "PerformControlLotTrackInReply";
            }
        }
    }


    public class PerformMaterialCheck : CommonInput
    {
        public String GetInputMessage()
        {
            return XmlHelper.SerializeToXmlStr<PerformMaterialCheck>(this, true);
        }

        public override String GetReplyRootName
        {
            get
            {
                return "PerformMaterialCheckReply";
            }
        }
    }
}
