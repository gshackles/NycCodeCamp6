using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NycCodeCamp.WP7App.ValueConverters
{
    public class StringNullOrEmptyToVisibilityValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty(value.ToString())
                    ? Visibility.Collapsed
                    : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
