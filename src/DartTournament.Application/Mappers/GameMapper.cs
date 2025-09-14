using DartTournament.Application.DTO.Game;
using DartTournament.Application.UseCases.Player.Services.Interfaces;
using DartTournament.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DartTournament.Application.Mappers
{
    public static class GameMapper
    {
        public static async Task<GameResult> MapToGameResultAsync(GameParent gameParent, IPlayerService playerService)
        {
            if (gameParent == null)
                throw new ArgumentNullException(nameof(gameParent));

            var mainGameDto = await MapToGameDTOAsync(gameParent.MainGame, playerService);
            var looserGameDto = gameParent.LooserGame != null ? await MapToGameDTOAsync(gameParent.LooserGame, playerService) : null;

            return new GameResult(
                gameParent.Id,
                gameParent.Name,
                gameParent.HasLooserRound,
                mainGameDto,
                looserGameDto
            );
        }

        private static async Task<GameDTO> MapToGameDTOAsync(DartTournament.Domain.Entities.Game game, IPlayerService playerService)
        {
            if (game == null)
                throw new ArgumentNullException(nameof(game));

            var roundDtos = new List<GameRoundDTO>();
            if (game.Rounds != null)
            {
                foreach (var round in game.Rounds)
                {
                    roundDtos.Add(await MapToGameRoundDTOAsync(round, playerService));
                }
            }

            return new GameDTO(game.Id, roundDtos);
        }

        private static async Task<GameRoundDTO> MapToGameRoundDTOAsync(GameRound gameRound, IPlayerService playerService)
        {
            if (gameRound == null)
                throw new ArgumentNullException(nameof(gameRound));

            var matchDtos = new List<GameMatchDTO>();
            if (gameRound.Matches != null)
            {
                foreach (var match in gameRound.Matches)
                {
                    matchDtos.Add(await MapToGameMatchDTOAsync(match, playerService));
                }
            }

            return new GameRoundDTO(gameRound.Id, gameRound.RoundNumber, matchDtos);
        }

        private static async Task<GameMatchDTO> MapToGameMatchDTOAsync(GameMatch gameMatch, IPlayerService playerService)
        {
            if (gameMatch == null)
                throw new ArgumentNullException(nameof(gameMatch));

            string playerAName = string.Empty;
            string playerBName = string.Empty;

            // Fetch player names only if the IDs are not empty
            if (gameMatch.IdGameEntityA != Guid.Empty)
            {
                var playerA = await playerService.GetPlayerByIdAsync(gameMatch.IdGameEntityA);
                playerAName = playerA?.Name ?? string.Empty;
            }

            if (gameMatch.IdGameEntityB != Guid.Empty)
            {
                var playerB = await playerService.GetPlayerByIdAsync(gameMatch.IdGameEntityB);
                playerBName = playerB?.Name ?? string.Empty;
            }

            return new GameMatchDTO(gameMatch.Id, gameMatch.IdGameEntityA, gameMatch.IdGameEntityB, gameMatch.WinnerId, playerAName, playerBName);
        }
    }
}