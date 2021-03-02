using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProducerAndConsumer
{
    class Program
    {
        //Øvelse 5, vi fik lov til at springe øvelse 4.


        public static BufferQueue<int> buffer = new BufferQueue<int>(3);



        static void Main(string[] args)
        {


            //create a Producer and a Consumer
            Producer pro = new Producer();

            Consumer con = new Consumer();

            Consumer con1 = new Consumer();


            //Create the one thread as producer and consumer
            Thread pT = new Thread(pro.Produce);

            Thread cT = new Thread(con.Consume);
            cT.Name = "Tråd1";
            cT.Priority = ThreadPriority.Highest;

            Thread cT1 = new Thread(con1.Consume);
            cT1.Name = "Tråd2";
            cT1.Priority = ThreadPriority.Lowest;




            pT.Start();

            cT.Start();

            cT1.Start();


            Console.ReadLine();

        }
    }
    class Producer
    {
        public void Produce()
        {
            while (true)
            {
                while (Program.buffer.IsLimitReached())
                {
                    Console.WriteLine("P Wait...");
                    Monitor.Enter(Program.buffer);

                    Monitor.Wait(Program.buffer);

                    Monitor.Exit(Program.buffer);
                }



                if (Monitor.TryEnter(Program.buffer))
                {

                    int counter = 1;
                    //if limit not reache then do the stuff in whie
                    while (!Program.buffer.IsLimitReached())
                    {
                        Program.buffer.Enqueue(counter);
                        Console.WriteLine("P enqueue: " + counter);
                        //increase counter
                        counter++;
                    }

                    Monitor.Pulse(Program.buffer);

                    Monitor.Exit(Program.buffer);

                }

                



            }
        }
    }

    class Consumer
    {
        public void Consume()
        {
            while (true)
            {
                Thread.Sleep(500);


                if (Monitor.TryEnter(Program.buffer))
                {
                    while (Program.buffer.Count == 0)
                    {
                        Monitor.Pulse(Program.buffer);

                        Monitor.Wait(Program.buffer);
                    }

                    Program.buffer.Dequeue();

                    Console.WriteLine("{0} deque", Thread.CurrentThread.Name);


                    Monitor.Exit(Program.buffer);
                }
                //else
                //{
                //    Console.WriteLine("P --- Venter");
                //    Monitor.Wait(Program.buffer);
                //}

            }
        }
    }
}
