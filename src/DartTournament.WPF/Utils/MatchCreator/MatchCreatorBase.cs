using DartTournament.WPF.Models;
using DartTournament.WPF.Utils.GameData;
using DartTournament.WPF.Utils.RoundCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Utils.MatchCreator
{
    internal abstract class MatchCreatorBase<T> : IMatchCreator
    {
        protected GameDataCreator _gameDataCreator;

        protected MatchCreatorBase(GameDataCreator gameDataCreator)
        {
            _gameDataCreator = gameDataCreator;
        }

        public abstract T Create(int maxPlayer, List<DartPlayerUI> players);
    }

    internal interface IMatchCreator
    {
    }
}
