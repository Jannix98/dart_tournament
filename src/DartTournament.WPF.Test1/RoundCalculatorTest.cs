using DartTournament.WPF.Controls.TournamentTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DartTournament.WPF.Test
{
    [TestClass]
    public sealed class RoundCalculatorTest
    {
        [TestMethod]
        [DataRow(4, 2)]
        [DataRow(8, 3)]
        [DataRow(16, 4)]
        [DataRow(32, 5)]
        [DataRow(64, 6)]
        [DataRow(128, 7)]
        public void GetRoundCount_ValidPowersOfTwo_ReturnsExpectedRounds(int teamCount, int expectedRounds)
        {
            int actualRounds = RoundCalculator.GetRoundCount(teamCount);
            Assert.AreEqual(expectedRounds, actualRounds, $"Expected {expectedRounds} rounds for {teamCount} teams.");
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(6)]
        [DataRow(7)]
        [DataRow(9)]
        [DataRow(15)]
        [DataRow(17)]
        [DataRow(127)]
        public void GetRoundCount_InvalidTeamCounts_ThrowsArgumentException(int teamCount)
        {
            Assert.ThrowsException<ArgumentException>(() => RoundCalculator.GetRoundCount(teamCount));
        }
    }
}
