using DartTournament.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Application.UseCases.Player.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<DartPlayer> CreatePlayerAsync(string name);
        Task UpdatePlayerAsync(DartPlayer dartPlayer);
        Task<List<DartPlayer>> GetPlayerAsync();
    }
}
