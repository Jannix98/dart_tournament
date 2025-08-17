using DartTournament.Presentation.Base.Services;
using DartTournament.WPF.Controls.GameNavigationRail;
using DartTournament.WPF.Controls.GameSession;
using DartTournament.WPF.Dialogs.Base;
using DartTournament.WPF.Dialogs.LoadGame;
using DartTournament.WPF.Utils;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Controls.Toolbar
{
    internal class GameLoader
    {
        private IGamePresentationService _gamePresentationService;

        public GameLoader(IGamePresentationService gamePresentationService)
        {
            _gamePresentationService = gamePresentationService;
        }

        public async void LoadGame()
        {
            Guid? guid = ShowGameSelectionDialog();
            if (guid == null)
            {
                return;
            }
            var game = await _gamePresentationService.GetGame(guid.Value);
            var control = new GameSessionControl(game);

            Mediator.Notify("AddMenuItem", new GameNavigationItem(game.Name, control, PackIconKind.ControllerClassicOutline, PackIconKind.ControllerClassic, false));
        }

        private Guid? ShowGameSelectionDialog()
        {
            LoadGameView dialog = new LoadGameView(new DialogOwner());
            LoadGameViewResult result = dialog.ShowDialog();
            if (result.DialogResult == true && result.SelectedGame != null)
            {
                return result.SelectedGame.Id;
            }
            else
            {
                return null;
            }
        }
    }
}
