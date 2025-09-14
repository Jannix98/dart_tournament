using DartTournament.Application.DTO.Match;
using DartTournament.Domain.Entities;

namespace DartTournament.Application.UseCases.Match.Mappers
{
    public static class GameMatchMapper
    {

        public static GameMatch ToEntity(GameMatchUpdateDto dto)
        {
            return new GameMatch(dto.IdGameEntityA, dto.IdGameEntityB, dto.WinnerId)
            {
                Id = dto.Id
            };
        }
    }
}