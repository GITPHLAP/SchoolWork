using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace ThreadPoolDemo
{
    class Program
    {
        //
        public void task1(object obj)
        {
            //for 3 times
            for (int i = 0; i <= 2; i++)
            {
                Console.WriteLine("Task 1 is being executed");
            }
        }
        public void task2(object obj)
        {
            //for 3 times
            for (int i = 0; i <= 2; i++)
            {
                Console.WriteLine("Task 2 is being executed");
            }
        }

        static void Main()
        {
            Program tpd = new Program();
            for (int i = 0; i < 2; i++)
            {
                //create thread pools 
                ThreadPool.QueueUserWorkItem(new WaitCallback(tpd.task1));
                ThreadPool.QueueUserWorkItem(new WaitCallback(tpd.task2));
            }

            Console.Read();
        }
    }
}
