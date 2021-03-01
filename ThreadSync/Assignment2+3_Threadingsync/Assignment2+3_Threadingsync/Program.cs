using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Assignment2_3_Threadingsync
{
    class Program
    {
        static int currentchar;

        static object locks = new object();
        static void Main(string[] args)
        {
            //Create the threads
            Thread starT = new Thread(WriteStars);
            Thread tagT = new Thread(WriteTags);

            starT.Start();
            tagT.Start();


            Console.ReadLine();

        }


        static void WriteStars()
        {
            while (true)
            {
                //enter the lock
                Monitor.Enter(locks);
                Thread.Sleep(1000);
                string starstring = "";
                for (int i = 0; i < 60; i++)
                {
                    starstring = starstring + "*";
                }

                currentchar = currentchar + starstring.Length;

                Console.WriteLine("\n{0} {1}", starstring, currentchar);

                //exit the lock 
                Monitor.Exit(locks);
            }
            
            
        }

        static void WriteTags()
        {
            while (true)
            {
                //enter the lock
                Monitor.Enter(locks);

                //thread sleep
                Thread.Sleep(1000);

                //local string to write to console
                string tagstring = "";
                //add # to string 60 times
                for (int i = 0; i < 60; i++)
                {
                    tagstring = tagstring + "#";
                }

                //Take the length and add it 
                currentchar = currentchar + tagstring.Length;

                Console.WriteLine("\n{0} {1}", tagstring, currentchar);

                //exit the lock 
                Monitor.Exit(locks);
            }
            

        }
    }
}
