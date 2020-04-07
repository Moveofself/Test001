using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TestForm
{
    [XmlRootAttribute("PerformLotQueryReply")]
    public class LotQueryOutput : CommonOutput
    {
        public String LotId;

        public String MESCurrentStep;

        public String MESRecipeName;

        public String StripRows;

        public String StripColumns;

        public String StripCount;

        public String StripIdSeparator;

        public String StripIdList;

        public String ProcessedStripIdList;

        public String Block;

        public String IsAlreadyTrackIn;

        public String IsCheckMESRecipe;

        public String OutputMagazineId;

        public String DefaultDefectCode;

        public String AlreadyTrackOutErrorCode;

        public String SubLotWaferIdQty;

        public int DieQty=1;

        public List<DefectCodeEqpBinCodeMapping> DefectCodeEqpBinCodeMappings = new List<DefectCodeEqpBinCodeMapping>();
               
    }

    public class DefectCodeEqpBinCodeMapping
    {
        public String DefectCode;

        public String EqpBinCode;
    }

 

    public class PerformQueryStripInfo : CommonInput
    {

    

        public string StripId
        {
            get;
            set;
        }
        public string LotId
        {
            get;
            set;
        }
        public override string GetReplyRootName
        {
            get
            {
                return "PerformQueryStripInfoReply";
            }
        }

    }

}
