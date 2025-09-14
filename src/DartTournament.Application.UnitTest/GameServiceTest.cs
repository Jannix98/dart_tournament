using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DartTournament.Application.DTO.Game;
using DartTournament.Application.DTO.Player;
using DartTournament.Application.UseCases.Game.Services;
using DartTournament.Application.UseCases.Player.Services.Interfaces;
using DartTournament.Domain.Entities;
using DartTournament.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DartTournament.Application.UnitTest
{
    [TestClass]
    public class GameServiceTest
    {
        private Mock<IGameParentRepository> _gameRepositoryMock = null!;
        private Mock<IPlayerService> _playerServiceMock = null!;
        private GameService _gameService = null!;

        [TestInitialize]
        public void Setup()
        {
            _gameRepositoryMock = new Mock<IGameParentRepository>();
            _playerServiceMock = new Mock<IPlayerService>();
            _gameService = new GameService(_gameRepositoryMock.Object, _playerServiceMock.Object);
        }

        [TestMethod]
        public void CreateGame_Should_Create_Game_Without_Looser_Round()
        {
            // Arrange
            var playerIds = GeneratePlayerIds(8);
            var createGameDto = new CreateGameDTO("Test Tournament", 8, playerIds, false);
            GameParent? savedGameParent = null;
            
            _gameRepositoryMock
                .Setup(r => r.CreateGameParent(It.IsAny<GameParent>()))
                .Callback<GameParent>(gp => savedGameParent = gp);

            // Act
            var result = _gameService.CreateGame(createGameDto);

            // Assert
            Assert.IsNotNull(savedGameParent);
            Assert.AreEqual("Test Tournament", savedGameParent!.Name);
            Assert.IsFalse(savedGameParent.HasLooserRound);
            Assert.IsNotNull(savedGameParent.MainGame);
            Assert.IsNull(savedGameParent.LooserGame);
            _gameRepositoryMock.Verify(r => r.CreateGameParent(It.IsAny<GameParent>()), Times.Once);
        }

        [TestMethod]
        public void CreateGame_Should_Create_Game_With_Looser_Round()
        {
            // Arrange
            var playerIds = GeneratePlayerIds(8);
            var createGameDto = new CreateGameDTO("Test Tournament", 8, playerIds, true);
            GameParent? savedGameParent = null;
            
            _gameRepositoryMock
                .Setup(r => r.CreateGameParent(It.IsAny<GameParent>()))
                .Callback<GameParent>(gp => savedGameParent = gp);

            // Act
            var result = _gameService.CreateGame(createGameDto);

            // Assert
            Assert.IsNotNull(savedGameParent);
            Assert.AreEqual("Test Tournament", savedGameParent!.Name);
            Assert.IsTrue(savedGameParent.HasLooserRound);
            Assert.IsNotNull(savedGameParent.MainGame);
            Assert.IsNotNull(savedGameParent.LooserGame);
            _gameRepositoryMock.Verify(r => r.CreateGameParent(It.IsAny<GameParent>()), Times.Once);
        }

        [TestMethod]
        [DataRow(4, new[] { 2, 1 })] // 4 players: Round 1 has 2 matches, Round 2 has 1 match
        [DataRow(8, new[] { 4, 2, 1 })] // 8 players: Round 1 has 4 matches, Round 2 has 2 matches, Round 3 has 1 match
        [DataRow(16, new[] { 8, 4, 2, 1 })] // 16 players: Round 1 has 8 matches, Round 2 has 4 matches, Round 3 has 2 matches, Round 4 has 1 match
        [DataRow(32, new[] { 16, 8, 4, 2, 1 })] // 32 players: 5 rounds with decreasing matches
        public void CreateGame_Should_Create_Correct_Match_Counts_Per_Round(int playerCount, int[] expectedMatchCounts)
        {
            // Arrange
            var playerIds = GeneratePlayerIds(playerCount);
            var createGameDto = new CreateGameDTO("Test Tournament", playerCount, playerIds, false);
            GameParent? savedGameParent = null;
            
            _gameRepositoryMock
                .Setup(r => r.CreateGameParent(It.IsAny<GameParent>()))
                .Callback<GameParent>(gp => savedGameParent = gp);

            // Act
            _gameService.CreateGame(createGameDto);

            // Assert
            Assert.IsNotNull(savedGameParent);
            var mainGame = savedGameParent!.MainGame;
            Assert.IsNotNull(mainGame);
            Assert.AreEqual(expectedMatchCounts.Length, mainGame.Rounds.Count);

            for (int i = 0; i < expectedMatchCounts.Length; i++)
            {
                Assert.AreEqual(expectedMatchCounts[i], mainGame.Rounds[i].Matches.Count, 
                    $"Round {i + 1} should have {expectedMatchCounts[i]} matches");
            }
        }

        [TestMethod]
        public void CreateGame_Should_Assign_Player_Ids_To_First_Round_Matches()
        {
            // Arrange
            var playerIds = GeneratePlayerIds(8);
            var createGameDto = new CreateGameDTO("Test Tournament", 8, playerIds, false);
            GameParent? savedGameParent = null;
            
            _gameRepositoryMock
                .Setup(r => r.CreateGameParent(It.IsAny<GameParent>()))
                .Callback<GameParent>(gp => savedGameParent = gp);

            // Act
            _gameService.CreateGame(createGameDto);

            // Assert
            Assert.IsNotNull(savedGameParent);
            var firstRound = savedGameParent!.MainGame.Rounds.First();
            Assert.AreEqual(4, firstRound.Matches.Count);

            // Verify all players are assigned to matches in pairs
            var assignedPlayerIds = new List<Guid>();
            foreach (var match in firstRound.Matches)
            {
                Assert.AreNotEqual(Guid.Empty, match.IdGameEntityA);
                Assert.AreNotEqual(Guid.Empty, match.IdGameEntityB);
                Assert.AreNotEqual(match.IdGameEntityA, match.IdGameEntityB);
                assignedPlayerIds.Add(match.IdGameEntityA);
                assignedPlayerIds.Add(match.IdGameEntityB);
            }

            // Verify all original player IDs are present
            foreach (var playerId in playerIds)
            {
                Assert.IsTrue(assignedPlayerIds.Contains(playerId), 
                    $"Player {playerId} should be assigned to a match");
            }
        }

        [TestMethod]
        public void CreateGame_Should_Create_Empty_Matches_For_Subsequent_Rounds()
        {
            // Arrange
            var playerIds = GeneratePlayerIds(8);
            var createGameDto = new CreateGameDTO("Test Tournament", 8, playerIds, false);
            GameParent? savedGameParent = null;
            
            _gameRepositoryMock
                .Setup(r => r.CreateGameParent(It.IsAny<GameParent>()))
                .Callback<GameParent>(gp => savedGameParent = gp);

            // Act
            _gameService.CreateGame(createGameDto);

            // Assert
            Assert.IsNotNull(savedGameParent);
            var rounds = savedGameParent!.MainGame.Rounds;
            
            // Skip first round (index 0) and check subsequent rounds have empty matches
            for (int i = 1; i < rounds.Count; i++)
            {
                foreach (var match in rounds[i].Matches)
                {
                    Assert.AreEqual(Guid.Empty, match.IdGameEntityA, 
                        $"Round {i + 1} should have empty Player1Id");
                    Assert.AreEqual(Guid.Empty, match.IdGameEntityB, 
                        $"Round {i + 1} should have empty Player2Id");
                }
            }
        }

        [TestMethod]
        [DataRow(4, new[] { 1 })] // 4 players, looser round has 4/2 = 2 players, so 1 match
        [DataRow(8, new[] { 2, 1 })] // 8 players, looser round has 8/2 = 4 players, so 2 matches in round 1, 1 in round 2
        [DataRow(16, new[] { 4, 2, 1 })] // 16 players, looser round has 16/2 = 8 players, so 4 matches in round 1, 2 in round 2, 1 in round 3
        [DataRow(32, new[] { 8, 4, 2, 1 })] // 32 players, looser round has 32/2 = 16 players, so 8 matches in round 1, etc.
        public void CreateGame_With_Looser_Round_Should_Create_Correct_Match_Counts(int playerCount, int[] expectedLooserMatchCounts)
        {
            // Arrange
            var playerIds = GeneratePlayerIds(playerCount);
            var createGameDto = new CreateGameDTO("Test Tournament", playerCount, playerIds, true);
            GameParent? savedGameParent = null;
            
            _gameRepositoryMock
                .Setup(r => r.CreateGameParent(It.IsAny<GameParent>()))
                .Callback<GameParent>(gp => savedGameParent = gp);

            // Act
            _gameService.CreateGame(createGameDto);

            // Assert
            Assert.IsNotNull(savedGameParent);
            var looserGame = savedGameParent!.LooserGame;
            Assert.IsNotNull(looserGame);
            Assert.AreEqual(expectedLooserMatchCounts.Length, looserGame.Rounds.Count);

            for (int i = 0; i < expectedLooserMatchCounts.Length; i++)
            {
                Assert.AreEqual(expectedLooserMatchCounts[i], looserGame.Rounds[i].Matches.Count, 
                    $"Looser round {i + 1} should have {expectedLooserMatchCounts[i]} matches");
            }
        }

        [TestMethod]
        public void CreateGame_With_Looser_Round_Should_Have_Empty_Player_Ids()
        {
            // Arrange
            var playerIds = GeneratePlayerIds(8);
            var createGameDto = new CreateGameDTO("Test Tournament", 8, playerIds, true);
            GameParent? savedGameParent = null;
            
            _gameRepositoryMock
                .Setup(r => r.CreateGameParent(It.IsAny<GameParent>()))
                .Callback<GameParent>(gp => savedGameParent = gp);

            // Act
            _gameService.CreateGame(createGameDto);

            // Assert
            Assert.IsNotNull(savedGameParent);
            var looserGame = savedGameParent!.LooserGame;
            Assert.IsNotNull(looserGame);

            // All matches in looser game should have empty player IDs initially
            foreach (var round in looserGame.Rounds)
            {
                foreach (var match in round.Matches)
                {
                    Assert.AreEqual(Guid.Empty, match.IdGameEntityA);
                    Assert.AreEqual(Guid.Empty, match.IdGameEntityB);
                }
            }
        }

        [TestMethod]
        public void CreateGame_With_Empty_Player_List_Should_Create_Empty_Matches()
        {
            // Arrange
            var createGameDto = new CreateGameDTO("Test Tournament", 8, new List<Guid>(), false);
            GameParent? savedGameParent = null;
            
            _gameRepositoryMock
                .Setup(r => r.CreateGameParent(It.IsAny<GameParent>()))
                .Callback<GameParent>(gp => savedGameParent = gp);

            // Act
            _gameService.CreateGame(createGameDto);

            // Assert
            Assert.IsNotNull(savedGameParent);
            var firstRound = savedGameParent!.MainGame.Rounds.First();
            
            foreach (var match in firstRound.Matches)
            {
                Assert.AreEqual(Guid.Empty, match.IdGameEntityA);
                Assert.AreEqual(Guid.Empty, match.IdGameEntityB);
            }
        }

        [TestMethod]
        public async Task CreateGame_With_Mismatched_Player_Count_Should_Throw_ArgumentException()
        {
            // Arrange
            var playerIds = GeneratePlayerIds(6); // 6 players but expecting 8
            var createGameDto = new CreateGameDTO("Test Tournament", 8, playerIds, false);

            // Act & Assert
            var exception = await Assert.ThrowsExceptionAsync<ArgumentException>(() => _gameService.CreateGame(createGameDto));
            Assert.IsTrue(exception.Message.Contains("The number of players must be 8"));
        }

        [TestMethod]
        public void CreateGame_Should_Return_Valid_Guid()
        {
            // Arrange
            var playerIds = GeneratePlayerIds(8);
            var createGameDto = new CreateGameDTO("Test Tournament", 8, playerIds, false);

            _gameRepositoryMock.Setup(x => x.CreateGameParent(It.IsAny<GameParent>()))
                .ReturnsAsync(Guid.NewGuid());

            // Act
            var result = _gameService.CreateGame(createGameDto).Result;

            // Assert
            Assert.AreNotEqual(Guid.Empty, result);
        }

        // New tests for GetGame method with player name resolution
        [TestMethod]
        public async Task GetGame_Should_Return_Mapped_Game_With_Player_Names()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var playerIds = GeneratePlayerIds(4);
            var gameParent = CreateTestGameParentWithPlayerIds("Test Tournament", playerIds, false);
            
            _gameRepositoryMock
                .Setup(r => r.GetGameParent(gameId))
                .ReturnsAsync(gameParent);

            // Setup player service to return player names
            for (int i = 0; i < playerIds.Count; i++)
            {
                var playerId = playerIds[i];
                _playerServiceMock
                    .Setup(ps => ps.GetPlayerByIdAsync(playerId))
                    .ReturnsAsync(new DartPlayerGetDto { Id = playerId, Name = $"Player {i + 1}" });
            }

            // Act
            var result = await _gameService.GetGame(gameId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(gameParent.Id, result.Id);
            Assert.AreEqual("Test Tournament", result.Name);
            Assert.IsFalse(result.HasLooserRound);
            Assert.IsNotNull(result.MainGame);
            Assert.IsNull(result.LooserGame);

            // Verify player names are populated
            var firstRound = result.MainGame.Rounds.First();
            var matchesWithNames = firstRound.Matches.Where(m => !string.IsNullOrEmpty(m.PlayerAName) && !string.IsNullOrEmpty(m.PlayerBName));
            Assert.AreEqual(2, matchesWithNames.Count());

            _gameRepositoryMock.Verify(r => r.GetGameParent(gameId), Times.Once);
            _playerServiceMock.Verify(ps => ps.GetPlayerByIdAsync(It.IsAny<Guid>()), Times.Exactly(4));
        }

        [TestMethod]
        public async Task GetGame_Should_Handle_Missing_Players_Gracefully()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var playerIds = GeneratePlayerIds(4);
            var gameParent = CreateTestGameParentWithPlayerIds("Test Tournament", playerIds, false);
            
            _gameRepositoryMock
                .Setup(r => r.GetGameParent(gameId))
                .ReturnsAsync(gameParent);

            // Setup player service to return null for some players
            _playerServiceMock
                .Setup(ps => ps.GetPlayerByIdAsync(playerIds[0]))
                .ReturnsAsync(new DartPlayerGetDto { Id = playerIds[0], Name = "Player 1" });
            
            _playerServiceMock
                .Setup(ps => ps.GetPlayerByIdAsync(playerIds[1]))
                .ReturnsAsync((DartPlayerGetDto)null); // Player not found

            _playerServiceMock
                .Setup(ps => ps.GetPlayerByIdAsync(playerIds[2]))
                .ReturnsAsync(new DartPlayerGetDto { Id = playerIds[2], Name = "Player 3" });

            _playerServiceMock
                .Setup(ps => ps.GetPlayerByIdAsync(playerIds[3]))
                .ReturnsAsync((DartPlayerGetDto)null); // Player not found

            // Act
            var result = await _gameService.GetGame(gameId);

            // Assert
            Assert.IsNotNull(result);
            var firstRound = result.MainGame.Rounds.First();
            
            // First match: Player 1 vs missing player
            Assert.AreEqual("Player 1", firstRound.Matches[0].PlayerAName);
            Assert.AreEqual(string.Empty, firstRound.Matches[0].PlayerBName);
            
            // Second match: Player 3 vs missing player
            Assert.AreEqual("Player 3", firstRound.Matches[1].PlayerAName);
            Assert.AreEqual(string.Empty, firstRound.Matches[1].PlayerBName);
        }

        [TestMethod]
        public async Task GetGame_Should_Not_Call_PlayerService_For_Empty_Guids()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var gameParent = CreateTestGameParent("Test Tournament", 8, false);
            
            _gameRepositoryMock
                .Setup(r => r.GetGameParent(gameId))
                .ReturnsAsync(gameParent);

            // Act
            var result = await _gameService.GetGame(gameId);

            // Assert
            Assert.IsNotNull(result);
            
            // Verify player service was not called for empty GUIDs (only first round should have non-empty GUIDs)
            var totalNonEmptyMatches = gameParent.MainGame.Rounds[0].Matches.Count * 2; // 2 players per match in first round
            _playerServiceMock.Verify(ps => ps.GetPlayerByIdAsync(It.IsAny<Guid>()), Times.Exactly(totalNonEmptyMatches));
        }

        [TestMethod]
        public async Task GetGame_Should_Return_Mapped_Game_Without_Looser_Round()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var gameParent = CreateTestGameParent("Test Tournament", 8, false);
            
            _gameRepositoryMock
                .Setup(r => r.GetGameParent(gameId))
                .ReturnsAsync(gameParent);

            // Act
            var result = await _gameService.GetGame(gameId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(gameParent.Id, result.Id);
            Assert.AreEqual("Test Tournament", result.Name);
            Assert.IsFalse(result.HasLooserRound);
            Assert.IsNotNull(result.MainGame);
            Assert.IsNull(result.LooserGame);
            _gameRepositoryMock.Verify(r => r.GetGameParent(gameId), Times.Once);
        }

        [TestMethod]
        public async Task GetGame_Should_Return_Mapped_Game_With_Looser_Round()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var gameParent = CreateTestGameParent("Championship", 16, true);
            
            _gameRepositoryMock
                .Setup(r => r.GetGameParent(gameId))
                .ReturnsAsync(gameParent);

            // Act
            var result = await _gameService.GetGame(gameId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(gameParent.Id, result.Id);
            Assert.AreEqual("Championship", result.Name);
            Assert.IsTrue(result.HasLooserRound);
            Assert.IsNotNull(result.MainGame);
            Assert.IsNotNull(result.LooserGame);
            _gameRepositoryMock.Verify(r => r.GetGameParent(gameId), Times.Once);
        }

        [TestMethod]
        [DataRow(4)]
        [DataRow(8)]
        [DataRow(16)]
        [DataRow(32)]
        public async Task GetGame_Should_Map_Correct_Tournament_Structure_For_Various_Player_Counts(int playerCount)
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var gameParent = CreateTestGameParent($"Tournament {playerCount}", playerCount, false);
            
            _gameRepositoryMock
                .Setup(r => r.GetGameParent(gameId))
                .ReturnsAsync(gameParent);

            // Act
            var result = await _gameService.GetGame(gameId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.MainGame);
            
            var expectedRounds = (int)Math.Log2(playerCount);
            Assert.AreEqual(expectedRounds, result.MainGame.Rounds.Count);
            
            // Verify first round has correct number of matches
            var expectedFirstRoundMatches = playerCount / 2;
            Assert.AreEqual(expectedFirstRoundMatches, result.MainGame.Rounds[0].Matches.Count);
        }

        [TestMethod]
        [DataRow(4)]
        [DataRow(8)]
        [DataRow(16)]
        [DataRow(32)]
        public async Task GetGame_With_Looser_Round_Should_Map_Correct_Structure(int playerCount)
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var gameParent = CreateTestGameParent($"Tournament {playerCount}", playerCount, true);
            
            _gameRepositoryMock
                .Setup(r => r.GetGameParent(gameId))
                .ReturnsAsync(gameParent);

            // Act
            var result = await _gameService.GetGame(gameId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.LooserGame);
            
            var expectedLooserRounds = (int)Math.Log2(playerCount / 2);
            Assert.AreEqual(expectedLooserRounds, result.LooserGame.Rounds.Count);
            
            // Verify first looser round has correct number of matches
            var expectedFirstLooserRoundMatches = (playerCount / 2) / 2;
            Assert.AreEqual(expectedFirstLooserRoundMatches, result.LooserGame.Rounds[0].Matches.Count);
        }

        [TestMethod]
        public async Task GetGame_With_Null_GameParent_Should_Throw_ArgumentException()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            
            _gameRepositoryMock
                .Setup(r => r.GetGameParent(gameId))
                .ReturnsAsync((GameParent)null);

            // Act & Assert
            var exception = await Assert.ThrowsExceptionAsync<ArgumentException>(() => _gameService.GetGame(gameId));
            Assert.IsTrue(exception.Message.Contains($"Game with ID {gameId} not found"));
            _gameRepositoryMock.Verify(r => r.GetGameParent(gameId), Times.Once);
        }

        [TestMethod]
        public async Task GetGame_Should_Pass_Correct_Id_To_Repository()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var gameParent = CreateTestGameParent("Test", 4, false);
            
            _gameRepositoryMock
                .Setup(r => r.GetGameParent(It.IsAny<Guid>()))
                .ReturnsAsync(gameParent);

            // Act
            await _gameService.GetGame(gameId);

            // Assert
            _gameRepositoryMock.Verify(r => r.GetGameParent(gameId), Times.Once);
        }

        private GameParent CreateTestGameParent(string name, int playerCount, bool hasLooserRound)
        {
            var playerIds = GeneratePlayerIds(playerCount);
            return CreateTestGameParentWithPlayerIds(name, playerIds, hasLooserRound);
        }

        private GameParent CreateTestGameParentWithPlayerIds(string name, List<Guid> playerIds, bool hasLooserRound)
        {
            var mainGame = CreateTestGame(playerIds.Count, playerIds);
            DartTournament.Domain.Entities.Game looserGame = null;

            if (hasLooserRound)
            {
                looserGame = CreateTestGame(playerIds.Count / 2, new List<Guid>());
            }

            var gameParent = new GameParent(name, DateTime.Now, mainGame, looserGame, hasLooserRound);
            gameParent.Id = Guid.NewGuid();
            return gameParent;
        }

        private DartTournament.Domain.Entities.Game CreateTestGame(int playerCount, List<Guid> playerIds)
        {
            var rounds = CreateTestRounds(playerCount, playerIds);
            var game = new DartTournament.Domain.Entities.Game(rounds);
            game.Id = Guid.NewGuid();
            return game;
        }

        private List<GameRound> CreateTestRounds(int playerCount, List<Guid> playerIds)
        {
            if (playerCount == 0) return new List<GameRound>();

            var rounds = new List<GameRound>();
            int roundsCount = (int)Math.Log2(playerCount);

            // First round with players
            var firstRoundMatches = new List<GameMatch>();
            for (int i = 0; i < playerCount; i += 2)
            {
                Guid player1Id = Guid.Empty;
                Guid player2Id = Guid.Empty;

                if (playerIds.Count > i)
                {
                    player1Id = playerIds[i];
                    if (playerIds.Count > i + 1)
                    {
                        player2Id = playerIds[i + 1];
                    }
                }

                var match = new GameMatch(player1Id, player2Id, Guid.Empty);
                match.Id = Guid.NewGuid();
                firstRoundMatches.Add(match);
            }

            var firstRound = new GameRound(0, firstRoundMatches);
            firstRound.Id = Guid.NewGuid();
            rounds.Add(firstRound);

            // Subsequent rounds with empty matches
            int matchesInPreviousRound = firstRoundMatches.Count;
            for (int roundIndex = 1; roundIndex < roundsCount; roundIndex++)
            {
                var roundMatches = new List<GameMatch>();
                int matchesInThisRound = matchesInPreviousRound / 2;

                for (int j = 0; j < matchesInThisRound; j++)
                {
                    var match = new GameMatch(Guid.Empty, Guid.Empty, Guid.Empty);
                    match.Id = Guid.NewGuid();
                    roundMatches.Add(match);
                }

                var round = new GameRound(roundIndex, roundMatches);
                round.Id = Guid.NewGuid();
                rounds.Add(round);

                matchesInPreviousRound = matchesInThisRound;
            }

            return rounds;
        }

        private List<Guid> GeneratePlayerIds(int count)
        {
            var playerIds = new List<Guid>();
            for (int i = 0; i < count; i++)
            {
                playerIds.Add(Guid.NewGuid());
            }
            return playerIds;
        }
    }
}
