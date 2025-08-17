using DartTournament.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Domain.Interfaces
{
    public interface IGameRepository
    {
        Task Insert(Game game);
    }
}
