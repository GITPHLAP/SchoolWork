using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine
{
    public class EspressoMachine : Machine, IFilter
    {
        public enum EspressoShotsInGram {One = 1*7, Two = 2*7, Three = 3*7 }

        public bool Usefilter { get { return true; } }

        public void RemoveFilter()
        {
            throw new NotImplementedException();
        }

        public void SetNewFilter()
        {
            throw new NotImplementedException();
        }

        public EspressoMachine(string thenameofbrewing, string content, EspressoShotsInGram amountofshots)
        {
            Amountwatertank = 1;
            Thenameofbrewing = thenameofbrewing;
            ContainerContent = FillContent(content, (double)amountofshots, Usefilter);
        }
    }
}
