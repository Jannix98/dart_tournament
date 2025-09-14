using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DartTournament.WPF.ValueConverter
{
    /// <summary>
    /// Converter that converts a boolean indicating if a player is a loser to a grayed out brush
    /// </summary>
    public class LoserToForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isLoser && isLoser)
            {
                return Brushes.Gray;
            }

            return Brushes.Black; // Default text color
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}