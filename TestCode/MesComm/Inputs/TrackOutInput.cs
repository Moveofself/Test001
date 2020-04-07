using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using CMF.ATSDB.Common;

namespace CMF.ATS.DB.MesComm.Inputs
{
    [XmlRootAttribute("PerformTrackOut")]
    public class TrackOutInput : CommonInput
    {
        public String LotId;

        public String MESCurrentStep;

        public String RemainLot = "False";

        public String StripIdSeparator;

        public String StripIdList;

        public String GetInputMessage()
        {
            return XmlHelper.SerializeToXmlStr<TrackOutInput>(this, true);
        }

        public override String GetReplyRootName
        {
            get
            {
            return "PerformTrackOutReply";
        }
        }
    }


    

}
