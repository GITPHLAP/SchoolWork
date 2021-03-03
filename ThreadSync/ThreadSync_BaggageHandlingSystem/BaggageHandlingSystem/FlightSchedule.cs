using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaggageHandlingSystem
{
    class FlightSchedule
    {
        string departure;

        int gateNum;

        DateTime from;

        DateTime to;

        bool isStarted;


        public string Departure { get => departure; set => departure = value; }
        public int GateNum { get => gateNum; set => gateNum = value; }
        public DateTime From { get => from; set => from = value; }
        public DateTime To { get => to; set => to = value; }
        public bool IsStarted { get => isStarted; set => isStarted = value; }

        public FlightSchedule(string departure, int gateNum, DateTime from)
        {
            this.departure = departure;
            this.gateNum = gateNum;
            this.from = from;
        }


    }
}
