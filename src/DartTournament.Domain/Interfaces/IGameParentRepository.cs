using DartTournament.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Domain.Interfaces
{
    public interface IGameParentRepository
    {
        Task<Guid> CreateGameParent(GameParent gameParent);
        Task<GameParent> GetGameParent(Guid id);
        Task<List<GameParent>> GetAllGameParents();
        Task<List<GameParent>> GetAllGameParentsWithProperties();
    }
}
