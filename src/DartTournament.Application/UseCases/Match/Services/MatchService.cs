using DartTournament.Application.DTO.Match;
using DartTournament.Application.UseCases.Match.Mappers;
using DartTournament.Application.UseCases.Match.Services.Interfaces;
using DartTournament.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DartTournament.Application.UseCases.Match.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;

        public MatchService(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task UpdateMatchAsync(GameMatchUpdateDto updateDto)
        {
            var match = GameMatchMapper.ToEntity(updateDto);
            await _matchRepository.Update(match);
        }
    }
}