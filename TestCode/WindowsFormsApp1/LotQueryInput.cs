using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TestForm
{
    [XmlRootAttribute("PerformLotQuery")]
    public class LotQueryInput : CommonInput 
    {
        public String StripId;
        public String WaferId;
        public String LotId;
        public String Source = "AutoTrackInOut";

        public String GetInputMessage()
        {
            return XmlHelper.SerializeToXmlStr<LotQueryInput>(this, true);
        }

        public override String GetReplyRootName
        {
            get
            {
            return "PerformLotQueryReply";
        }
        }
    }
}
