using DartTournament.Application.DTO.Game;
using DartTournament.Application.UseCases.Game.Services.Interfaces;
using DartTournament.Presentation.Base.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Presentation.Services
{
    public class GamePresentationService : IGamePresentationService
    {
        private readonly IGameService _gameService;

        public GamePresentationService(IGameService gameService)
        {
            _gameService = gameService;
        }

        public Guid CreateGame(CreateGameDTO createGame)
        {
            return _gameService.CreateGame(createGame);
        }
    }
}
