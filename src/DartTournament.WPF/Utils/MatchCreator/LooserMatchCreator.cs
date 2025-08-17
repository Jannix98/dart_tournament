using DartTournament.WPF.Models;
using DartTournament.WPF.Utils.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Utils.MatchCreator
{
    //internal class LooserMatchCreator : MatchCreatorBase<LooserMatchData>
    //{
    //    public LooserMatchCreator(GameDataCreator gameDataCreator) : base(gameDataCreator)
    //    {
    //    }

    //    public override LooserMatchData Create(int maxPlayer, List<DartPlayerUI> players)
    //    {
    //        var rounds = _gameDataCreator.Create(maxPlayer, players);
    //        if (rounds.Count == 0)
    //        {
    //            throw new InvalidOperationException("No rounds created.");
    //        }
    //        int looserMatchCount = (maxPlayer / 2); 
    //        List<DartPlayerUI> looserMatchPlayer = new List<DartPlayerUI>();
    //        var looserRounds = _gameDataCreator.Create(looserMatchCount, looserMatchPlayer);
    //        LooserMatchData matchData = new LooserMatchData
    //        {
    //            GameRounds = rounds,
    //            LooserRounds = looserRounds
    //        };
    //        return matchData;
    //    }
    //}
}
