using DartTournament.Application.DTO.Game;
using DartTournament.Presentation.Base.Services;
using DartTournament.WPF.Controls.CreateGame;
using DartTournament.WPF.Controls.GameNavigationRail;
using DartTournament.WPF.Controls.GameSession;
using DartTournament.WPF.Utils;
using MaterialDesignThemes.Wpf;
using System.Linq;
using System.Threading.Tasks;

namespace DartTournament.WPF.Controls.Toolbar
{
    internal class GameCreator
    {
        private IGamePresentationService _gamePresentationService;

        public GameCreator(IGamePresentationService gamePresentationService)
        {
            _gamePresentationService = gamePresentationService;
        }

        public async void CreateGame()
        {
            var result = await ShowPlayerSelectionDialogAsync();
            if (result == null || !result.DialogResult)
            {
                return;
            }

            CreateGameDTO createGame = new CreateGameDTO(
                result.TournamentName, 
                result.SelectedPlayers.Count, 
                result.SelectedPlayers.Select(x => x.Id).ToList(), 
                result.AdvancedMode);
                
            var guid = await _gamePresentationService.CreateGame(createGame);
            var game = await _gamePresentationService.GetGame(guid);

            var control = new GameSessionControl(game);

            Mediator.Notify("AddMenuItem", new GameNavigationItem(
                result.TournamentName, 
                control, 
                PackIconKind.ControllerClassicOutline, 
                PackIconKind.ControllerClassic, 
                false));
        }

        private async Task<CreateGameDialogResult> ShowPlayerSelectionDialogAsync()
        {
            // Create the dialog content
            var createGameDialog = new CreateGameDialog();
            
            // Show the dialog using DialogHost
            var result = await DialogHost.Show(createGameDialog, "RootDialogHost");
            
            // Return the result or null if dialog was canceled
            return result as CreateGameDialogResult;
        }
    }
}
