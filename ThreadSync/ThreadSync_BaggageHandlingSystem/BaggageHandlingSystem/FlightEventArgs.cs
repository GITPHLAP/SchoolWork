using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBaggageHandlingSystem
{
    public class FlightEventArgs : EventArgs
    {
        public FlightSchedule Schedule { get; private set; }

        //This constructer is used when flight take off
        public FlightEventArgs(FlightSchedule schedule, int passengers)
        {
            this.Schedule = schedule;
            this.Schedule.PassengerAmount = passengers;
        }
        public FlightEventArgs(FlightSchedule schedule)
        {
            this.Schedule = schedule;
        }
    }
}
