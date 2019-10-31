using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SampleApp.Converters
{
    public class ModelErrorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return string.Empty;

            var errors = value as Dictionary<string, List<string>>;
            var propertyName = parameter?.ToString();

            var firstError = errors.FirstOrDefault(k => k.Key == propertyName).Value?.FirstOrDefault();
            return firstError;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
