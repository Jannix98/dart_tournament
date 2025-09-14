using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace DartTournament.WPF.ValueConverter
{
    /// <summary>
    /// Converter that converts a boolean indicating if a player is a loser to text decoration (strikethrough)
    /// </summary>
    public class LoserToTextDecorationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isLoser && isLoser)
            {
                return TextDecorations.Strikethrough;
            }

            return null; // No text decoration
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}