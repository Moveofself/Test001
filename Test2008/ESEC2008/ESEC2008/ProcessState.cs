using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESEC2008
{
    public enum ProcessState
    {
        UNKNOWN = -1,
        INIT = 1,
        NOT_READY,
        DB_READY, //READY
        EXECUTING
    }
}
