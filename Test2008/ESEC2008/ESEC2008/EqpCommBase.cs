using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESEC2008
{
    public class EqpCommBase
    {
        
        public virtual bool ResetBoxWithReply(out String response)
        {
            response = "";
            return true;
        }

        public virtual string GetDevicesOnLeadframe()
        {
            return "";
        }

        public virtual String GetCurrentProcessWaferId()
        {
            return "";
        }

        public virtual void Stop()
        {
        }

        public virtual void ManualReadStripId()
        {
        }

        public virtual void LockBox()
        {

        }

        public virtual void Init()
        {

        }

        public virtual String ReadStripId()
        {
            return "";
        }

        public virtual void StopBox()
        {
        }
        public virtual void Start()
        {

        }

        public virtual bool ResetBox()
        {
            return true;
        }

       
    }
}
