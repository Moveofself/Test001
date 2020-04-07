using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using CMF.ATSDB.Common;

namespace CMF.ATS.DB.MesComm.Inputs
{
    [XmlRootAttribute("PerformTrackIn")]
    public class TrackInInput : CommonInput
    {
        public String LotId;

        public String MESCurrentStep;

        public String GetInputMessage()
        {
            return XmlHelper.SerializeToXmlStr<TrackInInput>(this, true);
        }

        public override String GetReplyRootName
        {
            get
            {
                return "PerformTrackInReply";
            }
        }
    }




}
