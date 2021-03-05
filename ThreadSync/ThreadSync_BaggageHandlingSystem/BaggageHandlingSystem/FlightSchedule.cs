﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaggageHandlingSystem
{
    class FlightSchedule
    {

        //TODO: Destination instead of departure
        string distination;

        int gateNum;

        DateTime arrival;

        DateTime departure;

        bool isDone;


        public string Distination { get => distination; set => distination = value; }
        public int GateNum { get => gateNum; set => gateNum = value; }
        public DateTime Arrival { get => arrival; set => arrival = value; }
        public DateTime Departure { get => departure; set => departure = value; }
        public bool IsDone { get => isDone; set => isDone = value; }

        public FlightSchedule(string distination, int gateNum, DateTime arrival, DateTime departure)
        {
            this.distination = distination;
            this.gateNum = gateNum;
            this.arrival = arrival;
            this.departure = departure;
        }

        public override bool Equals(object obj)
        {
            FlightSchedule fs = obj as FlightSchedule;
            if (string.Equals(this.Distination, fs.Distination, StringComparison.OrdinalIgnoreCase)
                && int.Equals(this.GateNum, fs.GateNum)
                && DateTime.Equals(this.Arrival, fs.Arrival)
                && DateTime.Equals(this.Departure, fs.Departure))   
            {
                return true;
            }
            return false;
        }

    }
}
