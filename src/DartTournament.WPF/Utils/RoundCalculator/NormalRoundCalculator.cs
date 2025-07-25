using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Utils.RoundCalculator
{
    internal class NormalRoundCalculator : RoundCalculatorBase
    {
        protected override int MinmalRoundCount()
        {
            return 4;
        }
    }
}
