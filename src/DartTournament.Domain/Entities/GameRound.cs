using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DartTournament.Domain.Entities
{
    public class GameRound
    {
        private Guid _id;
        private int _roundNumber;
        private List<Guid> _matchIds;
        private List<GameMatch> _matches;

        public GameRound(int roundNumber, List<GameMatch> matches)
        {
            _roundNumber = roundNumber;
            _matches = matches ?? new List<GameMatch>();
        }

        public List<Guid> MatchIds { get => _matchIds; set => _matchIds = value; }
        [JsonIgnore]
        public List<GameMatch> Matches
        {
            get => _matches;
            set
            {
                _matches = value;
            }
        }

        public Guid Id { get => _id; set => _id = value; }
        public int RoundNumber { get => _roundNumber; set => _roundNumber = value; }
    }
}
