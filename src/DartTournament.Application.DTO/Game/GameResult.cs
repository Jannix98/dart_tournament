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
        private DateTime _creationDate;
        private string _name;
        private bool _hasLooserRound;
        private GameDTO _mainGame;
        private GameDTO? _looserGame;

        public GameResult(Guid id, DateTime creationDate, string name, bool hasLooserRound, GameDTO mainGame, GameDTO? looserGame)
        {
            _id = id;
            CreationDate = creationDate;
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
        public DateTime CreationDate { get => _creationDate; set => _creationDate = value; }
    }
}
