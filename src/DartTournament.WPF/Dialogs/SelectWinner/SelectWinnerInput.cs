using DartTournament.WPF.Controls.GameTreeControl;
using DartTournament.WPF.Dialogs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Dialogs.SelectWinner
{
    internal class SelectWinnerInput : BaseDialogInput
    {
        MatchViewModel _match;
        public MatchViewModel Match
        {
            get => _match;
        }
        public SelectWinnerInput(MatchViewModel match)
        {
            _match = match ?? throw new ArgumentNullException(nameof(match));
        }
    }
}
