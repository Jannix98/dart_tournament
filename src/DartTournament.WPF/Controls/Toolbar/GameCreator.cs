using DartTournament.WPF.Controls.Game.GameMenue;
using DartTournament.WPF.Controls.GameSession;
using DartTournament.WPF.Dialogs.Base;
using DartTournament.WPF.Dialogs.CreateGame;
using DartTournament.WPF.Screens.StartScreen;
using DartTournament.WPF.Utils;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Controls.Toolbar
{
    internal class GameCreator
    {
        public void CreateGame()
        {
            CreateGameViewResult result;
            bool flowControl = ShowPlayerSelectionDialog(out result);
            if (!flowControl)
            {
                return;
            }

            //var control = GameTreeControl.GameTreeControl.CreateGame(result.SelectedPlayers.Count, result.SelectedPlayers);
            var control = new GameSessionControl(result.TournamentName, result.AddLooserRound, result.SelectedPlayers);

            Mediator.Notify("AddMenuItem", new ApplicationMenueItem(result.TournamentName, control, PackIconKind.ControllerClassicOutline, PackIconKind.ControllerClassic, false));
        }

        private static bool ShowPlayerSelectionDialog(out CreateGameViewResult result)
        {
            IDialogOwner owner = new DialogOwner(); // TODO: implement interface of CreateGameView
            CreateGameView createGameView = new CreateGameView(owner);
            result = createGameView.ShowDialog();
            if (result.DialogResult == false)
                return false;

            return true;
        }
    }
}
