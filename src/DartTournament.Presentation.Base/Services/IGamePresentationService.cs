using DartTournament.Application.DTO.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Presentation.Base.Services
{
    public interface IGamePresentationService
    {
        Task<Guid> CreateGame(CreateGameDTO createGame);
        Task<GameResult> GetGame(Guid gameId);
        Task<List<GameResult>> GetAllGames();
    }
}
