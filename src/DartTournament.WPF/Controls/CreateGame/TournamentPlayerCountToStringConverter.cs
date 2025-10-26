using System;
using System.Globalization;
using System.Windows.Data;
using DartTournament.WPF.Models.Enums;

namespace DartTournament.WPF.Controls.CreateGame;
public class TournamentPlayerCountToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is TournamentPlayerCount count)
        {
            // You can customize the display text here
            return $"{(int)count} Players";
        }
        return value?.ToString() ?? string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}