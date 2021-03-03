using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace BaggageHandlingSystem
{
    class Reservation
    {
        //properties 
        int passengerNumber;
        string name;
        string departure;

        //encapsulate properties
        public int PassengerNumber { get => passengerNumber; set => passengerNumber = value; }
        public string Name { get => name; set => name = value; }
        public string Departure { get => departure; set => departure = value; }

        //Constructer
        public Reservation(int passengerNumber, string name, string departureNumber)
        {
            this.passengerNumber = passengerNumber;
            this.name = name;
            this.departure = departureNumber;
        }

    }
}
