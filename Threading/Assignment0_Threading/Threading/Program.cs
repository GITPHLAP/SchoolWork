using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Threading
{
    class Program
    {
        //function to thread
        public void WorkThreadFunction(object threadname)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Simple Thread Name: " + threadname);
            }
        }
    }

    class threprog
    {
        public static void Main()
        {
            Program pg = new Program();

            //new thread with workthreadfunction
            Thread thread = new Thread(pg.WorkThreadFunction);
            //Give first thread a name
            thread.Name = "First Thread";
            //Start the first thread and add parameter 
            thread.Start(thread.Name);

            Thread thread2 = new Thread(pg.WorkThreadFunction);
            thread2.Name = "Second Thread";
            thread2.Start(thread2.Name);


            Console.Read();
        }
    }

}
