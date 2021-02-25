using System;

namespace ChrewNPhew
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Dispenser d = Dispenser.Instance;

            //at trække et tyggegummi”

            while (true)
            {
                if (d.GumList.Count == 0)
                {
                    RefillDispenser(d);
                }
                PullDispenser(d);
                Console.ReadLine();

                
            }

            

        }

        static void PullDispenser(Dispenser dispenser)
        {
            Console.WriteLine("Her har du et {0} tyggegummi", dispenser.GetGum().Gumcolor.ToString());
        }

        static void RefillDispenser(Dispenser d)
        {
            d.FillAll();
            Console.WriteLine("Du har refill dispenseren");
        }
    }
}
