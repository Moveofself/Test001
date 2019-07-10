using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string sInput;
            long input;
            while (true)
            {
                sInput = "";
                input = 0;
                Console.Write("请输入16进制字符串：");
                sInput = Console.ReadLine();
               
                try
                {
                    input = Convert.ToInt64(sInput, 16);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("转换为10进制结果为：" + input);
                Console.WriteLine("按任意键继续！");
                Console.ReadKey();
            }
            
        }
    }
}
