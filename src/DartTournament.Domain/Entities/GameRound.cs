using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Domain.Entities
{
    internal class GameRound
    {
        private Guid _id;
        private List<Guid> _matches;


        public List<Guid> Matches { get => _matches; set => _matches = value; }

        public Guid Id { get => _id; set => _id = value; }
    }
}
