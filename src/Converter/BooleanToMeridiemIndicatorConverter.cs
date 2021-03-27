using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FliqloWPF.Converter
{
    public class BooleanToMeridiemIndicatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if((bool)value)
            {
                return "AM";
            }
            else
            {
                return "PM";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
