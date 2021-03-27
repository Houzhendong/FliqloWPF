using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FliqloWPF.Converter
{
    public class HalfHeightToMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is double height)
            {
                return new Thickness(0, -height/2, 0, 0);
            }

            return new Thickness(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
