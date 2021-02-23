using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine
{
    public class ContentContainer
    {
        string content;

        double contentamount;

        bool usefilter;

        public bool Usefilter { get => usefilter; set => usefilter = value; }
        public double Contentamount { get => contentamount; set => contentamount = value; }
        public string Content { get => content; set => content = value; }
    }
}
