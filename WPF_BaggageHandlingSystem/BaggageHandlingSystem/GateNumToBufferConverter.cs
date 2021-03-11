using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ConsoleBaggageHandlingSystem;

namespace BaggageHandlingSystem
{
    public class GateNumToBufferConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //TODO: ErrorHandling
            Gate thisGate = SimulationManager.Gates.Where(g => g.GateNumber == (int)value).First();

            return thisGate.LuggagesBuffer.Count;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
