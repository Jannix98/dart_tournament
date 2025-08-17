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

        public GameMatchDTO(Guid id, Guid playerAId, Guid playerBId)
        {
            _id = id;
            _playerAId = playerAId;
            _playerBId = playerBId;
        }

        public Guid Id { get => _id; set => _id = value; }
        public Guid PlayerAId { get => _playerAId; set => _playerAId = value; }
        public Guid PlayerBId { get => _playerBId; set => _playerBId = value; }
    }
}