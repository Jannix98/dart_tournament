using DartTournament.WPF.Controls.GameTreeControl;
using DartTournament.Application.DTO.Game;
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

        public static List<RoundViewModel> FromGameDTO(GameDTO gameDTO)
        {
            var rounds = new List<RoundViewModel>();
            
            foreach (var roundDTO in gameDTO.Rounds.OrderBy(r => r.RoundNumber))
            {
                var roundViewModel = new RoundViewModel();
                
                foreach (var matchDTO in roundDTO.Matches)
                {
                    var matchViewModel = new MatchViewModel(
                        matchDTO.Id, 
                        matchDTO.PlayerAId, 
                        matchDTO.PlayerBId, 
                        matchDTO.PlayerAName, 
                        matchDTO.PlayerBName, 
                        roundDTO.RoundNumber, 
                        roundDTO.Matches.IndexOf(matchDTO), 
                        matchDTO.WinnerId);
                    
                    roundViewModel.Matches.Add(matchViewModel);
                }
                
                rounds.Add(roundViewModel);
            }
            
            return rounds;
        }

        public static List<MatchViewModel> FromGameDTO(GameDTO gameDTO, bool flatten = true)
        {
            var rounds = FromGameDTO(gameDTO);
            return flatten ? FromRounds(rounds) : new List<MatchViewModel>();
        }
    }
}
