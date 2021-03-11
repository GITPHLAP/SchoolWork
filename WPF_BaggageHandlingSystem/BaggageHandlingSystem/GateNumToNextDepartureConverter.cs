using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Data;
using ConsoleBaggageHandlingSystem;



namespace BaggageHandlingSystem
{
    public class GateNumToNextDepartureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //TODO: ErrorHandling

            Gate thisGate = SimulationManager.Gates.Where(g => g.GateNumber == (int)value).FirstOrDefault();

            if (thisGate.GateSchedule != null)
            {
                return thisGate.GateSchedule.Departure.ToString();
            }
            else
            {
                return "";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
