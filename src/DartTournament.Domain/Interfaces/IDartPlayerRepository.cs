using DartTournament.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Domain.Interfaces
{
    public interface IDartPlayerRepository
    {
        Task AddAsync(DartPlayer player);
        Task<List<DartPlayer>> GetAllAsync();
        Task Update(DartPlayer player);
        Task<DartPlayer?> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
