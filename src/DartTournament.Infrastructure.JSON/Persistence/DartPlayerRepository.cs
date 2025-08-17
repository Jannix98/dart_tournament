using DartTournament.Domain.Entities;
using DartTournament.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DartTournament.Infrastructure.JSON.Persistence
{
    public class DartPlayerRepository : BaseRepository<DartPlayer>, IDartPlayerRepository
    {
        protected override string FileName => "player.json";

        public DartPlayerRepository() : base()
        {
        }

        public async Task AddAsync(DartPlayer player)
        {
            var players = await GetAllAsync();
            players.Add(player);

            await SaveInFile(players);
        }

        public async Task Update(DartPlayer player)
        {
            var players = await GetAllAsync();
            int index = players.FindIndex(x => x.Id == player.Id);
            players[index] = player;

            await SaveInFile(players);
        }

        public async Task<DartPlayer?> GetByIdAsync(Guid id)
        {
            var players = await GetAllAsync();
            return players.FirstOrDefault(p => p.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var players = await GetAllAsync();
            var player = players.FirstOrDefault(p => p.Id == id);
            if (player != null)
            {
                players.Remove(player);
                await SaveInFile(players);
            }
        }

        Task<List<DartPlayer>> IDartPlayerRepository.GetAllAsync()
        {
            return base.GetAllAsync();
        }
    }
}
