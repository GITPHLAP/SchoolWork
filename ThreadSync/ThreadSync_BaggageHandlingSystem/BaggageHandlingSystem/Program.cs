using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace BaggageHandlingSystem
{
    class Program
    {

        //public static object testobj = new object();


        //static void TestMethod()
        //{
        //    while (true)
        //    {
        //        Debug.WriteLine($"{Thread.CurrentThread.Name} run");
        //        Thread.Sleep(5000);
        //        lock (testobj)
        //        {
        //            Debug.WriteLine($"{Thread.CurrentThread.Name} wait");
        //            Monitor.Wait(testobj);
        //        }
        //    }
        //}

        //static void MainTestMethod()
        //{
        //    while (true)
        //    {
        //        char userInput = Console.ReadKey().KeyChar;
        //        switch (userInput)
        //        {
        //            case '1':
        //                lock (testobj)
        //                {
        //                    Debug.WriteLine($"{Thread.CurrentThread.Name} pulse");
        //                    Monitor.PulseAll(testobj);
        //                }

        //                break;
        //            case '2':
        //                //threadlist[0].ThreadState = System.Threading.ThreadState.WaitSleepJoin
        //                Console.WriteLine(threadlist[0].ThreadState);
        //                Console.WriteLine(threadlist[1]);

        //                var test = Process.GetCurrentProcess().Threads;
        //                break;
        //            default:
        //                break;
        //        }

        //        Console.ReadLine();
        //    }
        //}

        //public static List<Thread> threadlist = new List<Thread>();


        static void Main(string[] args)
        {
            //Thread testmain = new Thread(MainTestMethod);
            //testmain.Name = "testmain";
            //testmain.Start();

            //threadlist.Add(new Thread(TestMethod));
            //threadlist[0].Name = "test1";
            //threadlist[0].Start();


            //threadlist.Add(new Thread(TestMethod));
            //threadlist[1].Name = "test2";
            //threadlist[1].Start();












            SimulationManager manager = new SimulationManager();




            manager.CLI();






            //ReservationSystem testre = new ReservationSystem();

            //testre.ReadCSVFileAndAddToList(@"Reservation.csv");





            Console.ReadLine();
        }
    }

}
