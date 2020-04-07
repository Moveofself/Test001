using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CMF.ATS.DB.MesComm.Outputs
{
    [XmlRootAttribute("PerformControlLotTrackInReply")]
    public class ControlLotTrackInOutput : CommonOutput
    {
        public List<Alarm> Alarms = new List<Alarm>();

        public Material Materials { get; set; }
    }


}
