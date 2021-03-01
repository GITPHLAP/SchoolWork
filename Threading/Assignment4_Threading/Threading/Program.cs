using System;
using System.Threading;

namespace Threading
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Thread Class
            ThreadPrg tp = new ThreadPrg();

            //printer thread
            Thread printerThread = new Thread(tp.PrintMethod);

            printerThread.Start();

            //reader thread
            Thread readerThread = new Thread(tp.ReadMethod);

            readerThread.Start();

        }

    }

    class ThreadPrg
    {
        public char ch = '*';
        public void PrintMethod()
        {
            while (true)
            {
                Console.Write(ch);
            }
        }

        public void ReadMethod()
        {
            while (true)
            {
                ch = Console.ReadKey().KeyChar;
            }
        }
    }
}
