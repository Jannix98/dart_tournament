using DartTournament.WPF.Controls.GameTreeControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Utils.MatchGenerator
{
    internal static class MatchGenerator
    {
        public static List<MatchViewModel> FromRounds(List<RoundViewModel> rounds)
        {
            var all = new List<MatchViewModel>();
            for (int round = 0; round < rounds.Count; round++)
            {
                for (int match = 0; match < rounds[round].Matches.Count; match++)
                {
                    var m = rounds[round].Matches[match];
                    all.Add(m);
                }
            }
            return all;
        }
    }
}
