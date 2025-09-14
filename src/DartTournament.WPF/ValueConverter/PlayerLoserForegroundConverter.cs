using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DartTournament.WPF.ValueConverter
{
    /// <summary>
    /// Multi-value converter that determines foreground color based on whether a player is the loser
    /// </summary>
    public class PlayerLoserForegroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
                return Brushes.Black;

            // values[0] should be the player's ID (Guid)
            // values[1] should be the winner ID (Guid)
            
            if (values[0] is Guid playerId && values[1] is Guid winnerId)
            {
                // If there's no winner yet, use default color
                if (winnerId == Guid.Empty)
                    return Brushes.Black;
                    
                // Player is a loser if their ID doesn't match the winner ID
                bool isLoser = playerId != winnerId;
                return isLoser ? Brushes.Gray : Brushes.Black;
            }

            return Brushes.Black;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}