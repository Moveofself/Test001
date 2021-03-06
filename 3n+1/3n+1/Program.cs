﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3n_1
{
    class Program
    {
        //卡拉兹(Callatz)猜想：
        //对任何一个正整数 n，如果它是偶数，那么把它砍掉一半；如果它是奇数，那么把(3n+1) 砍掉一半。
        //这样一直反复砍下去，最后一定在某一步得到 n = 1。卡拉兹在 1950 年的世界数学家大会上公布了这个猜想
        //传说当时耶鲁大学师生齐动员，拼命想证明这个貌似很傻很天真的命题，结果闹得学生们无心学业，一心只证(3n+1)，
        //以至于有人说这是一个阴谋，卡拉兹是在蓄意延缓美国数学界教学与科研的进展……

        //我们今天的题目不是证明卡拉兹猜想，而是对给定的任一不超过 1000 的正整数 n，简单地数一下，需要多少步（砍几下）才能得到 n = 1？

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
