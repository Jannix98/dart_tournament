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
    public class PlayerRepository : IDartPlayerRepository
    {
        private readonly string FilePath = null;
        private const string FileName = "player.json";

        public PlayerRepository()
        {
            FilePath = PathManager.GetAndCreatePath(FileName);
        }


        public async Task AddAsync(DartPlayer player)
        {
            var players = await GetAllAsync();
            players.Add(player);

            await SaveInFile(players);
        }

        private async Task SaveInFile(List<DartPlayer> players)
        {
            var json = JsonSerializer.Serialize(players, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(FilePath, json);
        }

        public async Task<List<DartPlayer>> GetAllAsync()
        {
            if (!File.Exists(FilePath)) return new List<DartPlayer>();

            var json = await File.ReadAllTextAsync(FilePath);
            if(String.IsNullOrEmpty(json))
                return new List<DartPlayer>();

            return JsonSerializer.Deserialize<List<DartPlayer>>(json) ?? new List<DartPlayer>();
        }

        public async Task Update(DartPlayer player)
        {
            var players = await GetAllAsync();
            int index = players.FindIndex(x => x.Id == player.Id);
            players[index] = player;

            await SaveInFile(players);
        }
    }
}
