using DartTournament.WPF.NotifyPropertyChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Controls.GameTreeControl.MatchControl
{
    internal class MatchControlVM : NotifyPropertyChanged
    {
        MatchViewModel _match;

        public MatchViewModel Match { get => _match; set => SetProperty(ref _match, value); }
    }
}
