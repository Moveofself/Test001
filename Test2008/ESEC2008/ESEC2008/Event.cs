using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESEC2008
{
    public enum Event
    {
        NOTREADY = 10,
        DB_READY = 11, //READY
        EXECUTING = 12,
        WAFERIDREAD = 114,
        WAFERLOADED = 38,
        WAFERUNLOADED = 39,
        EAPINIT = 100,
        ClearData=999,
       
    }
}
