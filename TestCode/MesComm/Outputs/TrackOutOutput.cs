using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CMF.ATS.DB.MesComm.Outputs
{
    [XmlRootAttribute("PerformTrackOutReply")]
    public class TrackOutOutput : CommonOutput
    {
        public String LotId;

        public int LotQty;

        public String StripCount;

        public String StripIdSeparator;

        public String ProcessedStripIdList;

        public int GoodDieQty;
    }


    public class PerformQueryStripInfoReply
    {
        public string TransactionId
        {
            get;
            set;
        }
        public string LotId
        {
            get;
            set;
        }
        public string EquipmentId
        {
            get;
            set;
        }
        public string WaferLot
        {
            get;
            set;
        }
        public string StripId
        {
            get;
            set;
        }
        public string GoodQty
        {
            get;
            set;
        }
        public string BadQty
        {
            get;
            set;
        }
        public string StripRows
        {
            get;
            set;
        }
        public string StripColumns
        {
            get;
            set;
        }
        public string StripBlock
        {
            get;
            set;
        }
        public string PitchMove
        {
            get;
            set;
        }
        public string PackageGroup
        {
            get;
            set;
        }
        public string StripBinCodeMap
        {
            get;
            set;
        }
        public string StripBinCodeSeparator
        {
            get;
            set;
        }
        public string MapVersion
        {
            get;
            set;
        }
    }

}
