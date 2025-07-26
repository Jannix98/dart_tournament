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
        [TestMethod]
        public void Test1()
        {
            var match1 = new MatchViewModel { RoundIndex = 0, MatchIndex = 0, Team1Name = "Team A", Team2Name = "Team B" };
            var match2 = new MatchViewModel { RoundIndex = 0, MatchIndex = 1, Team1Name = "Team C", Team2Name = "Team D" };
            var match3 = new MatchViewModel { RoundIndex = 1, MatchIndex = 0, Team1Name = "", Team2Name = "" };

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

            gameMatchHandler.SetToNextMatch(0, 0, winnerResult1);
            gameMatchHandler.SetToNextMatch(0, 1, winnerResult2);

            Assert.AreEqual("Team B", match3.Team1Name);
            Assert.AreEqual("Team C", match3.Team2Name);
        }

        [TestMethod]
        public void Test2()
        {
            var match1 = new MatchViewModel { RoundIndex = 0, MatchIndex = 0, Team1Name = "Team A", Team2Name = "Team B" };
            var match2 = new MatchViewModel { RoundIndex = 0, MatchIndex = 1, Team1Name = "Team C", Team2Name = "Team D" };
            var match3 = new MatchViewModel { RoundIndex = 1, MatchIndex = 0, Team1Name = "", Team2Name = "" };

            // Arrange
            var matches = new List<MatchViewModel>
            {
                match1,
                match2, 
                match3
            };

            var looserMatch1 = new MatchViewModel { RoundIndex = 0, MatchIndex = 0, Team1Name = "", Team2Name = "" };
            var looserMatches = new List<MatchViewModel>
            {
                looserMatch1
            };

            var looserMatchHandler = new LooserGameMatchHandler(looserMatches);
            var gameMatchHandler = new GameMatchHandler(matches, looserMatchHandler);

            SelectWinnerResult winnerResult1 = new SelectWinnerResult(Guid.Empty, "Team B", Guid.Empty, "Team A", true);
            SelectWinnerResult winnerResult2 = new SelectWinnerResult(Guid.Empty, "Team C", Guid.Empty, "Team D", true);

            gameMatchHandler.SetToNextMatch(0, 0, winnerResult1);
            gameMatchHandler.SetToNextMatch(0, 1, winnerResult2);

            Assert.AreEqual("Team B", match3.Team1Name);
            Assert.AreEqual("Team C", match3.Team2Name);

            Assert.AreEqual("Team A", looserMatch1.Team1Name);
            Assert.AreEqual("Team D", looserMatch1.Team2Name);
        }

    }
}
