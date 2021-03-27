using System;
using System.Globalization;
using System.Windows.Data;

namespace FliqloWPF.Converter
{
    public class WidthToSmallFontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is double width)
            {
                return width * 0.1f;
            }
            return 80;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
