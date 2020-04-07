using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            DeleteFiles("D:\\Test");
            Console.ReadKey();
            //string sInput;
            //long input;
            //while (true)
            //{

            //    sInput = "";
            //    input = 0;
            //    Console.Write("请输入16进制字符串：");
            //    sInput = Console.ReadLine();

            //    try
            //    {
            //        input = Convert.ToInt64(sInput, 16);

            //        byte[] data = Encoding.Unicode.GetBytes(sInput);
            //        StringBuilder result = new StringBuilder(data.Length * 8);

            //        dynamic a = Convert.ToString(int.Parse(sInput), 2);

            //        Console.WriteLine("转换为2进制结果为：" + a);
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e.Message);
            //        Console.ReadKey();
            //        return;
            //    }

            //    Console.WriteLine("转换为10进制结果为：" + input);
            //    Console.WriteLine("按任意键继续！");
            //    Console.ReadKey();
            //}

        }

        public static void DeleteFiles(string str)
        {
            DirectoryInfo fatherFolder = new DirectoryInfo(str);
            //删除当前文件夹内文件
            FileInfo[] files = fatherFolder.GetFiles();
            foreach (FileInfo file in files)
            {
                string fileName = file.Name;
                try
                {
                    File.Delete(file.FullName);
                    Console.WriteLine(file.FullName + "删除成功！");
                }
                catch (Exception ex)
                {
                }
            }

            //递归删除子文件夹内文件
            foreach (DirectoryInfo childFolder in fatherFolder.GetDirectories())
            {
                DeleteFiles(childFolder.FullName);
                //Directory.Delete(childFolder.FullName);
                //Console.WriteLine(childFolder.FullName + "删除成功！");
            }
        }

    }
}
