using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine
{
    public class CoffeeMachine : Machine, IFilter
    {
        
        public bool Usefilter { get { return true; } }

        public void RemoveFilter()
        {
            throw new NotImplementedException();
        }

        public void SetNewFilter()
        {
            throw new NotImplementedException();
        }

        public CoffeeMachine(double amountwatertank, string thenameofbrewing, string content, double amountofcontent)
        {
            Amountwatertank = amountwatertank;
            Thenameofbrewing = thenameofbrewing;
            ContainerContent = FillContent(content, amountofcontent, Usefilter);
        }

    }
}
