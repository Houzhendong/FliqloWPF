using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FliqloWPF.Converter
{
    public class WidthToFontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is double width)
            {
                return width * 0.9f;
            }

            return 80;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
