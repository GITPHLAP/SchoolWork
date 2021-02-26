using System;
using System.Collections.Generic;
using System.Text;

namespace HotDrinks
{
    class CupCoffee : CupHotDrink
    {
        protected override void Brew()
        {
            Console.WriteLine("Brewing coffee");
        }

        protected override void Pour()
        {
            Console.WriteLine("Pours the coffee into the cup");
        }

        private void AddMilkAndSugar()
        {
            Console.WriteLine("Add milk and sugar");
        }

        //Constructor to collect all methods in one.
        public CupCoffee()
        {
            BoilWater();
            Brew();
            Pour();
            AddMilkAndSugar();
        }
    }
}
