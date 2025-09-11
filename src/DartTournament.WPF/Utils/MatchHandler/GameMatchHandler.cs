using DartTournament.Application.DTO.Match;
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
    public class GameMatchHandler : GameMatchHandlerBase
    {
        LooserGameMatchHandler _looserGameMatchHandler;

        protected override bool UseWinnerInResult => true;

        /// <summary>
        /// this events notifies when a match has changed
        /// </summary>
        public event EventHandler NotifyMatchChange;
        

        public GameMatchHandler(List<MatchViewModel> matches, LooserGameMatchHandler looserMatchHandler, IMatchPresentationService matchPresentationService) : base(matches, matchPresentationService)
        {
            _looserGameMatchHandler = looserMatchHandler;
        }

        public override async Task SetToNextMatch(int currentRoundIndex, int currentMatchIndex, SelectWinnerResult winnerResult)
        {
            await base.SetToNextMatch(currentRoundIndex, currentMatchIndex, winnerResult);
            if (currentRoundIndex == 0 && _looserGameMatchHandler != null)
            {
                await _looserGameMatchHandler.SetToNextMatch(currentRoundIndex, currentMatchIndex, winnerResult);
            }
            NotifyMatchChange?.Invoke(null, null);
        }

        protected override MatchViewModel FindNextMatch(int currentRoundIndex, int currentMatchIndex, List<MatchViewModel> matches)
        {
            int newRoundIndex = currentRoundIndex + 1;
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
