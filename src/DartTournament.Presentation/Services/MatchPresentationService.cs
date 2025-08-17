using DartTournament.Application.DTO.Match;
using DartTournament.Application.UseCases.Match.Services.Interfaces;
using DartTournament.Presentation.Base.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DartTournament.Presentation.Services
{
    public class MatchPresentationService : IMatchPresentationService
    {
        private readonly IMatchService _matchService;

        public MatchPresentationService(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public Task UpdateMatchAsync(GameMatchUpdateDto updateDto)
            => _matchService.UpdateMatchAsync(updateDto);
    }
}