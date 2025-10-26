using DartTournament.Presentation.Base.Services;
using DartTournament.WPF.Controls.GameNavigationRail;
using DartTournament.WPF.Controls.GameSession;
using DartTournament.WPF.Controls.LoadGame;
using DartTournament.WPF.Utils;
using MaterialDesignThemes.Wpf;
using System;
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
            var selectedGame = await ShowGameSelectionDialogAsync();
            if (selectedGame == null)
            {
                return;
            }
            
            var game = await _gamePresentationService.GetGame(selectedGame.Id);
            var control = new GameSessionControl(game);

            Mediator.Notify("AddMenuItem", new GameNavigationItem(game.Name, control, PackIconKind.ControllerClassicOutline, PackIconKind.ControllerClassic, false));
        }

        private async Task<DartTournament.Application.DTO.Game.GameResult> ShowGameSelectionDialogAsync()
        {
            // Create the dialog content
            var loadGameDialog = new LoadGameDialog();
            
            // Show the dialog using DialogHost
            var result = await DialogHost.Show(loadGameDialog, "RootDialogHost");
            
            // Return the selected game or null if dialog was canceled
            return result as DartTournament.Application.DTO.Game.GameResult;
        }
    }
}
