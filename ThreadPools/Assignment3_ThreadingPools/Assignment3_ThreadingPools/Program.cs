using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Assignment3_ThreadingPools
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create Threadpool

            for (int i = 0; i < 10; i++)
            {

                ThreadPool.QueueUserWorkItem(Process);
            }
            Thread t1 = new Thread(Process);

            //begin the thread
            t1.Start();

            Thread t2 = new Thread(Process);
            t1.Start();

            //
            t2.Join();

            Console.ReadLine();

        }



        //the threads work
        static void Process(object callback)
        {

                    Console.WriteLine("[{0}] ThreadName: {1} Backgroud: {2} Priority: {3}",
                       Thread.CurrentThread.ManagedThreadId,
                       Thread.CurrentThread.IsAlive,
                       Thread.CurrentThread.IsBackground,
                       Thread.CurrentThread.Priority
                       );
                    Thread.Sleep(1000);



        }

    }
}
