using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using CMF.ATSDB.Common;

namespace CMF.ATS.DB.MesComm.Inputs
{
    [XmlRootAttribute("PerformControlLotQuery")]
    public class ControlLotQueryInput : CommonInput 
    {
        public String WaferId;
        public String LotId;
        public String Source = "AutoTrackInOut";

        public String GetInputMessage()
        {
            return XmlHelper.SerializeToXmlStr<ControlLotQueryInput>(this, true);
        }

        public override String GetReplyRootName
        {
            get
            {
                return "PerformControlLotQueryReply";
            }
        }
    }
}
