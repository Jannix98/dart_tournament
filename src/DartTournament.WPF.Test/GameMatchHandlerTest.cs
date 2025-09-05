using DartTournament.WPF.Controls.GameTreeControl;
using DartTournament.WPF.Dialogs.SelectWinner;
using DartTournament.WPF.Utils.MatchHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Initialize the ServiceManager to avoid dependency issues
            // This ensures services are properly configured for the test
            var _ = DartTournament.WPF.SM.ServiceManager.Instance;
        }

        [TestMethod]
        public async Task Test1()
        {
            var match1 = new MatchViewModel(Guid.NewGuid(), Guid.Empty, Guid.Empty, "Team A", "Team B", 0, 0);
            var match2 = new MatchViewModel(Guid.NewGuid(), Guid.Empty, Guid.Empty, "Team C", "Team D", 0, 1);
            var match3 = new MatchViewModel(Guid.NewGuid(), Guid.Empty, Guid.Empty, "", "", 1, 0);

            // Arrange
            var matches = new List<MatchViewModel>
            {
                match1,
                match2,
                match3
            };

            
            var gameMatchHandler = new GameMatchHandler(matches, null);

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
            var match1 = new MatchViewModel(Guid.NewGuid(), Guid.Empty, Guid.Empty, "Team A", "Team B", 0, 0);
            var match2 = new MatchViewModel(Guid.NewGuid(), Guid.Empty, Guid.Empty, "Team C", "Team D", 0, 1);
            var match3 = new MatchViewModel(Guid.NewGuid(), Guid.Empty, Guid.Empty, "", "", 1, 0);

            // Arrange
            var matches = new List<MatchViewModel>
            {
                match1,
                match2, 
                match3
            };

            var looserMatch1 = new MatchViewModel(Guid.NewGuid(), Guid.Empty, Guid.Empty, "", "", 0, 0);
            var looserMatches = new List<MatchViewModel>
            {
                looserMatch1
            };

            var looserMatchHandler = new LooserGameMatchHandler(looserMatches);
            var gameMatchHandler = new GameMatchHandler(matches, looserMatchHandler);

            SelectWinnerResult winnerResult1 = new SelectWinnerResult(Guid.Empty, "Team B", Guid.Empty, "Team A", true);
            SelectWinnerResult winnerResult2 = new SelectWinnerResult(Guid.Empty, "Team C", Guid.Empty, "Team D", true);

            await gameMatchHandler.SetToNextMatch(0, 0, winnerResult1);
            await gameMatchHandler.SetToNextMatch(0, 1, winnerResult2);

            Assert.AreEqual("Team B", match3.Player1Name);
            Assert.AreEqual("Team C", match3.Player2Name);

            Assert.AreEqual("Team A", looserMatch1.Player1Name);
            Assert.AreEqual("Team D", looserMatch1.Player2Name);
        }

    }
}
