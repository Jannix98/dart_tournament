using DartTournament.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Domain.Interfaces
{
    public interface IMatchRepository
    {
        Task Insert(GameMatch match);
        Task Update(GameMatch match);
        Task InsertRange(List<GameMatch> matches);
        Task<List<GameMatch>> GetByIdsAsync(List<Guid> ids);
    }
}
