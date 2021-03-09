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
        int lastluggageNum =0;

        int luggageNum;

        Reservation reservation;
        public int LuggageNum { get => luggageNum; set => luggageNum = value; }
        public string Destination { get => reservation.Destination;}

        public Luggage(Reservation reservation)
        {
            this.reservation = reservation;

            this.luggageNum = lastluggageNum + 1;

            //add luggge num to last luggagenum
            this.lastluggageNum = luggageNum;
        }

    }
}
