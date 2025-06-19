using DartTournament.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Domain.Entities
{
    internal class Game
    {
        private Guid _id;
        private string _name;
        private List<Guid> _rounds;
        private GameType _type;

        public Guid Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public List<Guid> Rounds { get => _rounds; set => _rounds = value; }
        public GameType Type { get => _type; set => _type = value; }
    }
}
