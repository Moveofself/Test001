using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3n_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int n, count;
            string input;

            for (int i = 0; ; i++)
            {
                input = "";
                count = 0;
                Console.Write("请输入数字：");
                input = Console.ReadLine();
                if (input.ToUpper() == "EXIT")
                {
                    Console.Write("请重新输入!");
                    continue;
                }
                if (string.IsNullOrEmpty("input"))
                {
                    Console.Write("请重新输入!");
                    continue;
                }
                try
                {
                    n = int.Parse(input);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    return;
                }

                if (n > 0 && n <= 1000)
                {
                    do
                    {
                        if (n % 2 == 0)//偶数
                        {
                            n = n / 2;
                        }
                        else //奇数
                        {
                            n = (3 * n + 1) / 2;
                        }
                        count++;
                    } while (n != 1);
                    Console.WriteLine("从 n 计算到 1 需要的步数：" + count);
                    Console.WriteLine("按任意键继续！");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("输入数值超出1000，请重新输入!");
                    continue;
                }
            }

        }
    }
}
