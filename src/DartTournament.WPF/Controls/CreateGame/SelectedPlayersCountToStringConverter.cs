using System;
using System.Globalization;
using System.Windows.Data;
using DartTournament.WPF.Models.Enums;
using DartTournament.WPF.Resources;

namespace DartTournament.WPF.Controls.CreateGame
{
    public class SelectedPlayersCountToStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
                return Labels.SelectedPlayers; 

            int selected = values[0] is int s ? s : 0;
            int total = 0;

            // SelectedTotalPlayers is of type TournamentPlayerCount, convert to int
            if (values[1] != null)
            {
                if (values[1] is int t)
                    total = t;
                else if (values[1] is TournamentPlayerCount tournamentPlayerCount)
                    total = (int)tournamentPlayerCount;
            }

            return $"{Labels.SelectedPlayers} ({selected}/{total})";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
