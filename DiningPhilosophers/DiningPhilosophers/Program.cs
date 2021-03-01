using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DiningPhilosophers
{
    class Program
    {

        static void Main(string[] args)
        {
            Philosopher ps = new Philosopher();


            //Declare all philosophis as threads
            Thread p0 = new Thread(ps.PhilosophMethod);
            Thread p1 = new Thread(ps.PhilosophMethod);
            Thread p2 = new Thread(ps.PhilosophMethod);
            Thread p3 = new Thread(ps.PhilosophMethod);
            Thread p4 = new Thread(ps.PhilosophMethod);

            p0.Start(0);
            p1.Start(1);
            p2.Start(2);
            p3.Start(3);
            p4.Start(4);



            Console.ReadLine();
        }
    }
    class Philosopher
    {
        bool[] forks = new bool[5];

        Random rndnumber = new Random();

        object locks = new object();

        public void Eat(int pIndex)
        {

            //Tell who eating
            Console.WriteLine("Philosopher[{0}] is eating...", pIndex);

            //random amount of seconds the philosoph eat
            Thread.Sleep(rndnumber.Next(1, 11) * 1000);

            Console.WriteLine("DONE eating Philosopher[{0}]", pIndex);

        }

        public void Think(int pIndex)
        {
            //write that philo is thinking
            //Console.WriteLine("Philosopher[{0}] is thinking...", pIndex);

            //random amount of seconds the philosoph think
            Thread.Sleep(rndnumber.Next(1, 11) * 1000);

        }

        //method to illustratet what the philosoph do
        public void PhilosophMethod(object pIndex)
        {
            if (pIndex is int)
                
                while (true)
                {

                    Think((int)pIndex);

                    //Take forks
                    TakeForks((int)pIndex);
                    
                    Eat((int)pIndex);

                    //Release the forks 
                    ReleaseForks((int)pIndex);

                }
        }


        public void TakeForks(int pIndex)
        {
            //find max and min of forks the philosoph can use.... The last part is index +1 mod (DK->Rest) of 5 
            int minindex = Math.Min((int)pIndex, ((int)pIndex + 1) % 5);
            int maxindex = Math.Max((int)pIndex, ((int)pIndex + 1) % 5);

            //create lock
            Monitor.Enter(locks);

            //so if forks[index] is true then it will wait
            while (forks[minindex] || forks[maxindex])
            {
                Monitor.Wait(locks);

                //Console.WriteLine("Philosopher[{0}] is waiting...", pIndex);
            }

            //when both forks is not in use 
            //then philosoph take them 
            forks[minindex] = true;
            forks[maxindex] = true;

            Monitor.Exit(locks);

        }




        public void ReleaseForks(int pIndex)
        {
            //find max and min of forks the philosoph can use.... The last part is index +1 mod (DK->Rest) of 5 
            int minindex = Math.Min((int)pIndex, ((int)pIndex + 1) % 5);
            int maxindex = Math.Max((int)pIndex, ((int)pIndex + 1) % 5);

            //create lock
            Monitor.Enter(locks);

            //set forks to not in use 
            forks[minindex] = false;
            forks[maxindex] = false;

            //put works down again on the table
            //Its a signal for the others philosophers to then can use them now
            Monitor.PulseAll(locks);

            Monitor.Exit(locks);

        }

    }
}
