using DartTournament.Domain.Entities;
using DartTournament.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Infrastructure.JSON.Persistence
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        protected override string FileName => "games.json";

        public async Task Insert(Game game)
        {
            var all = await GetAllAsync();
            all.Add(game);
            await SaveInFile(all);
        }
    }
}
