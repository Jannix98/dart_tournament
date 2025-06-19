using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Domain.Entities
{
    internal class GameMatch
    {
        private Guid _id;
        private Guid _idGameEntityA;
        private string _nameGameEntityA;
        private int _scoreGameEntityA;
        private Guid _idGameEntityB;
        private string _nameGameEntityB;
        private int _scoreGameEntityB;

        public GameMatch(Guid id, Guid idGameEntityA, string nameGameEntityA, int scoreGameEntityA, Guid idGameEntityB, string nameGameEntityB, int scoreGameEntityB)
        {
            Id = id;
            IdGameEntityA = idGameEntityA;
            NameGameEntityA = nameGameEntityA;
            ScoreGameEntityA = scoreGameEntityA;
            IdGameEntityB = idGameEntityB;
            NameGameEntityB = nameGameEntityB;
            ScoreGameEntityB = scoreGameEntityB;
        }

        public Guid Id { get => _id; set => _id = value; }
        public Guid IdGameEntityA { get => _idGameEntityA; set => _idGameEntityA = value; }
        public string NameGameEntityA { get => _nameGameEntityA; set => _nameGameEntityA = value; }
        public int ScoreGameEntityA { get => _scoreGameEntityA; set => _scoreGameEntityA = value; }
        public Guid IdGameEntityB { get => _idGameEntityB; set => _idGameEntityB = value; }
        public string NameGameEntityB { get => _nameGameEntityB; set => _nameGameEntityB = value; }
        public int ScoreGameEntityB { get => _scoreGameEntityB; set => _scoreGameEntityB = value; }
    }
}
