using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CMF.ATS.DB.MesComm.Outputs
{
    [XmlRootAttribute("PerformControlLotQueryReply")]
    public class ControlLotQueryOutput : CommonOutput
    {
        public String LotId;

        public int LotQty;

        public String MESCurrentStep;

        public String MESRecipeName;

        public String StripRows;

        public String StripColumns;

        public String IsAlreadyTrackIn;

        public String IsCheckMESRecipe;

        public String Block;

        public String StripIdSeparator;

        public String OutputMagazineId;

        public String DefaultDefectCode;

        public String AlreadyTrackOutErrorCode;

        public String MappingFlag;

        public int DieQty=1;

        public List<AoLotInfo> AoLotInfos = new List<AoLotInfo>();

        public List<WaferInfo> WaferInfos = new List<WaferInfo>();

        public List<DefectCodeEqpBinCodeMapping> DefectCodeEqpBinCodeMappings = new List<DefectCodeEqpBinCodeMapping>();
    }

    public class AoLotInfo
    {
        public String LotId;
        public int LotQty;
        public String StripCount;
        public String ProcessedStripIdList;
        public int GoodDieQty;
    }

    public class WaferInfo
    {
        public String WaferLot;
        public String WaferId;
        public String PickUpQty;
        public String UsedQty;
    }

}
