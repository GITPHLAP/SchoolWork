using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Assignment1_ThreadPools
{
    class Program
    {
        /// <summary> empty
        /// Thread Pool Execution
        /// Time consumed by ProcessWithThreadPoolMethod is : 27416
        /// Thread Execution
        /// Time consumed by ProcessWithThreadMethod is : 387815
        /// </summary>

        /// <summary> 1 forloop 
        /// Thread Pool Execution
        /// Time consumed by ProcessWithThreadPoolMethod is : 26070
        /// Thread Execution
        /// Time consumed by ProcessWithThreadMethod is : 857295
        /// </summary>

        /// <summary> 1 forloop 
        /// Thread Pool Execution
        /// Time consumed by ProcessWithThreadPoolMethod is : 28621
        /// Thread Execution
        /// Time consumed by ProcessWithThreadMethod is : 14468586
        /// </summary>




        static void Main(string[] args)
        {

            //All this to track time on it
            Stopwatch mywatch = new Stopwatch();
            Console.WriteLine("Thread Pool Execution");
            mywatch.Start();
            ProcessWithThreadPoolMethod();

            mywatch.Stop();
            Console.WriteLine("Time consumed by ProcessWithThreadPoolMethod is : " + mywatch.ElapsedTicks.ToString());
            mywatch.Reset();
            Console.WriteLine("Thread Execution");
            mywatch.Start();
            ProcessWithThreadMethod();
            mywatch.Stop();
            Console.WriteLine("Time consumed by ProcessWithThreadMethod is : " + mywatch.ElapsedTicks.ToString());



            Console.ReadLine();
        }

            //the threads work
            static void Process(object callback)
            {
                for (int i = 0; i < 100000; i++)
                {
                    for (int j = 0; j < 100000; j++)
                    {
                    }
                }
            }

            static void ProcessWithThreadMethod()
            {
                //create 10 threads 
                for (int i = 0; i <= 10; i++)
                {
                    Thread obj = new Thread(Process);
                    obj.Start();
                }
            }

            static void ProcessWithThreadPoolMethod()
            {
                //create threadpools
                for (int i = 0; i <= 10; i++)
                {
                    //control treads and queue
                    ThreadPool.QueueUserWorkItem(Process);
                }
            }
    }
}
