using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Mult_Threading_Test1
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadDemoClass demoClass = new ThreadDemoClass();

            //创建一个新的线程
            Thread thread = new Thread(demoClass.Run);

            //设置为后台线程
            thread.IsBackground = true;

            //开始线程
            thread.Start();

            //等待直到线程完成
            thread.Join();

            Console.WriteLine("Main thread working...");
            Console.WriteLine("Main thread ID is:" + Thread.CurrentThread.ManagedThreadId.ToString());

            Console.ReadKey();
        }
    }

    public class ThreadDemoClass
    {
        public void Run()
        {
            Console.WriteLine("Child thread working...");
            Console.WriteLine("Child thread ID is:" + Thread.CurrentThread.ManagedThreadId.ToString());
        }
    }


}
