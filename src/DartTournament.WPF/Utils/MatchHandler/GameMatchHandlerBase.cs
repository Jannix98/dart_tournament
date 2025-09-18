using DartTournament.Application.DTO.Match;
using DartTournament.Presentation.Base.Services;
using DartTournament.Presentation.Services;
using DartTournament.WPF.Controls.GameTreeControl;
using DartTournament.WPF.Dialogs.SelectWinner;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        protected IMatchPresentationService matchPresentationService;
        
        public List<MatchViewModel> Matches { get => matches; set => matches = value; }

        protected GameMatchHandlerBase(List<MatchViewModel> matches, IMatchPresentationService matchPresentationService)
        {
            Matches = matches;
            _roundCount = matches.Max(m => m.RoundIndex) + 1;
            this.matchPresentationService = matchPresentationService;
        }

        protected async Task SetWinnerToNextMatch(int currentRoundIndex, int currentMatchIndex, SetMatchEntityData matchData)
        {
            // Update the current match with the winner
            await SetWinnerInCurrentMatch(currentRoundIndex, currentMatchIndex, matchData);
            await SetPlayerToNextMatch(currentRoundIndex, currentMatchIndex, matchData);
        }

        public async Task SetPlayerToNextMatch(int currentRoundIndex, int currentMatchIndex, SetMatchEntityData matchData)
        {
            var nextMatch = FindNextMatch(currentRoundIndex, currentMatchIndex, Matches);
            if (nextMatch != null)
            {
                await SetPlayerInMatch(matchData, currentMatchIndex, nextMatch);
            }
        }

        protected async Task SetWinnerInCurrentMatch(int currentRoundIndex, int currentMatchIndex, SetMatchEntityData matchData)
        {
            var currentMatch = Matches.FirstOrDefault(m => m.RoundIndex == currentRoundIndex && m.MatchIndex == currentMatchIndex);
            if (currentMatch != null)
            {
                currentMatch.WinnerId = matchData.Entityid;
                
                GameMatchUpdateDto data = new GameMatchUpdateDto
                {
                    Id = currentMatch.Id,
                    IdGameEntityA = currentMatch.IdGameEntityA,
                    IdGameEntityB = currentMatch.IdGameEntityB,
                    WinnerId = matchData.Entityid
                };
                Trace.WriteLine($"About to update match {currentMatch.Id} with winner {matchData.Entityid}");
                await matchPresentationService.UpdateMatchAsync(data);
                Trace.WriteLine($"Updated match {currentMatch.Id} with winner {matchData.Entityid}");
            }
        }

        private async Task SetPlayerInMatch(SetMatchEntityData matchData, int currentMatchIndex, MatchViewModel nextMatch)
        {
            if (currentMatchIndex % 2 == 0)
            {
                await UpdateFirstEntityInMatch(matchData, nextMatch);
            }
            else
            {
                await UpdateSecondEntityInMatch(matchData, nextMatch);
            }

        }

        private async Task UpdateSecondEntityInMatch(SetMatchEntityData matchData, MatchViewModel nextMatch)
        {
            Guid firstId = nextMatch.IdGameEntityA;
            await UpdateMatch(nextMatch, firstId, matchData.Entityid);
            nextMatch.IdGameEntityB = matchData.Entityid;
            nextMatch.Player2Name = matchData.EntityName;
        }

        private async Task UpdateFirstEntityInMatch(SetMatchEntityData matchData, MatchViewModel nextMatch)
        {
            Guid secondId = nextMatch.IdGameEntityB;
            await UpdateMatch(nextMatch, matchData.Entityid, secondId);
            nextMatch.IdGameEntityA = matchData.Entityid;
            nextMatch.Player1Name = matchData.EntityName;
        }

        protected (Guid entityId, string entityName) GetEntityFromResult(SelectWinnerResult winnerResult, bool useWinnerInResult)
        {
            if (useWinnerInResult)
            {
                return (winnerResult.WinnerId, winnerResult.WinnerName);
            }
            else
            {
                return (winnerResult.LooserId, winnerResult.LooserName);
            }
        }

        private async Task UpdateMatch(MatchViewModel nextMatch, Guid firstId, Guid secondId)
        {
            GameMatchUpdateDto data = new GameMatchUpdateDto
            {
                Id = nextMatch.Id,
                IdGameEntityA = firstId,
                IdGameEntityB = secondId,
                WinnerId = nextMatch.WinnerId // Preserve existing winner if any
            };
            Trace.WriteLine($"About to match {nextMatch.Id} with players {firstId} and {secondId}");
            await matchPresentationService.UpdateMatchAsync(data);
            Trace.WriteLine($"Updated match {nextMatch.Id} with players {firstId} and {secondId}");
        }

        protected abstract MatchViewModel FindNextMatch(int currentRoundIndex, int currentMatchIndex, List<MatchViewModel> matches);
        public abstract Task SetWinnerToNextMatch(int currentRoundIndex, int currentMatchIndex, SelectWinnerResult winnerResult);

        protected int GetNextIndex(int inputIndex)
        {
            return inputIndex / 2;
        }
    }
}
