using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DartTournament.Domain.Entities
{
    public class GameParent
    {
        Guid _id;
        string _name;
        Guid _mainGameId;
        Game _mainGame;
        Guid? _looserGameId;
        Game? _looserGame;
        bool _hasLooserRound;

        public GameParent(string name, Game mainGame, Game looserGame, bool hasLooserRound)
        {
            _name = name;
            _mainGame = mainGame;
            _looserGame = looserGame;
            _hasLooserRound = hasLooserRound;
        }

        public Guid Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public bool HasLooserRound { get => _hasLooserRound; set => _hasLooserRound = value; }
        [JsonIgnore]
        public Game MainGame 
        { 
            get => _mainGame; 
            set
            {
                _mainGame = value;
            } 
        }
        [JsonIgnore]
        public Game? LooserGame 
        { 
            get => _looserGame; 
            set
            {
                _looserGame = value;
            } 
        }
        public Guid MainGameId { get => _mainGameId; set => _mainGameId = value; }
        public Guid? LooserGameId { get => _looserGameId; set => _looserGameId = value; }
    }
}
