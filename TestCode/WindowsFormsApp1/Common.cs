using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestForm
{

    public enum LogLevel
    {
        Info,
        Warn,
        Error
    }

    public enum eqpState
    {
        Run,
        Idle,
        Alarm
    }


    /// <summary>
    /// 开始读码：start
    /// 比对成功：Processing
    /// 保存到MES：End
    /// </summary>
    public enum StripProcessStatus
    {
        Init,
        Start,
        Processing,
        End
    }



    public class StaticEvent
    {
        
    }




   


}
