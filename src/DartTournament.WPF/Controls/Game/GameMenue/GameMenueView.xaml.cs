using DartTournament.WPF.Controls.GameSession;
using DartTournament.WPF.Controls.TournamentTree;
using DartTournament.WPF.Dialogs.Base;
using DartTournament.WPF.Dialogs.CreateGame;
using DartTournament.WPF.Navigator;
using DartTournament.WPF.Screens.StartScreen;
using DartTournament.WPF.Utils;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DartTournament.WPF.Controls.Game.GameMenue
{
    /// <summary>
    /// Interaction logic for GameMenueView.xaml
    /// </summary>
    public partial class GameMenueView : UserControl
    {
        private IScreenNavigator _navigator;

        public GameMenueView(IScreenNavigator navigator)
        {
            InitializeComponent();
            _navigator = navigator;
        }

        private void LoadGameClick(object sender, RoutedEventArgs e)
        {
           MessageBox.Show("Load Game Clicked", "This feature is not implemented yet.", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddNewGameClick(object sender, RoutedEventArgs e)
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
