using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine
{
    public interface IFilter
    {
        bool Usefilter { get; }
        //To set a new filter in content container
        void SetNewFilter();

        //To remove the old filter
        void RemoveFilter();

    }
}
