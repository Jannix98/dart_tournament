using DartTournament.Application.UseCases.Player.Services.Interfaces;
using DartTournament.Domain.Entities;
using DartTournament.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Application.UseCases.Player.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IDartPlayerRepository _playerRepository;

        public PlayerService(IDartPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<DartPlayer> CreatePlayerAsync(string name)
        {
            var player = new DartPlayer(name);
            await _playerRepository.AddAsync(player);
            return player;
        }

        public async Task<List<DartPlayer>> GetPlayerAsync()
        {
            return await _playerRepository.GetAllAsync();
        }
    }
}
