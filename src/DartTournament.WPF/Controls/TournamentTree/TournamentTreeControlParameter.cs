using DartTournament.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Controls.TournamentTree
{
    internal class TournamentTreeControlParameter
    {
        public List<DartPlayerUI> Players;
        public int MaxPlayers => Players != null ? Players.Count : 0;    
        public bool IsAdvancedGame { get; set; } = false;
        public string TournamentName { get; set; } = string.Empty;
        public TournamentTreeControlParameter(List<DartPlayerUI> players, bool isAdvancedGame, string tournamentName)
        {
            Players = players ?? new List<DartPlayerUI>();
            IsAdvancedGame = isAdvancedGame;
            TournamentName = tournamentName;
        }
    }
}
