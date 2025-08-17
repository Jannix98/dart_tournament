using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Domain.Entities
{
    public class GameMatch
    {
        private Guid _id;
        private Guid _idGameEntityA;
        private Guid _idGameEntityB;

        public GameMatch(Guid idGameEntityA, Guid idGameEntityB)
        {
            IdGameEntityA = idGameEntityA;
            IdGameEntityB = idGameEntityB;
        }

        public Guid Id { get => _id; set => _id = value; }
        public Guid IdGameEntityA { get => _idGameEntityA; set => _idGameEntityA = value; }
        public Guid IdGameEntityB { get => _idGameEntityB; set => _idGameEntityB = value; }
    }
}
