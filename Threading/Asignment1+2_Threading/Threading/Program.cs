using System;
using System.Threading;

namespace Threading
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Create thread
            Thread thread = new Thread(ThreadMethod);

            //Start thread
            thread.Start();

            //Create second thread
            Thread thread2 = new Thread(SecondThreadMethod);

            thread2.Start();
        }


        static void ThreadMethod()
        {
            //execute cw 5 times
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("C#-trådning er nemt!");
            }
        }

        static void SecondThreadMethod()
        {
            for (int i = 0; i < 5; i++)
            {
                //Ikke den smarteste ide at bruge thread.sleep fordi den lukker hele applikationen
                Thread.Sleep(1000);
                Console.WriteLine("Også med flere tråde …");
            }
        }
    }
}
