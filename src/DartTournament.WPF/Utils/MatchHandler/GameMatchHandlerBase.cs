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
        protected abstract bool UseWinnerInResult { get;  }

        public List<MatchViewModel> Matches { get => matches; set => matches = value; }

        protected GameMatchHandlerBase(List<MatchViewModel> matches)
        {
            Matches = matches;
            _roundCount = matches.Max(m => m.RoundIndex) + 1;
            matchPresentationService = SM.ServiceManager.Instance.GetRequiredService<IMatchPresentationService>();
        }

        public virtual async Task SetToNextMatch(int currentRoundIndex, int currentMatchIndex, SelectWinnerResult winnerResult)
        {
            var nextMatch = FindNextMatch(currentRoundIndex, currentMatchIndex, Matches);
            await SetPlayerInMatch(winnerResult, currentMatchIndex, nextMatch);
        }

        protected async Task SetPlayerInMatch(SelectWinnerResult winnerResult, int currentMatchIndex, MatchViewModel nextMatch)
        {
            if (currentMatchIndex % 2 == 0)
            {
                await UpdateFirstEntityInMatch(winnerResult, nextMatch);
            }
            else
            {
                await UpdateSecondEntityInMatch(winnerResult, nextMatch);
            }

        }

        private async Task UpdateSecondEntityInMatch(SelectWinnerResult winnerResult, MatchViewModel nextMatch)
        {
            Guid firstId = nextMatch.IdGameEntityA;
            (Guid secondId, string entityName) = GetEntityFromResult(winnerResult);
            await UpdateMatch(nextMatch, firstId, secondId);
            nextMatch.IdGameEntityB = secondId;
            nextMatch.Player2Name = entityName;
        }

        private async Task UpdateFirstEntityInMatch(SelectWinnerResult winnerResult, MatchViewModel nextMatch)
        {
            (Guid firstId, string entityName) = GetEntityFromResult(winnerResult);
            Guid secondId = nextMatch.IdGameEntityB;
            await UpdateMatch(nextMatch, firstId, secondId);
            nextMatch.IdGameEntityA = firstId;
            nextMatch.Player1Name = entityName;
        }

        private (Guid entityId, string entityName) GetEntityFromResult(SelectWinnerResult winnerResult)
        {
            if (UseWinnerInResult)
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
                IdGameEntityB = secondId
            };
            Trace.WriteLine($"About to match {nextMatch.Id} with players {firstId} and {secondId}");
            await matchPresentationService.UpdateMatchAsync(data);
            Trace.WriteLine($"Updated match {nextMatch.Id} with players {firstId} and {secondId}");
        }

        protected abstract MatchViewModel FindNextMatch(int currentRoundIndex, int currentMatchIndex, List<MatchViewModel> matches);

        protected int GetNextIndex(int inputIndex)
        {
            return inputIndex / 2;
        }
    }
}
