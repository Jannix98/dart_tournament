using DartTournament.Presentation.Base.Services;
using DartTournament.WPF.Controls.GameTreeControl;
using DartTournament.WPF.Dialogs.SelectWinner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Utils.MatchHandler
{
    public class LooserGameMatchHandler : GameMatchHandlerBase
    {
        public LooserGameMatchHandler(List<MatchViewModel> matches, IMatchPresentationService matchPresentationService) : base(matches, matchPresentationService)
        {
        }

        public override Task SetWinnerToNextMatch(int currentRoundIndex, int currentMatchIndex, SelectWinnerResult winnerResult)
        {
            (Guid entityId, string entityName) = GetEntityFromResult(winnerResult, true);
            SetMatchEntityData matchData = new SetMatchEntityData(entityId, entityName);
            return SetWinnerToNextMatch(currentRoundIndex, currentMatchIndex, winnerResult);
        }

        protected override MatchViewModel FindNextMatch(int currentRoundIndex, int currentMatchIndex, List<MatchViewModel> matches)
        {
            int newRoundIndex = currentRoundIndex;
            if (newRoundIndex >= _roundCount)
                return null;

            int nextMatchIndex = GetNextIndex(currentMatchIndex);
            var newMatch = matches.Where(m => m.RoundIndex == newRoundIndex && m.MatchIndex == nextMatchIndex)
                .FirstOrDefault();

            if (newMatch == null)
                throw new InvalidOperationException($"No match found for Round {newRoundIndex}, Match {nextMatchIndex}.");

            return newMatch;
        }
    }
}
