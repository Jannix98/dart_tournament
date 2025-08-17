using DartTournament.Domain.Entities;
using DartTournament.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DartTournament.Infrastructure.JSON.Persistence
{
    public class MatchRepository : BaseRepository<GameMatch>, IMatchRepository
    {
        protected override string FileName => "matches.json";

        public async Task Insert(GameMatch match)
        {
            var all = await GetAllAsync();
            all.Add(match);
            await SaveInFile(all);
        }

        public async Task InsertRange(List<GameMatch> matches)
        {
            var all = await GetAllAsync();
            all.AddRange(matches);
            await SaveInFile(all);
        }

        public async Task Update(GameMatch match)
        {
            var all = await GetAllAsync();
            var index = all.FindIndex(m => m.Id == match.Id);
            if (index >= 0)
            {
                all[index] = match;
                await SaveInFile(all);
            }
            else
            {
                throw new KeyNotFoundException($"Match with ID {match.Id} not found.");
            }
        }

        public async Task<List<GameMatch>> GetByIdsAsync(List<Guid> ids)
        {
            var all = await GetAllAsync();
            return all.Where(m => ids.Contains(m.Id)).ToList();
        }
    }
}
