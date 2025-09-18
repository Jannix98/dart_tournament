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

        protected override bool UseWinnerInResult => false;

        public override Task SetToNextMatch(int currentRoundIndex, int currentMatchIndex, SelectWinnerResult winnerResult)
        {
            return base.SetToNextMatch(currentRoundIndex, currentMatchIndex, winnerResult);
        }

        /// <summary>
        /// Adds an eliminated player from the main tournament to the loser bracket.
        /// The eliminated player is treated as a participant, not as a loser.
        /// </summary>
        /// <param name="currentRoundIndex">The round index in the main tournament where the player was eliminated</param>
        /// <param name="currentMatchIndex">The match index in the main tournament where the player was eliminated</param>
        /// <param name="winnerResult">The result containing the eliminated player information</param>
        /// <returns></returns>
        public async Task AddEliminatedPlayer(int currentRoundIndex, int currentMatchIndex, SelectWinnerResult winnerResult)
        {
            var nextMatch = FindNextMatch(currentRoundIndex, currentMatchIndex, Matches);
            if (nextMatch != null)
            {
                // For eliminated players, we want to place the LOSER from the main tournament
                // as a participant in the loser bracket
                await SetEliminatedPlayerInMatch(winnerResult, currentMatchIndex, nextMatch);
            }
        }

        private async Task SetEliminatedPlayerInMatch(SelectWinnerResult winnerResult, int currentMatchIndex, MatchViewModel nextMatch)
        {
            if (currentMatchIndex % 2 == 0)
            {
                await UpdateFirstEntityWithEliminatedPlayer(winnerResult, nextMatch);
            }
            else
            {
                await UpdateSecondEntityWithEliminatedPlayer(winnerResult, nextMatch);
            }
        }

        private async Task UpdateFirstEntityWithEliminatedPlayer(SelectWinnerResult winnerResult, MatchViewModel nextMatch)
        {
            // For eliminated players, we use the LOSER from the main tournament result
            Guid firstId = winnerResult.LooserId;
            string entityName = winnerResult.LooserName;
            Guid secondId = nextMatch.IdGameEntityB;
            
            await UpdateMatch(nextMatch, firstId, secondId);
            nextMatch.IdGameEntityA = firstId;
            nextMatch.Player1Name = entityName;
        }

        private async Task UpdateSecondEntityWithEliminatedPlayer(SelectWinnerResult winnerResult, MatchViewModel nextMatch)
        {
            Guid firstId = nextMatch.IdGameEntityA;
            // For eliminated players, we use the LOSER from the main tournament result
            Guid secondId = winnerResult.LooserId;
            string entityName = winnerResult.LooserName;
            
            await UpdateMatch(nextMatch, firstId, secondId);
            nextMatch.IdGameEntityB = secondId;
            nextMatch.Player2Name = entityName;
        }

        private async Task UpdateMatch(MatchViewModel nextMatch, Guid firstId, Guid secondId)
        {
            var data = new DartTournament.Application.DTO.Match.GameMatchUpdateDto
            {
                Id = nextMatch.Id,
                IdGameEntityA = firstId,
                IdGameEntityB = secondId,
                WinnerId = nextMatch.WinnerId // Preserve existing winner if any
            };
            
            await matchPresentationService.UpdateMatchAsync(data);
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
