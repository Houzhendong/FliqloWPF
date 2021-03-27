using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FliqloWPF.Converter
{
    public class WidthToSmallFontMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is double width)
            {
                return new Thickness(width / 20.0f);
            }
            return new Thickness(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
