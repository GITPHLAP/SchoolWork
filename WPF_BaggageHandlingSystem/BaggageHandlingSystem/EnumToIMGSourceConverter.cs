using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using ConsoleBaggageHandlingSystem;

namespace BaggageHandlingSystem
{
    public class EnumToIMGSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            switch ((value))
            {
                case Gate.FlightStatus.Working:
                    
                    return "/Images/Working.png";
                case Gate.FlightStatus.TakeOff:

                    return "/Images/TakeOff.png";
                case Gate.FlightStatus.Landing:

                    return "/Images/Landing.png";
                case Gate.FlightStatus.NoMoreFlight:
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
