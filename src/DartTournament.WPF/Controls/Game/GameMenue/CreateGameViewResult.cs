using DartTournament.WPF.Dialogs.Base;
using DartTournament.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Controls.Game.GameMenue
{
    internal class CreateGameViewResult : BaseDialogResult
    {
        public string TournamentName { get; set; } = string.Empty;


        public List<DartPlayerUI> SelectedPlayers { get; private set; }
        public bool IsAdvancedGame { get; private set; } = false;


        public CreateGameViewResult(BaseDialogResult baseDialogResult, List<DartPlayerUI> selectedPlayers, bool isAdvancedGame, string tournamentName) : base(baseDialogResult)
        {
            SelectedPlayers = selectedPlayers;
            IsAdvancedGame = isAdvancedGame;
            TournamentName = tournamentName ?? string.Empty;
        }
    }
}
