using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TestForm
{
    [XmlRootAttribute("PerformRecordStripInfo")]
    public class RecordResultByStripInput: CommonInput
    {
        public String LotId;

        public String MESCurrentStep;

        public String OutputMagazineId;

        public String StripRows;

        public String StripColumns;

        public String StripId;

        public String BinCodeSeparator;

        public String StripBinCodeMap;

        public String StripEqpBinCodeMap;

        public String IgnoreStripEqpBinCode;

        public String FromWaferInfo;

        public String GetInputMessage()
        {
            return XmlHelper.SerializeToXmlStr<RecordResultByStripInput>(this, true);
        }

        public override String GetReplyRootName
        {
            get
            {
                return "PerformRecordStripInfoReply";
            }
        }
    }
}
