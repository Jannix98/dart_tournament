using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Application.DTO.Game
{
    public class GameMatchDTO
    {
        private Guid _id;
        private Guid _playerAId;
        private Guid _playerBId;
        private Guid _winnerId;
        private string _playerAName;
        private string _playerBName;

        public GameMatchDTO(Guid id, Guid playerAId, Guid playerBId, Guid winnderId, string playerAName, string playerBName)
        {
            _id = id;
            _playerAId = playerAId;
            _playerBId = playerBId;
            _playerAName = playerAName;
            _playerBName = playerBName;
            _winnerId = winnderId;
        }

        public Guid Id { get => _id; set => _id = value; }
        public Guid PlayerAId { get => _playerAId; set => _playerAId = value; }
        public Guid PlayerBId { get => _playerBId; set => _playerBId = value; }
        public string PlayerAName { get => _playerAName; set => _playerAName = value; }
        public string PlayerBName { get => _playerBName; set => _playerBName = value; }
        public Guid WinnerId { get => _winnerId; set => _winnerId = value; }
    }
}