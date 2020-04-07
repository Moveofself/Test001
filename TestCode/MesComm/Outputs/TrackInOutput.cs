using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CMF.ATS.DB.MesComm.Outputs
{

    [XmlRootAttribute("PerformTrackInReply")]
    public class TrackInOutput : CommonOutput
    {
        public List<Alarm> Alarms = new List<Alarm>();
        public List<Material> Materials { get; set; }
    }
    

    public class Material
    {
        public string Type { get; set; }
        public string PartNo { get; set; }
        public string MaterialLot { get; set; }
        public string ExpireDate { get; set; }

    }

               
    public class PerformMaterialCheckReply : CommonOutput
    {
        
        public List<Material> Materials { get; set; }
                        
    }

}
