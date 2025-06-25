using DartTournament.Application.DTO.Player;
using DartTournament.Domain.Entities;

namespace DartTournament.Application.UseCases.Player.Mappers
{
    public static class DartPlayerMapper
    {
        public static DartPlayerGetDto ToGetDto(DartPlayer player) =>
            new DartPlayerGetDto { Id = player.Id, Name = player.Name };

        public static DartPlayer ToEntity(DartPlayerInsertDto dto) =>
            new DartPlayer(dto.Name);

        public static void UpdateEntity(DartPlayer player, DartPlayerUpdateDto dto)
        {
            player.Name = dto.Name;
        }

        public static DartPlayerGetDto MapAndReturn(DartPlayerInsertDto insertDto)
        {
            var player = DartPlayerMapper.ToEntity(insertDto);
            return DartPlayerMapper.ToGetDto(player);
        }
    }
}