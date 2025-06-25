using DartTournament.Domain.Entities;
using DartTournament.Application.DTO.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Application.UseCases.Player.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<DartPlayerGetDto> CreatePlayerAsync(DartPlayerInsertDto insertDto);
        Task UpdatePlayerAsync(DartPlayerUpdateDto updateDto);
        Task<List<DartPlayerGetDto>> GetPlayerAsync();
    }
}
