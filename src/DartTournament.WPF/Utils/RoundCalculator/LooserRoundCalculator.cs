using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Utils.RoundCalculator
{
    internal class LooserRoundCalculator : RoundCalculatorBase
    {
        protected override int MinmalRoundCount()
        {
            return 2;
        }
    }
}
