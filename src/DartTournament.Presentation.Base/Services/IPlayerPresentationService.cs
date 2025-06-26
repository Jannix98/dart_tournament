using DartTournament.Application.DTO.Player;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DartTournament.Presentation.Base.Services
{
    public interface IPlayerPresentationService
    {
        Task<DartPlayerGetDto> CreatePlayerAsync(DartPlayerInsertDto insertDto);
        Task UpdatePlayerAsync(DartPlayerUpdateDto updateDto);
        Task<List<DartPlayerGetDto>> GetPlayerAsync();
        Task DeletePlayerAsync(Guid id); 
    }
}