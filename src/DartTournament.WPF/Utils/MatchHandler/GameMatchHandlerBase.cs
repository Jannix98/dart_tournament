using DartTournament.WPF.Controls.GameTreeControl;
using DartTournament.WPF.Dialogs.SelectWinner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DartTournament.WPF.Utils.MatchHandler
{
    public abstract class GameMatchHandlerBase
    {
        private List<MatchViewModel> matches;
        protected int _roundCount = 0;

        public List<MatchViewModel> Matches { get => matches; set => matches = value; }

        protected GameMatchHandlerBase(List<MatchViewModel> matches)
        {
            Matches = matches;
            _roundCount = matches.Max(m => m.RoundIndex) + 1;
        }

        public virtual void SetToNextMatch(int currentRoundIndex, int currentMatchIndex, SelectWinnerResult winnerResult)
        {
            var nextMatch = FindNextMatch(currentRoundIndex, currentMatchIndex, Matches);
            SetPlayerInMatch(winnerResult, currentMatchIndex, nextMatch);
        }

        protected abstract void SetPlayerInMatch(SelectWinnerResult winnerResult, int currentMatchIndex, MatchViewModel nextMatch);

        protected abstract MatchViewModel FindNextMatch(int currentRoundIndex, int currentMatchIndex, List<MatchViewModel> matches);

        protected int GetNextIndex(int inputIndex)
        {
            return inputIndex / 2;
        }
    }
}
