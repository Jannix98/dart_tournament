using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Helper.RoundCalculator
{
    public class LooserRoundCalculator : RoundCalculatorBase
    {
        protected override int MinmalRoundCount()
        {
            return 2;
        }
    }
}
