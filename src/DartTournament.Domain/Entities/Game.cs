using DartTournament.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DartTournament.Domain.Entities
{
    public class Game
    {
        private Guid _id;
        private List<Guid> _roundIds;
        private List<GameRound> _rounds;
        private GameType _type;

        public Game(List<GameRound> rounds)
        {
            _rounds = rounds;
            _type = GameType.PlayerGame;
        }

        public Guid Id { get => _id; set => _id = value; }
        public List<Guid> RoundIds { get => _roundIds; set => _roundIds = value; }
        [JsonIgnore]
        public List<GameRound> Rounds 
        { 
            get => _rounds; 
            set
            {
                _rounds = value;
            } 
        }
        public GameType Type { get => _type; set => _type = value; }
    }
}
