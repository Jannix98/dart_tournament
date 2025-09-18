using DartTournament.Presentation.Base.Services;
using DartTournament.Presentation.Services;
using DartTournament.WPF.Controls.GameTreeControl;
using DartTournament.WPF.Dialogs.SelectWinner;
using DartTournament.WPF.Utils.MatchHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Test
{
    [TestClass]
    public class GameMatchHandlerTest
    {
        Mock<IMatchPresentationService> _mockMatchPresentationService;

        [TestInitialize]
        public void ClassInitialize()
        {
            // Initialize the ServiceManager to avoid dependency issues
            // This ensures services are properly configured for the test
            var _ = DartTournament.WPF.SM.ServiceManager.Instance;
            _mockMatchPresentationService = new Mock<IMatchPresentationService>();
        }

        [TestMethod]
        public async Task Test1()
        {
            var match1 = new MatchViewModel(Guid.NewGuid(), Guid.Empty, Guid.Empty, "Team A", "Team B", 0, 0, Guid.Empty);
            var match2 = new MatchViewModel(Guid.NewGuid(), Guid.Empty, Guid.Empty, "Team C", "Team D", 0, 1, Guid.Empty);
            var match3 = new MatchViewModel(Guid.NewGuid(), Guid.Empty, Guid.Empty, "", "", 1, 0, Guid.Empty);

            // Arrange
            var matches = new List<MatchViewModel>
            {
                match1,
                match2,
                match3
            };

            
            var gameMatchHandler = new GameMatchHandler(matches, null, _mockMatchPresentationService.Object);

            SelectWinnerResult winnerResult1 = new SelectWinnerResult(Guid.Empty, "Team B", Guid.Empty, "Team A", true);
            SelectWinnerResult winnerResult2 = new SelectWinnerResult(Guid.Empty, "Team C", Guid.Empty, "Team D", true);

            await gameMatchHandler.SetToNextMatch(0, 0, winnerResult1);
            await gameMatchHandler.SetToNextMatch(0, 1, winnerResult2);

            Assert.AreEqual("Team B", match3.Player1Name);
            Assert.AreEqual("Team C", match3.Player2Name);
        }

        [TestMethod]
        public async Task Test2()
        {
            var match1 = new MatchViewModel(Guid.NewGuid(), Guid.Empty, Guid.Empty, "Team A", "Team B", 0, 0, Guid.Empty);
            var match2 = new MatchViewModel(Guid.NewGuid(), Guid.Empty, Guid.Empty, "Team C", "Team D", 0, 1, Guid.Empty);
            var match3 = new MatchViewModel(Guid.NewGuid(), Guid.Empty, Guid.Empty, "", "", 1, 0, Guid.Empty);

            // Arrange
            var matches = new List<MatchViewModel>
            {
                match1,
                match2, 
                match3
            };

            var looserMatch1 = new MatchViewModel(Guid.NewGuid(), Guid.Empty, Guid.Empty, "", "", 0, 0, Guid.Empty);
            var looserMatches = new List<MatchViewModel>
            {
                looserMatch1
            };

            var looserMatchHandler = new LooserGameMatchHandler(looserMatches, _mockMatchPresentationService.Object);
            var gameMatchHandler = new GameMatchHandler(matches, looserMatchHandler, _mockMatchPresentationService.Object);

            SelectWinnerResult winnerResult1 = new SelectWinnerResult(Guid.Empty, "Team B", Guid.Empty, "Team A", true);
            SelectWinnerResult winnerResult2 = new SelectWinnerResult(Guid.Empty, "Team C", Guid.Empty, "Team D", true);

            await gameMatchHandler.SetToNextMatch(0, 0, winnerResult1);
            await gameMatchHandler.SetToNextMatch(0, 1, winnerResult2);

            Assert.AreEqual("Team B", match3.Player1Name);
            Assert.AreEqual("Team C", match3.Player2Name);

            Assert.AreEqual("Team A", looserMatch1.Player1Name);
            Assert.AreEqual("Team D", looserMatch1.Player2Name);
        }

        [TestMethod]
        public async Task TestLooserBracketHandlesEliminatedPlayersCorrectly()
        {
            // Arrange: Create main tournament matches
            var playerAId = Guid.NewGuid();
            var playerBId = Guid.NewGuid();
            var playerCId = Guid.NewGuid();
            var playerDId = Guid.NewGuid();

            var mainMatch1 = new MatchViewModel(Guid.NewGuid(), playerAId, playerBId, "Player A", "Player B", 0, 0, Guid.Empty);
            var mainMatch2 = new MatchViewModel(Guid.NewGuid(), playerCId, playerDId, "Player C", "Player D", 0, 1, Guid.Empty);
            var mainMatch3 = new MatchViewModel(Guid.NewGuid(), Guid.Empty, Guid.Empty, "", "", 1, 0, Guid.Empty);

            var mainMatches = new List<MatchViewModel>
            {
                mainMatch1,
                mainMatch2,
                mainMatch3
            };

            // Create loser bracket matches
            var looserMatch1 = new MatchViewModel(Guid.NewGuid(), Guid.Empty, Guid.Empty, "", "", 0, 0, Guid.Empty);
            var looserMatch2 = new MatchViewModel(Guid.NewGuid(), Guid.Empty, Guid.Empty, "", "", 1, 0, Guid.Empty);

            var looserMatches = new List<MatchViewModel>
            {
                looserMatch1,
                looserMatch2
            };

            var looserMatchHandler = new LooserGameMatchHandler(looserMatches, _mockMatchPresentationService.Object);
            var gameMatchHandler = new GameMatchHandler(mainMatches, looserMatchHandler, _mockMatchPresentationService.Object);

            // Act: Process main tournament matches
            // Player B beats Player A (Player A goes to loser bracket)
            SelectWinnerResult winnerResult1 = new SelectWinnerResult(playerBId, "Player B", playerAId, "Player A", true);
            await gameMatchHandler.SetToNextMatch(0, 0, winnerResult1);

            // Player C beats Player D (Player D goes to loser bracket)
            SelectWinnerResult winnerResult2 = new SelectWinnerResult(playerCId, "Player C", playerDId, "Player D", true);
            await gameMatchHandler.SetToNextMatch(0, 1, winnerResult2);

            // Assert: Check main tournament progression
            Assert.AreEqual("Player B", mainMatch3.Player1Name);
            Assert.AreEqual("Player C", mainMatch3.Player2Name);
            Assert.AreEqual(playerBId, mainMatch3.IdGameEntityA);
            Assert.AreEqual(playerCId, mainMatch3.IdGameEntityB);

            // Assert: Check loser bracket - eliminated players should be placed as participants
            Assert.AreEqual("Player A", looserMatch1.Player1Name);
            Assert.AreEqual("Player D", looserMatch1.Player2Name);
            Assert.AreEqual(playerAId, looserMatch1.IdGameEntityA);
            Assert.AreEqual(playerDId, looserMatch1.IdGameEntityB);

            // Act: Process loser bracket match
            // Player A beats Player D in loser bracket (Player D is eliminated completely)
            SelectWinnerResult looserResult = new SelectWinnerResult(playerAId, "Player A", playerDId, "Player D", true);
            await looserMatchHandler.SetToNextMatch(0, 0, looserResult);

            // Assert: Check loser bracket progression - is set in the correct match
            Assert.AreEqual(playerAId, looserMatch1.WinnerId);
        }

        /// <summary>
        /// This test validates that the LooserGameMatchHandler correctly treats eliminated players
        /// from the main tournament as participants (not losers) in the loser bracket.
        /// This addresses the bug where losers were incorrectly handled in the loser bracket.
        /// </summary>
        [TestMethod]
        public async Task TestFixForLooserGameMatchHandlerDoesNotAssumePlayerIsLooser()
        {
            // Arrange: Setup a scenario that demonstrates the fix
            var playerAId = Guid.NewGuid();
            var playerBId = Guid.NewGuid();

            // Main tournament match
            var mainMatch = new MatchViewModel(Guid.NewGuid(), playerAId, playerBId, "Alice", "Bob", 0, 0, Guid.Empty);
            var mainMatches = new List<MatchViewModel> { mainMatch };

            // Loser bracket match - initially empty
            var looserMatch = new MatchViewModel(Guid.NewGuid(), Guid.Empty, Guid.Empty, "", "", 0, 0, Guid.Empty);
            var looserMatches = new List<MatchViewModel> { looserMatch };

            var looserMatchHandler = new LooserGameMatchHandler(looserMatches, _mockMatchPresentationService.Object);
            var gameMatchHandler = new GameMatchHandler(mainMatches, looserMatchHandler, _mockMatchPresentationService.Object);

            // Act: Alice beats Bob in main tournament
            SelectWinnerResult mainTournamentResult = new SelectWinnerResult(playerAId, "Alice", playerBId, "Bob", true);
            await gameMatchHandler.SetToNextMatch(0, 0, mainTournamentResult);

            // Assert: Bob (the eliminated player) should be placed in the loser bracket as a participant
            // Before the fix: the LooserGameMatchHandler would incorrectly use UseWinnerInResult=false 
            // and place Alice (the winner) instead of Bob (the eliminated player)
            // After the fix: Bob should be correctly placed in the loser bracket
            Assert.AreEqual("Bob", looserMatch.Player1Name, 
                "The eliminated player (Bob) should be placed in the loser bracket, not the winner (Alice)");
            Assert.AreEqual(playerBId, looserMatch.IdGameEntityA, 
                "The eliminated player's ID should be correctly set in the loser bracket");

            // Verify that the main tournament winner is NOT in the loser bracket
            Assert.AreNotEqual("Alice", looserMatch.Player1Name, 
                "The winner (Alice) should NOT be placed in the loser bracket");
            Assert.AreNotEqual(playerAId, looserMatch.IdGameEntityA, 
                "The winner's ID should NOT be in the loser bracket");
        }

    }
}
