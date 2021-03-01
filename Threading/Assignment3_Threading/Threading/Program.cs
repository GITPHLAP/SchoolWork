using System;
using System.Threading;

namespace Threading
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //the thread class
            ThreadPrg thrprg = new ThreadPrg();

            //new thread to get a random temperatur
            Thread rndtempThread = new Thread(thrprg.RandomTempMethod);

            rndtempThread.Start();


            Thread checktemp = new Thread(thrprg.CheckTempMethod);

            checktemp.Start();


            while (true)
            {
                Thread.Sleep(10000);
                if (!checktemp.IsAlive)
                {
                    Console.WriteLine("Alarm-tråd termineret!");
                    Environment.Exit(1);
                }
            }
        }


    }

    class ThreadPrg
    {
        public int AlertCounts = 0;
        int randomtemp;
        public void RandomTempMethod()
        {
            Random rndtemp = new Random();
            while (true)
            {
                Thread.Sleep(2000);
                //random number between -20 and 120 
                int rndtempnumber = rndtemp.Next(-20, 121);

                //set global variable to rndtemp
                randomtemp = rndtempnumber;

                Console.WriteLine(rndtempnumber);
            }
        }

        public void CheckTempMethod()
        {

            while (true)
            {
                //sleep so the be sure to get a new number
                Thread.Sleep(2000);

                //if randomtemp smaller then 0 and bigger then 100 then...
                if (0 > randomtemp || randomtemp > 100)
                {
                    //add one to alert counter
                    AlertCounts++;

                    //Write message to console
                    Console.WriteLine("Alert bad temp");

                }
                //if alert counts more then 3 then stop method
                if (AlertCounts >= 3)
                {
                    break;
                }
            }
        }
    }
}
