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
    public class TeamRepository : ITeamRepository
    {
        private readonly string FilePath = null;
        private const string FileName = "teams.json";

        public TeamRepository()
        {
            FilePath = PathManager.GetAndCreatePath(FileName);
        }


        public async Task AddAsync(Team team)
        {
            var teams = await GetAllAsync();
            teams.Add(team);

            var json = JsonSerializer.Serialize(teams, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(FilePath, json);
        }

        public async Task<List<Team>> GetAllAsync()
        {
            if (!File.Exists(FilePath)) return new List<Team>();

            var json = await File.ReadAllTextAsync(FilePath);
            if(String.IsNullOrEmpty(json))
                return new List<Team>();

            return JsonSerializer.Deserialize<List<Team>>(json) ?? new List<Team>();
        }
    }
}
