using System;
using System.Globalization;
using System.Windows.Data;

namespace FliqloWPF.Converter
{
    public class FlipNumberSizeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double scale = (double)values[0];
            double height = (double)values[1];
            return scale * height;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
