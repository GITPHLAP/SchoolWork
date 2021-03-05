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
        FlightSchedule schedule;

        //encapsulate properties
        public int PassengerNumber { get => passengerNumber; set => passengerNumber = value; }
        public string Name { get => name; set => name = value; }
        public string Destination { get => schedule.Destination;}

        //Constructer
        public Reservation(int passengerNumber, string name, FlightSchedule schedule)
        {
            this.passengerNumber = passengerNumber;
            this.name = name;
            this.schedule = schedule;

        }

    }
}
