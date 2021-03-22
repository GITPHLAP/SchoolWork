using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;


namespace BaggageHandlingSystem
{
    public class ThreadToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF9191")) : (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF93FF93"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
