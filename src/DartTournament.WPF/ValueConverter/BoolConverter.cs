using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DartTournament.WPF.ValueConverter
{
    internal class BoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool )
            {
                throw new ArgumentException("Value must be a boolean", nameof(value));
            }

            bool boolValue = (bool)value;

            if (targetType == typeof(Visibility))
            {
                BoolToVisibilityConverter boolToVisibilityConverter = new BoolToVisibilityConverter();
                return boolToVisibilityConverter.Convert(boolValue, targetType, parameter, culture);
            }
            else if(targetType == typeof(bool))
            { 
                return IsInverse(parameter) ? !boolValue : boolValue;
            }
            else
            {
                return DependencyProperty.UnsetValue;
            }
        }

        private bool IsInverse(object parameter)
        {
            return parameter?.ToString()?.ToUpper()?.Contains("INVERSE") ?? false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
