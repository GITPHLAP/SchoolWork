using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine
{
    public abstract class Machine
    {

        double amountwatertank;

        bool turnon;

        ContentContainer containerContent;

        string thenameofbrewing;


        protected double Amountwatertank { get => amountwatertank; set => amountwatertank = value; }
        protected ContentContainer ContainerContent { get => containerContent; set => containerContent = value; }
        protected string Thenameofbrewing { get => thenameofbrewing; set => thenameofbrewing = value; }

        public void TurnMachineOn()
        {
            Console.WriteLine("Turn ON");
            turnon = true;

            Brewing();
        }

        private void Brewing()
        {
            DateTime starttime = DateTime.Now;

            //watertank * 2 because it takes 2 seconds for every cup
            DateTime endtime = starttime.AddSeconds(amountwatertank * 2);

            while (DateTime.Now <= endtime)
            {
                Console.WriteLine("Brewing");
            }
            amountwatertank = 0;
        }

        public void FillWaterTank(double amountofwater)
        {
            amountwatertank += amountofwater;
        }

        public void TurnMachineOff()
        {
            Console.WriteLine("Turn OFF");
            turnon = false;
        }

        //To fill machine with content
        public ContentContainer FillContent(string contentinput, double amount, bool usefilter)
        {
            return new ContentContainer()
            {
                Usefilter = usefilter,
                Content = contentinput,
                Contentamount = amount
            };
        }

    }
}
