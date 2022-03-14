using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ValutaConverterApp.Models
{
    public class ExchangeRate
    {
        public ExchangeRate(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public double Rate { get; set; }

    }
}
