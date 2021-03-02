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
            //initalize philosophers
            Philosophers philo = new Philosophers();

            //Declare all philosophis as threads
            Thread p0 = new Thread(philo.PhilosophMethod);
            Thread p1 = new Thread(philo.PhilosophMethod);
            Thread p2 = new Thread(philo.PhilosophMethod);
            Thread p3 = new Thread(philo.PhilosophMethod);
            Thread p4 = new Thread(philo.PhilosophMethod);

            p0.Start(0);
            p1.Start(1);
            p2.Start(2);
            p3.Start(3);
            p4.Start(4);

            Console.ReadLine();
        }
    }
}
