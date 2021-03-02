using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace DiningPhilosophers
{
    public class Philosophers
    {
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

                    //Take Forks
                    TakeForks((int)pIndex);

                    Eat((int)pIndex);

                    //Release the Forks 
                    ReleaseForks((int)pIndex);

                }
        }


        public void TakeForks(int pIndex)
        {
            //find max and min of Forks the philosoph can use.... The last part is index +1 mod (DK->Rest) of 5 
            int minindex = Math.Min((int)pIndex, ((int)pIndex + 1) % 5);
            int maxindex = Math.Max((int)pIndex, ((int)pIndex + 1) % 5);

            //create lock
            Monitor.Enter(locks);

            //so if Forks[index] is true then it will wait 
            // So if both of Forks is false then the filosoph can take Forks
            while (DinnerTable.Forks[minindex] || DinnerTable.Forks[maxindex])
            {
                Monitor.Wait(locks);

                //Console.WriteLine("Philosopher[{0}] is waiting...", pIndex);
            }

            //when both Forks is not in use 
            //then philosoph take them 
            DinnerTable.Forks[minindex] = true;
            DinnerTable.Forks[maxindex] = true;

            Monitor.Exit(locks);

        }

        public void ReleaseForks(int pIndex)
        {
            //find max and min of Forks the philosoph can use.... The last part is index +1 mod (DK->Rest) of 5 
            int minindex = Math.Min((int)pIndex, ((int)pIndex + 1) % 5);
            int maxindex = Math.Max((int)pIndex, ((int)pIndex + 1) % 5);


            //create lock
            Monitor.Enter(locks);

            //set Forks to not in use 
            DinnerTable.Forks[minindex] = false;
            DinnerTable.Forks[maxindex] = false;

            //put works down again on the table
            //Its a signal for the others philosophers to then can use them now
            Monitor.PulseAll(locks);

            Monitor.Exit(locks);

        }
    }
}
