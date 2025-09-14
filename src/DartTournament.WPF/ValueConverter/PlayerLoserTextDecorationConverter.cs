using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DartTournament.WPF.ValueConverter
{
    /// <summary>
    /// Multi-value converter that determines text decoration based on whether a player is the loser
    /// </summary>
    public class PlayerLoserTextDecorationConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
                return null;

            // values[0] should be the player's ID (Guid)
            // values[1] should be the winner ID (Guid)
            
            if (values[0] is Guid playerId && values[1] is Guid winnerId)
            {
                // If there's no winner yet, no decoration
                if (winnerId == Guid.Empty)
                    return null;
                    
                // Player is a loser if their ID doesn't match the winner ID
                bool isLoser = playerId != winnerId;
                return isLoser ? TextDecorations.Strikethrough : null;
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}