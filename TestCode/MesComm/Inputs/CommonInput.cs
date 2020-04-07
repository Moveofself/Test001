using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using CMF.ATSDB.Common;

namespace CMF.ATS.DB.MesComm.Inputs
{
    public class CommonInput
    {
        private String transactionId;

        public String TransactionId
        {
            get
            {
                if(String.IsNullOrEmpty(transactionId))
                {
                    transactionId = String.Format("{0}.{1}.{2}", GetMachineName(), EquipmentId, DateTime.Now.ToString("yyyyMMddHHmmssfffff"));
                }

                return transactionId;
            }
            set
            {
                transactionId = value;
            }
        }

        public String EquipmentId;

        private String userId = "Normal";

        public String UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }

        /// <summary>
        /// 获取计算机名
        /// </summary>
        /// <returns>计算机名</returns>
        public string GetMachineName()
        {
            try
            {
                return System.Environment.MachineName;
            }
            catch
            {
                return "UnknownComputerName";
            }
        }

        public String GetInputMessage<T>(CommonInput t) where T : CommonInput
        {
            return XmlHelper.SerializeToXmlStr<T>((T)t, true);
        }

        public virtual String GetReplyRootName
        {
            get { return ""; }
        }
    }
}
