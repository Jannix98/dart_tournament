using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Helper.RoundCalculator
{
    public abstract class RoundCalculatorBase
    {
        protected abstract int MinmalRoundCount();

        public int GetRoundCount(int teamCount)
        {
            // Valid team sizes: 4, 8, 16, 32, ...
            // Each round halves the teams until 1 remains
            if (teamCount < MinmalRoundCount() || (teamCount & (teamCount - 1)) != 0)
                throw new ArgumentException($"Team count must be a power of 2 and at least {MinmalRoundCount}.");
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
