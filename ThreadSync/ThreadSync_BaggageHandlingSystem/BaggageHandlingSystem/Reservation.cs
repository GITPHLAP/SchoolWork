using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaggageHandlingSystem
{
    class Reservation
    {
        //properties 
        int passengerNumber;
        string name;
        int departureNumber;

        //encapsulate properties
        public int PassengerNumber { get => passengerNumber; set => passengerNumber = value; }
        public string Name { get => name; set => name = value; }
        public int DepartureNumber { get => departureNumber; set => departureNumber = value; }

        //Constructer
        public Reservation(int passengerNumber, string name, int departureNumber)
        {
            this.passengerNumber = passengerNumber;
            this.name = name;
            this.departureNumber = departureNumber;
        }

    }
}
