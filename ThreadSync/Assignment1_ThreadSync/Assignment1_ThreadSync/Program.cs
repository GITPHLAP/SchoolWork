using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Assignment1_ThreadSync
{
    class Program
    {
        static int sum;

        static object locks = new object();

        static void Main(string[] args)
        {
            //Create the 2 threads
            Thread t1 = new Thread(Up);

            Thread t2 = new Thread(Down);

            t1.Start();
            t2.Start();

            while (true)
            {
                //Write it to console 
                Monitor.Enter(locks);
                Console.WriteLine(sum);
                Monitor.Exit(locks);
            }

        }

        static void Up()
        {
            while (true)
            {
                //Create the lock and add 2 to sum
                Monitor.Enter(locks);
                sum = sum + 2;
                Monitor.Exit(locks);
                Thread.Sleep(1000);
            }
            
        }
        static void Down()
        {
            while (true)
            {
                Monitor.Enter(locks);
                sum--;
                Monitor.Exit(locks);
                Thread.Sleep(1000);
            }
            
        }
    }
}
