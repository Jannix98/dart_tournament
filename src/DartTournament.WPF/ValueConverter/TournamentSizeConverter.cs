using DartTournament.WPF.Dialogs.CreateGame;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DartTournament.WPF.ValueConverter
{
    internal class TournamentSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            return value.Equals(Enum.Parse(typeof(TournamentSize), parameter.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Enum.Parse(typeof(TournamentSize), parameter.ToString());

            return Binding.DoNothing;
        }
    }
}
