using System;
using System.Globalization;
using System.Windows.Data;

namespace DartTournament.WPF.ValueConverter
{
    /// <summary>
    /// Converter that determines if a player is the loser in a match
    /// </summary>
    public class PlayerIsLoserConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
                return false;

            // values[0] should be the player's ID (Guid)
            // values[1] should be the winner ID (Guid)
            
            if (values[0] is Guid playerId && values[1] is Guid winnerId)
            {
                // If there's no winner yet, nobody is a loser
                if (winnerId == Guid.Empty)
                    return false;
                    
                // Player is a loser if their ID doesn't match the winner ID
                return playerId != winnerId;
            }

            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}