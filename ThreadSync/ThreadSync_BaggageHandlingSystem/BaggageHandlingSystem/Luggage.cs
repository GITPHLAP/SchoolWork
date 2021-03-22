using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBaggageHandlingSystem
{
    public class Luggage
    {
        //to check luggage cant have the same number
        static int lastluggageNum =0;

        int luggageNum;

        public int LuggageNum { get => luggageNum; set => luggageNum = value; }
        public string Destination { get => Reservation.Destination;}
        public Reservation Reservation { get; }

        public Luggage(Reservation reservation)
        {
            this.Reservation = reservation;

            this.luggageNum = lastluggageNum + 1;

            //add luggge num to last luggagenum
            Luggage.lastluggageNum = luggageNum;
        }

    }
}
