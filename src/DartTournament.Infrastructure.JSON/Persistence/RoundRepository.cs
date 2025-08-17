using DartTournament.Domain.Entities;
using DartTournament.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Infrastructure.JSON.Persistence
{
    public class RoundRepository : BaseRepository<GameRound>, IRoundRepository
    {
        protected override string FileName => "rounds.json";

        public async Task Insert(GameRound round)
        {
            var all = await GetAllAsync();
            all.Add(round);
            await SaveInFile(all);
        }

        public async Task InsertRange(List<GameRound> rounds)
        {
            var all = await GetAllAsync();
            all.AddRange(rounds);
            await SaveInFile(all);
        }
    }
}
