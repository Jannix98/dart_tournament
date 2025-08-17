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

        public async Task<Guid> CreateGame(CreateGameDTO createGame)
        {
            return await _gameService.CreateGame(createGame);
        }

        public async Task<GameResult> GetGame(Guid gameId)
        {
            return await _gameService.GetGame(gameId);
        }
    }
}
