using System;

namespace ChrewNPhew
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Dispenser d = new Dispenser();

            //at trække et tyggegummi”
            PullDispenser(d);

        }

        static void PullDispenser(Dispenser dispenser)
        {
            Console.WriteLine("Her har du et {0} tyggegummi", dispenser.GetGum().Gumcolor.ToString());
        }
    }
}
