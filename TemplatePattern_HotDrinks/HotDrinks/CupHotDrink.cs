using System;
using System.Collections.Generic;
using System.Text;

namespace HotDrinks
{
    public abstract class CupHotDrink
    {
        //abstract method which subclass need to add
        protected abstract void Brew();
        
        //method to boiling water
        protected void BoilWater()
        {
            Console.WriteLine("Boiling water");
        }

        protected abstract void Pour();
    }
}
