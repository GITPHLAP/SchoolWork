using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace BaggageHandlingSystem
{
    public class boolToGateColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
                return (bool)value ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF9191")) : (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF93FF93"));
            return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF9191"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
