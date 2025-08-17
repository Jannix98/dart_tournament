using DartTournament.WPF.Controls.GameTreeControl;
using DartTournament.WPF.Models;
using DartTournament.WPF.Utils.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Utils.MatchCreator
{
    //internal class NormalMatchCreator : MatchCreatorBase<NormalMatchData>
    //{
    //    public NormalMatchCreator(GameDataCreator gameDataCreator) : base(gameDataCreator)
    //    {
    //    }

    //    public override NormalMatchData Create(int maxPlayer, List<DartPlayerUI> players)
    //    {
    //        var rounds = _gameDataCreator.Create(maxPlayer, players);
    //        if (rounds.Count == 0)
    //        {
    //            throw new InvalidOperationException("No rounds created.");
    //        }
    //        NormalMatchData matchData = new NormalMatchData
    //        {
    //            GameRounds = rounds,
    //        };
    //        return matchData;
    //    }
    //}
}
