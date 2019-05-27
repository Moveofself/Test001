using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sumn_pinying
{
    class Program
    {
        //读入一个正整数 n，计算其各位数字之和，用汉语拼音写出和的每一位数字。

        //输入格式：
        //每个测试输入包含 1 个测试用例，即给出自然数 n 的值。这里保证 n 小于 10^100

        //输出格式：
        //在一行内输出 n 的各位数字之和的每一位，拼音数字间有 1 空格，但一行中最后一个拼音数字后没有空格。

        static void Main(string[] args)
        {
            char[] a = new char[100];
            int sum = 0, i = 0;

            ArrayList List = new ArrayList();

            //Console.Write("请输入数字：");
            string b = Console.ReadLine().ToString();
            a = b.ToCharArray();

            string[] py = new string[] { "ling", "yi", "er", "san", "si", "wu", "liu", "qi", "ba", "jiu" };

            foreach (char item in a)
            {
                sum += int.Parse(item.ToString());
            }

            //Console.WriteLine("和为：" + sum);
            if (sum==0)
            {
                List.Add(sum);
            }
            else
            {
                while (sum != 0)
                {
                    List.Add(sum % 10);
                    sum = sum / 10;
                    i++;
                }
            }
            

            for (int j = List.Count-1; j >= 0; j--)
            {
                if (j == 0)
                {
                    Console.Write(py[int.Parse(List[j].ToString())]);
                }
                else
                {
                    Console.Write(py[int.Parse(List[j].ToString())] + " ");
                }
            }
            Console.ReadKey();

        }
    }
}
