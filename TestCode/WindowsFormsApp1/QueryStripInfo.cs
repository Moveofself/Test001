using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TestForm
{
    [XmlRootAttribute("PerformQueryStripInfo")]
    public class QueryStripInfo : CommonInput 
    {
        public String StripId;
        public String LotId;

        public String GetInputMessage()
        {
            return XmlHelper.SerializeToXmlStr<QueryStripInfo>(this, true);
        }

        public override String GetReplyRootName
        {
            get
            {
            return "PerformQueryStripInfoReply";
        }
        }
    }
}
