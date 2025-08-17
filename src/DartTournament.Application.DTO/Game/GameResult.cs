using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Application.DTO.Game
{
    public class GameResult
    {
        private Guid _id;
        private string _name;
        private bool _hasLooserRound;
        private GameDTO _mainGame;
        private GameDTO? _looserGame;

        public GameResult(Guid id, string name, bool hasLooserRound, GameDTO mainGame, GameDTO? looserGame)
        {
            _id = id;
            _name = name;
            _hasLooserRound = hasLooserRound;
            _mainGame = mainGame;
            _looserGame = looserGame;
        }

        public Guid Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public bool HasLooserRound { get => _hasLooserRound; set => _hasLooserRound = value; }
        public GameDTO MainGame { get => _mainGame; set => _mainGame = value; }
        public GameDTO? LooserGame { get => _looserGame; set => _looserGame = value; }
    }
}
