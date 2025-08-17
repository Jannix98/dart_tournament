using DartTournament.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Domain.Interfaces
{
    public interface IRoundRepository
    {
        Task Insert(GameRound round);
        Task InsertRange(List<GameRound> rounds);
    }
}
