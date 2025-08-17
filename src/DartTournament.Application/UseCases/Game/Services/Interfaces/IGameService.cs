using DartTournament.Application.DTO.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Application.UseCases.Game.Services.Interfaces
{
    public interface IGameService
    {
        Guid CreateGame(CreateGameDTO createGame);
    }
}
