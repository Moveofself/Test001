using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSMQTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //FormatName:DIRECT=ftp://222.10.xx.xx/msmq/Private$/msmqpath
            MSMQUtil.MSMQHelper msmqHelper = new MSMQUtil.MSMQHelper(@".\Private$\msmqpath");
            msmqHelper.CreateQueue("first msmq", "first lable");
            msmqHelper.CreateQueue(new msmqtestclass() { age = 25, name = "xieyang", contents = new List<string>() { "my leg", "my head" } });
            var formater1 = new Type[] { typeof(string) };
            object obj1 = msmqHelper.ReceiveOneQueue(formater1);

            var formater2 = new Type[] { typeof(msmqtestclass) };
            object obj2 = msmqHelper.ReceiveOneQueue(formater2);
        }
    }

    public class msmqtestclass
    {
        public msmqtestclass()
        {

        }
        public List<string> contents { get; set; } = new List<string>();
        public string name { get; set; }
        public int age { get; set; }
    }
}
