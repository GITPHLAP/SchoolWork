using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BottleMachine
{
    class Program
    {
        public static BufferQueue<Bottle> Bottlebuffer = new BufferQueue<Bottle>(10);

        public static BufferQueue<Bottle> Beerbuffer = new BufferQueue<Bottle>(10);

        public static BufferQueue<Bottle> Sodabuffer = new BufferQueue<Bottle>(10);

        //Varians of Bottles 
        public static List<string> Bottlevarians = new List<string> { "Beer", "Soda" };

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Black;


            //create all the needed classes
            Producer bottlePro = new Producer();

            Consumer sodaCon = new Consumer();

            Consumer beerCon = new Consumer();

            Splitter split = new Splitter();

            //Create all the needed threads and give them names 
            Thread prT = new Thread(bottlePro.ProduceBottles);
            prT.Name = "ProducerThread";

            Thread sodacT = new Thread(sodaCon.ConsumeSoda);
            sodacT.Name = "SodaConThread";

            Thread beercT = new Thread(beerCon.ConsumeBeer);
            beercT.Name = "BeerConThread";

            Thread splitT = new Thread(split.SplitterMethod);
            splitT.Name = "SplitterThread";


            //Start all threads
            prT.Start();

            sodacT.Start();

            beercT.Start();

            splitT.Start();

            Console.ReadLine();
        }
    }

    class Producer
    {
        //To pick a random index 
        Random random = new Random();
        public void ProduceBottles()
        {

            while (true)
            {
                

                while (Program.Bottlebuffer.IsLimitReached())
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    //wait for a push
                    Console.WriteLine("P Wait...");
                    Monitor.Enter(Program.Bottlebuffer);

                    Monitor.Wait(Program.Bottlebuffer);

                    Monitor.Exit(Program.Bottlebuffer);
                }

                if (Monitor.TryEnter(Program.Bottlebuffer))
                {

                    int counter = 1;

                    //if limit not reache then do the stuff in whie
                    while (!Program.Bottlebuffer.IsLimitReached())
                    {
                        //get a random number from 0 to 2 
                        int tempindex = random.Next(Program.Bottlevarians.Count);

                        //take name from index of varians-list and add it to a new bottle 
                        Program.Bottlebuffer.Enqueue(new Bottle(Program.Bottlevarians[tempindex].ToString()));

                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("P enqueue: " + counter);
                        //increase counter
                        counter++;
                    }

                    Monitor.Pulse(Program.Bottlebuffer);

                    Monitor.Exit(Program.Bottlebuffer);

                }

            }
        }
    }

    class Consumer
    {
        public void ConsumeBeer()
        {
            while (true)
            {


                Thread.Sleep(500);

                //try to enter buffer
                if (Monitor.TryEnter(Program.Beerbuffer))
                {
                    //if buffer empty and wait for a pulse
                    while (Program.Beerbuffer.Count == 0)
                    {
                        //invoke threads who waiting 
                        //TODO: Pulse or PulseAll
                        Monitor.Pulse(Program.Beerbuffer);

                        Monitor.Wait(Program.Beerbuffer);
                    }
                    //take one from queue
                    Program.Beerbuffer.Dequeue();
                    
                    Console.BackgroundColor = ConsoleColor.Green;

                    Console.WriteLine("{0} deque", Thread.CurrentThread.Name);


                    Monitor.Exit(Program.Beerbuffer);
                }

            }
        }

        public void ConsumeSoda()
        {
            while (true)
            {


                Thread.Sleep(500);

                //try to enter buffer
                if (Monitor.TryEnter(Program.Sodabuffer))
                {
                    //if buffer empty and wait for a pulse
                    while (Program.Sodabuffer.Count == 0)
                    {
                        //invoke threads who waiting 
                        //TODO: Pulse or PulseAll
                        Monitor.Pulse(Program.Sodabuffer);

                        Monitor.Wait(Program.Sodabuffer);
                    }
                    //take one from queue
                    Program.Sodabuffer.Dequeue();

                    Console.BackgroundColor = ConsoleColor.Blue;

                    Console.WriteLine("{0} deque", Thread.CurrentThread.Name);


                    Monitor.Exit(Program.Sodabuffer);
                }

            }
        }
    }

    class Splitter
    {


        public void SplitterMethod()
        {
            while (true)
            {
                ConsumeBottles();
            }
        }



        public void SplitBeer(Bottle bottle)
        {

            while (Program.Beerbuffer.IsLimitReached())
            {
                //wait for a push

                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Consumer/Splitter Wait for Beer...");
                Monitor.Enter(Program.Beerbuffer);

                Monitor.Wait(Program.Beerbuffer);

                Monitor.Exit(Program.Beerbuffer);
            }


            //if limit not reache then do the stuff in while
            if (Monitor.TryEnter(Program.Beerbuffer) && !Program.Beerbuffer.IsLimitReached())
            {

                //take name from index of varians-list and add it to a new bottle 
                Program.Beerbuffer.Enqueue(bottle);

                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Consumer/Splitter add beer to BeerBuffer ");


                Monitor.Pulse(Program.Beerbuffer);

                Monitor.Exit(Program.Beerbuffer);

            }
        }
        public void SplitSoda(Bottle bottle)
        {
            while (Program.Beerbuffer.IsLimitReached())
            {
                //wait for a push
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Consumer/Splitter Wait for Soda...");
                Monitor.Enter(Program.Sodabuffer);

                Monitor.Wait(Program.Sodabuffer);

                Monitor.Exit(Program.Sodabuffer);
            }


            //if limit not reache then do the stuff in while
            if (Monitor.TryEnter(Program.Sodabuffer) && !Program.Sodabuffer.IsLimitReached())
            {

                //take name from index of varians-list and add it to a new bottle 
                Program.Sodabuffer.Enqueue(bottle);

                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Consumer/Splitter add soda to SodaBuffer ");


                Monitor.Pulse(Program.Sodabuffer);

                Monitor.Exit(Program.Sodabuffer);

            }
        }




        public Bottle ConsumeBottles()
        {
            Thread.Sleep(500);

            //try to enter buffer
            Monitor.Enter(Program.Bottlebuffer);

            //if buffer empty and wait for a pulse
            while (Program.Bottlebuffer.Count == 0)
            {
                //invoke threads who waiting 
                //TODO: Pulse or PulseAll
                Monitor.Pulse(Program.Bottlebuffer);

                Monitor.Wait(Program.Bottlebuffer);
            }

            //take one from queue and add it to temp bottle 
            Bottle tempbottle = Program.Bottlebuffer.Dequeue();
            
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0} Consume: {1}", Thread.CurrentThread.Name, tempbottle.Name);

            if (tempbottle.Name == "Beer")
            {
                //send tempbottle to next method and next buffer
                SplitBeer(tempbottle);
            }
            else
            {
                SplitSoda(tempbottle);
            }

            Monitor.Exit(Program.Bottlebuffer);
            return tempbottle;


        }






    }
}
