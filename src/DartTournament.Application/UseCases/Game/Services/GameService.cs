using DartTournament.Application.DTO.Game;
using DartTournament.Application.UseCases.Game.Services.Interfaces;
using DartTournament.Domain.Entities;
using DartTournament.Domain.Interfaces;
using DartTournament.Helper.RoundCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Application.UseCases.Game.Services
{
    public class GameService : IGameService
    {
        private readonly IGameParentRepository _dartGameRepository;

        public GameService(IGameParentRepository dartGameRepository)
        {
            _dartGameRepository = dartGameRepository;
        }

        public Guid CreateGame(CreateGameDTO createGame)
        {
            DartTournament.Domain.Entities.Game mainGame = null;
            DartTournament.Domain.Entities.Game looserGame = null;

            var rounds = CreateRounds(createGame.NumberOfPlayers, createGame.PlayerIds, new NormalRoundCalculator());
            mainGame = new DartTournament.Domain.Entities.Game(rounds);
            if(createGame.HasLooserRound)
            {
                var looserRounds = CreateRounds(createGame.NumberOfPlayers / 2, new List<Guid>(), new LooserRoundCalculator());
                looserGame = new DartTournament.Domain.Entities.Game(looserRounds);
            }
            GameParent gameParent = new GameParent(createGame.Name, mainGame, looserGame, createGame.HasLooserRound);
            _dartGameRepository.CreateGameParent(gameParent);
            return Guid.NewGuid();
        }

        private List<GameRound> CreateRounds(int maxPlayer, List<Guid> playerIds, RoundCalculatorBase roundCalculator)
        {
            if (maxPlayer != playerIds.Count && playerIds.Count > 0)
            {
                throw new ArgumentException($"The number of players must be {maxPlayer}.");
            }

            int gameRoundsCount = roundCalculator.GetRoundCount(maxPlayer);

            List<GameMatch> matches = new List<GameMatch>();
            int firsrtRoundMatchIndex = 0;
            for (int i = 0; i < maxPlayer; i += 2)
            {
                Guid id1 = Guid.Empty;
                Guid id2 = Guid.Empty;
                if (playerIds.Count > 0)
                {
                    id1 = playerIds[i];
                    id2 = playerIds[i + 1];
                }

                matches.Add(new GameMatch(id1, id2));
            }
            GameRound firstRound = new GameRound(0, matches);
            firstRound.Matches = matches;
            int matchesInFirstRound = matches.Count;
            List<GameRound> rounds = new List<GameRound>();
            rounds.Add(firstRound);

            for (int i = 1; i < gameRoundsCount; i++)
            {
                int matchIndex = 0;
                List<GameMatch> matchesInRound = new List<GameMatch>();
                for (int j = 0; j < matchesInFirstRound >> i; j++)
                {

                    matchesInRound.Add(new GameMatch(Guid.Empty, Guid.Empty));
                }
                GameRound round = new GameRound(i, matchesInRound);
                rounds.Add(round);
            }
            
            return rounds;
        }
    }
}
