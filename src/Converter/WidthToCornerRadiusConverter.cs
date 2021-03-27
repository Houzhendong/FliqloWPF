using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FliqloWPF.Converter
{
    public class WidthToCornerRadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is double width)
            {
                return  new CornerRadius(width / 15);
            }

            return new CornerRadius(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
