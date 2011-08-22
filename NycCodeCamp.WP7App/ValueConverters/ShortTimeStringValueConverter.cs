using System;
using System.Globalization;
using System.Windows.Data;

namespace NycCodeCamp.WP7App.ValueConverters
{
    public class ShortTimeStringValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime) value).ToLocalTime().ToShortTimeString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
