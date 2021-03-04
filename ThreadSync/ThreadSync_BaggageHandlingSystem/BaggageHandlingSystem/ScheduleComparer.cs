using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaggageHandlingSystem
{
    class ScheduleComparer : IEqualityComparer<FlightSchedule>
    {
        public bool Equals(FlightSchedule x, FlightSchedule y)
        {
            if (string.Equals(x.Distination, y.Distination, StringComparison.OrdinalIgnoreCase)
                && int.Equals(x.GateNum, y.GateNum)
                && DateTime.Equals(x.Arrival, y.Arrival)
                && DateTime.Equals(x.Departure, y.Departure))
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(FlightSchedule obj)
        {
            return obj.Distination.GetHashCode();
        }
    }
}
