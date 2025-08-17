using DartTournament.Application.DTO.Game;
using DartTournament.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Application.Mappers
{
    public static class GameMapper
    {
        public static GameResult MapToGameResult(GameParent gameParent)
        {
            if (gameParent == null)
                throw new ArgumentNullException(nameof(gameParent));

            var mainGameDto = MapToGameDTO(gameParent.MainGame);
            var looserGameDto = gameParent.LooserGame != null ? MapToGameDTO(gameParent.LooserGame) : null;

            return new GameResult(
                gameParent.Id,
                gameParent.Name,
                gameParent.HasLooserRound,
                mainGameDto,
                looserGameDto
            );
        }

        private static GameDTO MapToGameDTO(DartTournament.Domain.Entities.Game game)
        {
            if (game == null)
                throw new ArgumentNullException(nameof(game));

            var roundDtos = game.Rounds?.Select(MapToGameRoundDTO).ToList() ?? new List<GameRoundDTO>();

            return new GameDTO(game.Id, roundDtos);
        }

        private static GameRoundDTO MapToGameRoundDTO(GameRound gameRound)
        {
            if (gameRound == null)
                throw new ArgumentNullException(nameof(gameRound));

            var matchDtos = gameRound.Matches?.Select(MapToGameMatchDTO).ToList() ?? new List<GameMatchDTO>();

            return new GameRoundDTO(gameRound.Id, gameRound.RoundNumber, matchDtos);
        }

        private static GameMatchDTO MapToGameMatchDTO(GameMatch gameMatch)
        {
            if (gameMatch == null)
                throw new ArgumentNullException(nameof(gameMatch));

            return new GameMatchDTO(gameMatch.Id, gameMatch.IdGameEntityA, gameMatch.IdGameEntityB);
        }
    }
}