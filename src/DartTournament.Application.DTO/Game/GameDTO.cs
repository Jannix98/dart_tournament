using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Application.DTO.Game
{
    public class GameDTO
    {
        private Guid _id;
        private List<GameRoundDTO> _rounds;

        public GameDTO(Guid id, List<GameRoundDTO> rounds)
        {
            _id = id;
            _rounds = rounds ?? new List<GameRoundDTO>();
        }

        public Guid Id { get => _id; set => _id = value; }
        public List<GameRoundDTO> Rounds { get => _rounds; set => _rounds = value; }
    }
}