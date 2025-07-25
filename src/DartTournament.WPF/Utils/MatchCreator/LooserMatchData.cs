using DartTournament.WPF.Controls.GameTreeControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Utils.MatchCreator
{
    internal class LooserMatchData
    {
        public List<RoundViewModel> GameRounds { get; internal set; }
        public List<RoundViewModel> LooserRounds { get; internal set; }
    }
}
