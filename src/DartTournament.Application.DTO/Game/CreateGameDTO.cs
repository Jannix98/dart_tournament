using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Application.DTO.Game
{
    public class CreateGameDTO
    {
        string _name;
        int _numberOfPlayers;
        List<Guid> _playerIds;
        bool _hasLooserRound;

        public CreateGameDTO(string name, int numberOfPlayers, List<Guid> playerIds, bool hasLooserRound)
        {
            _name = name;
            _numberOfPlayers = numberOfPlayers;
            _playerIds = playerIds ?? new List<Guid>();
            _hasLooserRound = hasLooserRound;
        }

        public string Name { get => _name; set => _name = value; }
        public int NumberOfPlayers { get => _numberOfPlayers; set => _numberOfPlayers = value; }
        public List<Guid> PlayerIds { get => _playerIds; set => _playerIds = value; }
        public bool HasLooserRound { get => _hasLooserRound; set => _hasLooserRound = value; }
    }
}
