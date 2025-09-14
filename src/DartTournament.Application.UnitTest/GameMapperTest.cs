using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DartTournament.Application.Mappers;
using DartTournament.Application.DTO.Game;
using DartTournament.Application.DTO.Player;
using DartTournament.Application.UseCases.Player.Services.Interfaces;
using DartTournament.Domain.Entities;

namespace DartTournament.Application.UnitTest
{
    [TestClass]
    public class GameMapperTest
    {
        private Mock<IPlayerService> _playerServiceMock = null!;

        [TestInitialize]
        public void Setup()
        {
            _playerServiceMock = new Mock<IPlayerService>();
        }

        [TestMethod]
        public async Task MapToGameResultAsync_Should_Map_Game_Without_Looser_Round()
        {
            // Arrange
            var gameParent = CreateTestGameParent("Test Tournament", 4, false);

            // Act
            var result = await GameMapper.MapToGameResultAsync(gameParent, _playerServiceMock.Object);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(gameParent.Id, result.Id);
            Assert.AreEqual("Test Tournament", result.Name);
            Assert.IsFalse(result.HasLooserRound);
            Assert.IsNotNull(result.MainGame);
            Assert.IsNull(result.LooserGame);
        }

        [TestMethod]
        public async Task MapToGameResultAsync_Should_Map_Game_With_Looser_Round()
        {
            // Arrange
            var gameParent = CreateTestGameParent("Test Tournament", 8, true);

            // Act
            var result = await GameMapper.MapToGameResultAsync(gameParent, _playerServiceMock.Object);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(gameParent.Id, result.Id);
            Assert.AreEqual("Test Tournament", result.Name);
            Assert.IsTrue(result.HasLooserRound);
            Assert.IsNotNull(result.MainGame);
            Assert.IsNotNull(result.LooserGame);
        }

        [TestMethod]
        [DataRow(4, 2)] // 4 players: 2 rounds (2 matches + 1 match)
        [DataRow(8, 3)] // 8 players: 3 rounds (4 matches + 2 matches + 1 match)
        [DataRow(16, 4)] // 16 players: 4 rounds (8 + 4 + 2 + 1 matches)
        [DataRow(32, 5)] // 32 players: 5 rounds (16 + 8 + 4 + 2 + 1 matches)
        public async Task MapToGameResultAsync_Should_Map_Correct_Number_Of_Rounds_For_Main_Game(int playerCount, int expectedRounds)
        {
            // Arrange
            var gameParent = CreateTestGameParent("Test Tournament", playerCount, false);

            // Act
            var result = await GameMapper.MapToGameResultAsync(gameParent, _playerServiceMock.Object);

            // Assert
            Assert.IsNotNull(result.MainGame);
            Assert.AreEqual(expectedRounds, result.MainGame.Rounds.Count);
        }

        [TestMethod]
        [DataRow(4, new[] { 2, 1 })] // 4 players: Round 1 has 2 matches, Round 2 has 1 match
        [DataRow(8, new[] { 4, 2, 1 })] // 8 players: Round 1 has 4 matches, Round 2 has 2 matches, Round 3 has 1 match
        [DataRow(16, new[] { 8, 4, 2, 1 })] // 16 players: Round 1 has 8 matches, etc.
        [DataRow(32, new[] { 16, 8, 4, 2, 1 })] // 32 players: 5 rounds with decreasing matches
        public async Task MapToGameResultAsync_Should_Map_Correct_Match_Counts_Per_Round(int playerCount, int[] expectedMatchCounts)
        {
            // Arrange
            var gameParent = CreateTestGameParent("Test Tournament", playerCount, false);

            // Act
            var result = await GameMapper.MapToGameResultAsync(gameParent, _playerServiceMock.Object);

            // Assert
            Assert.IsNotNull(result.MainGame);
            Assert.AreEqual(expectedMatchCounts.Length, result.MainGame.Rounds.Count);

            for (int i = 0; i < expectedMatchCounts.Length; i++)
            {
                Assert.AreEqual(expectedMatchCounts[i], result.MainGame.Rounds[i].Matches.Count,
                    $"Round {i} should have {expectedMatchCounts[i]} matches");
                Assert.AreEqual(i, result.MainGame.Rounds[i].RoundNumber,
                    $"Round should have correct round number");
            }
        }

        [TestMethod]
        [DataRow(4, new[] { 1 })] // 4 players, looser round has 2 players, so 1 match
        [DataRow(8, new[] { 2, 1 })] // 8 players, looser round has 4 players, so 2 matches in round 1, 1 in round 2
        [DataRow(16, new[] { 4, 2, 1 })] // 16 players, looser round has 8 players, so 4 matches in round 1, etc.
        [DataRow(32, new[] { 8, 4, 2, 1 })] // 32 players, looser round has 16 players, so 8 matches in round 1, etc.
        public async Task MapToGameResultAsync_Should_Map_Correct_Looser_Round_Match_Counts(int playerCount, int[] expectedLooserMatchCounts)
        {
            // Arrange
            var gameParent = CreateTestGameParent("Test Tournament", playerCount, true);

            // Act
            var result = await GameMapper.MapToGameResultAsync(gameParent, _playerServiceMock.Object);

            // Assert
            Assert.IsNotNull(result.LooserGame);
            Assert.AreEqual(expectedLooserMatchCounts.Length, result.LooserGame.Rounds.Count);

            for (int i = 0; i < expectedLooserMatchCounts.Length; i++)
            {
                Assert.AreEqual(expectedLooserMatchCounts[i], result.LooserGame.Rounds[i].Matches.Count,
                    $"Looser round {i} should have {expectedLooserMatchCounts[i]} matches");
            }
        }

        [TestMethod]
        public async Task MapToGameResultAsync_Should_Map_Player_Ids_And_Names_Correctly()
        {
            // Arrange
            var playerIds = GeneratePlayerIds(8);
            var gameParent = CreateTestGameParentWithPlayerIds("Test Tournament", playerIds, false);

            // Setup player service to return player names
            for (int i = 0; i < playerIds.Count; i++)
            {
                var playerId = playerIds[i];
                _playerServiceMock
                    .Setup(ps => ps.GetPlayerByIdAsync(playerId))
                    .ReturnsAsync(new DartPlayerGetDto { Id = playerId, Name = $"Player {i + 1}" });
            }

            // Act
            var result = await GameMapper.MapToGameResultAsync(gameParent, _playerServiceMock.Object);

            // Assert
            Assert.IsNotNull(result.MainGame);
            var firstRound = result.MainGame.Rounds.First();
            Assert.AreEqual(4, firstRound.Matches.Count);

            // Verify all players are mapped correctly with both IDs and names
            var mappedPlayerIds = new List<Guid>();
            foreach (var match in firstRound.Matches)
            {
                Assert.AreNotEqual(Guid.Empty, match.PlayerAId);
                Assert.AreNotEqual(Guid.Empty, match.PlayerBId);
                Assert.AreNotEqual(match.PlayerAId, match.PlayerBId);
                Assert.IsFalse(string.IsNullOrEmpty(match.PlayerAName));
                Assert.IsFalse(string.IsNullOrEmpty(match.PlayerBName));
                Assert.IsTrue(match.PlayerAName.StartsWith("Player"));
                Assert.IsTrue(match.PlayerBName.StartsWith("Player"));
                mappedPlayerIds.Add(match.PlayerAId);
                mappedPlayerIds.Add(match.PlayerBId);
            }

            // Verify all original player IDs are present
            foreach (var playerId in playerIds)
            {
                Assert.IsTrue(mappedPlayerIds.Contains(playerId),
                    $"Player {playerId} should be mapped correctly");
            }

            // Verify player service was called for each player
            _playerServiceMock.Verify(ps => ps.GetPlayerByIdAsync(It.IsAny<Guid>()), Times.Exactly(8));
        }

        [TestMethod]
        public async Task MapToGameResultAsync_Should_Map_Empty_Matches_For_Subsequent_Rounds()
        {
            // Arrange
            var gameParent = CreateTestGameParent("Test Tournament", 8, false);

            // Act
            var result = await GameMapper.MapToGameResultAsync(gameParent, _playerServiceMock.Object);

            // Assert
            Assert.IsNotNull(result.MainGame);
            var rounds = result.MainGame.Rounds;

            // Skip first round (index 0) and check subsequent rounds have empty matches
            for (int i = 1; i < rounds.Count; i++)
            {
                foreach (var match in rounds[i].Matches)
                {
                    Assert.AreEqual(Guid.Empty, match.PlayerAId,
                        $"Round {i} should have empty PlayerAId");
                    Assert.AreEqual(Guid.Empty, match.PlayerBId,
                        $"Round {i} should have empty PlayerBId");
                    Assert.AreEqual(string.Empty, match.PlayerAName,
                        $"Round {i} should have empty PlayerAName");
                    Assert.AreEqual(string.Empty, match.PlayerBName,
                        $"Round {i} should have empty PlayerBName");
                }
            }
        }

        [TestMethod]
        public async Task MapToGameResultAsync_Should_Map_All_Ids_Correctly()
        {
            // Arrange
            var gameParent = CreateTestGameParent("Test Tournament", 4, false);
            var expectedGameId = Guid.NewGuid();
            var expectedMatchIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            var expectedRoundIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            // Set specific IDs for verification
            gameParent.MainGame.Id = expectedGameId;
            for (int i = 0; i < gameParent.MainGame.Rounds.Count; i++)
            {
                gameParent.MainGame.Rounds[i].Id = expectedRoundIds[i];
                for (int j = 0; j < gameParent.MainGame.Rounds[i].Matches.Count; j++)
                {
                    if (i == 0 && j < expectedMatchIds.Count)
                    {
                        gameParent.MainGame.Rounds[i].Matches[j].Id = expectedMatchIds[j];
                    }
                }
            }

            // Act
            var result = await GameMapper.MapToGameResultAsync(gameParent, _playerServiceMock.Object);

            // Assert
            Assert.AreEqual(expectedGameId, result.MainGame.Id);
            for (int i = 0; i < result.MainGame.Rounds.Count; i++)
            {
                Assert.AreEqual(expectedRoundIds[i], result.MainGame.Rounds[i].Id);
            }
            Assert.AreEqual(expectedMatchIds[0], result.MainGame.Rounds[0].Matches[0].Id);
            Assert.AreEqual(expectedMatchIds[1], result.MainGame.Rounds[0].Matches[1].Id);
        }

        [TestMethod]
        public async Task MapToGameResultAsync_Should_Handle_Missing_Players()
        {
            // Arrange
            var playerIds = GeneratePlayerIds(4);
            var gameParent = CreateTestGameParentWithPlayerIds("Test Tournament", playerIds, false);

            // Setup player service to return null for some players
            _playerServiceMock
                .Setup(ps => ps.GetPlayerByIdAsync(playerIds[0]))
                .ReturnsAsync(new DartPlayerGetDto { Id = playerIds[0], Name = "Player 1" });
            
            _playerServiceMock
                .Setup(ps => ps.GetPlayerByIdAsync(playerIds[1]))
                .ReturnsAsync((DartPlayerGetDto)null);

            _playerServiceMock
                .Setup(ps => ps.GetPlayerByIdAsync(playerIds[2]))
                .ReturnsAsync(new DartPlayerGetDto { Id = playerIds[2], Name = "Player 3" });

            _playerServiceMock
                .Setup(ps => ps.GetPlayerByIdAsync(playerIds[3]))
                .ReturnsAsync((DartPlayerGetDto)null);

            // Act
            var result = await GameMapper.MapToGameResultAsync(gameParent, _playerServiceMock.Object);

            // Assert
            var firstRound = result.MainGame.Rounds.First();
            
            // First match: Player 1 vs missing player
            Assert.AreEqual("Player 1", firstRound.Matches[0].PlayerAName);
            Assert.AreEqual(string.Empty, firstRound.Matches[0].PlayerBName);
            
            // Second match: Player 3 vs missing player
            Assert.AreEqual("Player 3", firstRound.Matches[1].PlayerAName);
            Assert.AreEqual(string.Empty, firstRound.Matches[1].PlayerBName);
        }

        [TestMethod]
        public async Task MapToGameResultAsync_Should_Handle_Null_Rounds_Gracefully()
        {
            // Arrange
            var gameParent = CreateTestGameParent("Test Tournament", 4, false);
            gameParent.MainGame = new DartTournament.Domain.Entities.Game(null); // null rounds

            // Act
            var result = await GameMapper.MapToGameResultAsync(gameParent, _playerServiceMock.Object);

            // Assert
            Assert.IsNotNull(result.MainGame);
            Assert.IsNotNull(result.MainGame.Rounds);
            Assert.AreEqual(0, result.MainGame.Rounds.Count);
        }

        [TestMethod]
        public async Task MapToGameResultAsync_Should_Handle_Null_Matches_Gracefully()
        {
            // Arrange
            var gameParent = CreateTestGameParent("Test Tournament", 4, false);
            foreach (var round in gameParent.MainGame.Rounds)
            {
                round.Matches = null; // null matches
            }

            // Act
            var result = await GameMapper.MapToGameResultAsync(gameParent, _playerServiceMock.Object);

            // Assert
            Assert.IsNotNull(result.MainGame);
            foreach (var round in result.MainGame.Rounds)
            {
                Assert.IsNotNull(round.Matches);
                Assert.AreEqual(0, round.Matches.Count);
            }
        }

        [TestMethod]
        public async Task MapToGameResultAsync_Should_Not_Call_PlayerService_For_Empty_Guids()
        {
            // Arrange
            var gameParent = CreateTestGameParent("Test Tournament", 8, false);
            // This creates a tournament where only the first round has player IDs, subsequent rounds have empty GUIDs

            // Act
            var result = await GameMapper.MapToGameResultAsync(gameParent, _playerServiceMock.Object);

            // Assert
            Assert.IsNotNull(result);
            
            // Verify player service was only called for non-empty GUIDs (first round matches)
            var firstRoundMatchCount = gameParent.MainGame.Rounds[0].Matches.Count;
            var expectedCalls = firstRoundMatchCount * 2; // 2 players per match
            _playerServiceMock.Verify(ps => ps.GetPlayerByIdAsync(It.IsAny<Guid>()), Times.Exactly(expectedCalls));
        }

        [TestMethod]
        public async Task MapToGameResultAsync_With_Null_GameParent_Should_Throw_ArgumentNullException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => GameMapper.MapToGameResultAsync(null, _playerServiceMock.Object));
            Assert.AreEqual("gameParent", exception.ParamName);
        }

        [TestMethod]
        public async Task MapToGameResultAsync_With_Null_MainGame_Should_Throw_ArgumentNullException()
        {
            // Arrange
            var gameParent = new GameParent("Test", null, null, false);

            // Act & Assert
            var exception = await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => GameMapper.MapToGameResultAsync(gameParent, _playerServiceMock.Object));
            Assert.AreEqual("game", exception.ParamName);
        }

        [TestMethod]
        public async Task MapToGameResultAsync_Tournament_Names_Should_Be_Mapped_Correctly()
        {
            // Arrange
            var tournamentNames = new[] 
            { 
                "Championship 2024", 
                "Spring Tournament", 
                "Finals", 
                "Local Cup",
                "Special Characters: הצü!@#$%^&*()"
            };

            foreach (var name in tournamentNames)
            {
                var gameParent = CreateTestGameParent(name, 4, false);

                // Act
                var result = await GameMapper.MapToGameResultAsync(gameParent, _playerServiceMock.Object);

                // Assert
                Assert.AreEqual(name, result.Name, $"Tournament name '{name}' should be mapped correctly");
            }
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

            var gameParent = new GameParent(name, mainGame, looserGame, hasLooserRound);
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