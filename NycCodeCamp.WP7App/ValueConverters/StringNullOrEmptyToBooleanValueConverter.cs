using System;
using System.Globalization;
using System.Windows.Data;

namespace NycCodeCamp.WP7App.ValueConverters
{
    public class StringNullOrEmptyToBooleanValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
