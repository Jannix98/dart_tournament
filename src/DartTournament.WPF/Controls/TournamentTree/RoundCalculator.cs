using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Controls.TournamentTree
{
    public static class RoundCalculator
    {
        public static int GetRoundCount(int teamCount)
        {
            // Valid team sizes: 4, 8, 16, 32, ...
            // Each round halves the teams until 1 remains
            if (teamCount < 4 || (teamCount & (teamCount - 1)) != 0)
                throw new ArgumentException("Team count must be a power of 2 and at least 4.");
            int rounds = 0;
            while (teamCount > 1)
            {
                teamCount /= 2;
                rounds++;
            }
            return rounds;
        }
    }
}
