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
    class CountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int count)
            {
                // If count is greater than 0, return Visible; otherwise, return Collapsed
                bool result = count > 0;
                result = IsInvert(parameter) ? !result : result;
                return result ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            }
            return DependencyProperty.UnsetValue;
        }

        private bool IsInvert(object parameter)
        {
            return parameter?.ToString()?.ToUpper()?.Contains("INVERT") ?? false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
