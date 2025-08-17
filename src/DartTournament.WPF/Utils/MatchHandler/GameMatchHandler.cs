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
        /// <summary>
        /// this events notifies when a match has changed
        /// </summary>
        public event EventHandler NotifyMatchChange;
        private IMatchPresentationService _matchPresentationService;

        public GameMatchHandler(List<MatchViewModel> matches, LooserGameMatchHandler looserMatchHandler) : base(matches)
        {
            _looserGameMatchHandler = looserMatchHandler;
            _matchPresentationService = SM.ServiceManager.Instance.GetRequiredService<IMatchPresentationService>();
        }

        public override void SetToNextMatch(int currentRoundIndex, int currentMatchIndex, SelectWinnerResult winnerResult)
        {
            base.SetToNextMatch(currentRoundIndex, currentMatchIndex, winnerResult);
            if (currentRoundIndex == 0 && _looserGameMatchHandler != null)
            {
                _looserGameMatchHandler.SetToNextMatch(currentRoundIndex, currentMatchIndex, winnerResult);
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

        protected async override void SetPlayerInMatch(SelectWinnerResult winnerResult, int currentIndex, MatchViewModel nextMatch)
        {
            if (currentIndex % 2 == 0)
            {
                Guid firstId = winnerResult.WinnerId;
                Guid secondId = nextMatch.IdGameEntityB;
                await UpdateMatch(nextMatch, firstId, secondId);
                nextMatch.Player1Name = winnerResult.WinnerName;
            }
            else
            {
                Guid firstId = nextMatch.IdGameEntityA;
                Guid secondId = winnerResult.WinnerId;
                await UpdateMatch(nextMatch, firstId, secondId);
                nextMatch.Player2Name = winnerResult.WinnerName;
            }

        }

        private async Task UpdateMatch(MatchViewModel nextMatch, Guid firstId, Guid secondId)
        {
            GameMatchUpdateDto data = new GameMatchUpdateDto
            {
                Id = nextMatch.Id,
                IdGameEntityA = firstId,
                IdGameEntityB = secondId
            };
            await _matchPresentationService.UpdateMatchAsync(data);
        }
    }


}
