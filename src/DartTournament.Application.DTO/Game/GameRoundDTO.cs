using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Application.DTO.Game
{
    public class GameRoundDTO
    {
        private Guid _id;
        private int _roundNumber;
        private List<GameMatchDTO> _matches;

        public GameRoundDTO(Guid id, int roundNumber, List<GameMatchDTO> matches)
        {
            _id = id;
            _roundNumber = roundNumber;
            _matches = matches ?? new List<GameMatchDTO>();
        }

        public Guid Id { get => _id; set => _id = value; }
        public int RoundNumber { get => _roundNumber; set => _roundNumber = value; }
        public List<GameMatchDTO> Matches { get => _matches; set => _matches = value; }
    }
}