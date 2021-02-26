using System;
using System.Collections.Generic;
using System.Text;

namespace HotDrinks
{
    class CupTea : CupHotDrink
    {
        //brewing
        protected override void Brew()
        {
            Console.WriteLine("Brewing tea");
        }
        //override method from abstract class
        protected override void Pour()
        {
            Console.WriteLine("Pours the tea into the cup");
        }

        // method to add lemon to the tea
        void AddLemon()
        {
            Console.WriteLine("Put lemon in the tea");
        }

        //Constructor to make instant tea when initialize tea
        public CupTea()
        {
            BoilWater();
            Brew();
            Pour();
            AddLemon();
        }

    }
}
