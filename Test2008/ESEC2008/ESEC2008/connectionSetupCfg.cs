using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESEC2008
{

    public static class connectionSetupCfg
    {
        public static readonly int SVID_WaferID = 124409;
        public static readonly int SVID_WaferIDExtension = 128496;

        public static readonly int SVID_DevicesOnLeadframe = 159103;
        public static string Esec2008BarCodeScanerRs232;
        public static string Esec2008BoxRs232;
        internal static bool ESEC2008DirectionForASEN;

        public static bool DebugMode { get;   set; }
    }

}
