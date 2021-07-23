// Author: David Numa

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using static RPSLS.VM;

namespace RPSLS
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //throw new NotImplementedException();
            var color = new SolidColorBrush(Colors.Black);
            switch ((ColorMessageCodes)value)
            {
                case ColorMessageCodes.Black:
                    color = new SolidColorBrush(Colors.Black);
                    break;
                case ColorMessageCodes.IndianRed:
                    color = new SolidColorBrush(Colors.IndianRed);
                    break;
                case ColorMessageCodes.ForestGreen:
                    color = new SolidColorBrush(Colors.ForestGreen);
                    break;
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class Converters
    {
    }
}
