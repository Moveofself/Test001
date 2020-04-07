using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestForm
{
    public class EquipmentIdMisMatchException : Exception
    {
        public EquipmentIdMisMatchException()
            : base()
        {

        }
    }

    public class TransactionIdMisMatchException : Exception
    {
        public TransactionIdMisMatchException()
            : base()
        {

        }
    }

    public class MesReplyMisMatchException : Exception
    {
        public MesReplyMisMatchException()
            : base()
        {

        }
    }
}
