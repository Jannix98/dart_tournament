using DartTournament.Domain.Entities;
using DartTournament.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DartTournament.Infrastructure.JSON.Persistence
{
    public class GameParentRepository : BaseRepository<GameParent>, IGameParentRepository
    {
        private readonly IGameRepository _gameRepository;
        private readonly IRoundRepository _roundRepository;
        private readonly IMatchRepository _matchRepository;

        public GameParentRepository(IGameRepository gameRepository, IRoundRepository roundRepository, IMatchRepository matchRepository) : base()
        {
            _gameRepository = gameRepository;
            _roundRepository = roundRepository;
            _matchRepository = matchRepository;
        }

        protected override string FileName => "gameparents.json";

        public Guid CreateGame(Game game)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> CreateGameParent(GameParent gameParent)
        {
            // set the ID properties manually since we don't have something like the Entity Framework
            gameParent.Id = Guid.NewGuid();
            gameParent.MainGame.Id = Guid.NewGuid();
            foreach (var round in gameParent.MainGame.Rounds)
            {
                round.Id = Guid.NewGuid();
                foreach (var match in round.Matches)
                {
                    match.Id = Guid.NewGuid();
                }
                round.MatchIds = round.Matches.Select(m => m.Id).ToList();
            }
            gameParent.MainGameId = gameParent.MainGame.Id;
            gameParent.MainGame.RoundIds = gameParent.MainGame.Rounds.Select(r => r.Id).ToList();

            if (gameParent.LooserGame != null)
            {
                gameParent.LooserGame.Id = Guid.NewGuid();
                foreach (var round in gameParent.LooserGame.Rounds)
                {
                    round.Id = Guid.NewGuid();
                    foreach (var match in round.Matches)
                    {
                        match.Id = Guid.NewGuid();
                    }
                    round.MatchIds = round.Matches.Select(m => m.Id).ToList();
                }
                gameParent.LooserGameId = gameParent.LooserGame.Id;
                gameParent.LooserGame.RoundIds = gameParent.LooserGame.Rounds.Select(r => r.Id).ToList();
            }

            await Insert(gameParent);
            await _gameRepository.Insert(gameParent.MainGame);
            if (gameParent.LooserGame != null)
                await _gameRepository.Insert(gameParent.LooserGame);

            var allRounds = gameParent.MainGame.Rounds.Concat(gameParent.LooserGame?.Rounds ?? new List<GameRound>()).ToList();
            await _roundRepository.InsertRange(allRounds);
            var allMatches = allRounds.SelectMany(r => r.Matches).ToList();
            await _matchRepository.InsertRange(allMatches);
            return gameParent.Id;
        }

        private async Task Insert(GameParent gameParent)
        {
            var all = await GetAllGameParents();
            all.Add(gameParent);
            await SaveInFile(all);
        }

        public async Task<GameParent> GetGameParent(Guid id)
        {
            var all = await GetAllGameParents();
            var gameParent = all.FirstOrDefault(gp => gp.Id == id);
            return await FillLoadedGameProperties(id, gameParent);
        }

        private async Task<GameParent> FillLoadedGameProperties(Guid id, GameParent? gameParent)
        {
            if (gameParent == null)
                throw new ArgumentException($"GameParent with ID {id} not found.");

            // Load the MainGame with its rounds and matches
            var mainGame = await _gameRepository.GetByIdAsync(gameParent.MainGameId);
            if (mainGame != null)
            {
                gameParent.MainGame = mainGame;
                await LoadGameRoundsAndMatches(mainGame);
            }

            // Load the LooserGame with its rounds and matches if it exists
            if (gameParent.LooserGameId.HasValue)
            {
                var looserGame = await _gameRepository.GetByIdAsync(gameParent.LooserGameId.Value);
                if (looserGame != null)
                {
                    gameParent.LooserGame = looserGame;
                    await LoadGameRoundsAndMatches(looserGame);
                }
            }

            return gameParent;
        }

        private async Task LoadGameRoundsAndMatches(Game game)
        {
            if (game.RoundIds != null && game.RoundIds.Any())
            {
                var rounds = await _roundRepository.GetByIdsAsync(game.RoundIds);
                game.Rounds = rounds;

                // Load matches for each round
                foreach (var round in rounds)
                {
                    if (round.MatchIds != null && round.MatchIds.Any())
                    {
                        var matches = await _matchRepository.GetByIdsAsync(round.MatchIds);
                        round.Matches = matches;
                    }
                }
            }
        }

        public Task<List<GameParent>> GetAllGameParents()
        {
            return base.GetAllAsync();
        }

        public async Task<List<GameParent>> GetAllGameParentsWithProperties()
        {
            var all = await GetAllGameParents();
            foreach (var gameParent in all)
            {
                FillLoadedGameProperties(gameParent.Id, gameParent).Wait();
            }
            return all;
        }
    }
}
