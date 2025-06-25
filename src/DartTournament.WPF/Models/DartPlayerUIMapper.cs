using DartTournament.Application.DTO.Player;

namespace DartTournament.WPF.Models
{
    internal static class DartPlayerUIMapper
    {
        public static DartPlayerInsertDto ToInsertDto(DartPlayerUI playerUI)
            => new DartPlayerInsertDto { Name = playerUI.Name };

        public static DartPlayerUpdateDto ToUpdateDto(DartPlayerUI playerUI)
            => new DartPlayerUpdateDto { Id = playerUI.Id, Name = playerUI.Name };

        public static DartPlayerUI FromGetDto(DartPlayerGetDto dto)
            => new DartPlayerUI(dto.Id, dto.Name);
    }
}