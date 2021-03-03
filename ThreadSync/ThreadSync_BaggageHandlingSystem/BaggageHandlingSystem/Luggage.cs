using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaggageHandlingSystem
{
    class Luggage
    {
        //to check luggage cant have the same number
        int lastluggageNum =0;


        int luggageNum;
        string departure;
        public int LuggageNum { get => luggageNum; set => luggageNum = value; }
        public string Departure { get => departure; set => departure = value; }

        public Luggage(string departure)
        {
            this.departure = departure;

            this.luggageNum = lastluggageNum + 1;

            //add luggge num to last luggagenum
            this.lastluggageNum = luggageNum;
        }

    }
}
