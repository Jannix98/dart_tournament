using DartTournament.WPF.Controls.TournamentTree;
using DartTournament.WPF.Dialogs.Base;
using DartTournament.WPF.Dialogs.CreateGame;
using DartTournament.WPF.Navigator;
using DartTournament.WPF.Screens.StartScreen;
using DartTournament.WPF.Utils;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateGameViewResult result;
            bool flowControl = ShowPlayerSelectionDialog(out result);
            if (!flowControl)
            {
                return;
            }

            var control = GameTreeControl.GameTreeControl.CreateGame(result.SelectedPlayers.Count, result.SelectedPlayers);

            Mediator.Notify("AddMenuItem1", new ApplicationMenueItem("Test1", control, MaterialDesignThemes.Wpf.PackIconKind.GamepadCircle, MaterialDesignThemes.Wpf.PackIconKind.GamepadCircleOutline, false));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateGameViewResult result;
            bool flowControl = ShowPlayerSelectionDialog(out result);
            if (!flowControl)
            {
                return;
            }

            TournamentTreeControlParameter parameter = new TournamentTreeControlParameter(result.SelectedPlayers, result.IsAdvancedGame, result.TournamentName);

            Mediator.Notify("AddMenuItem", new ApplicationMenueItem("Test", new TournamentTreeControl(parameter), MaterialDesignThemes.Wpf.PackIconKind.GamepadSquare, MaterialDesignThemes.Wpf.PackIconKind.GamepadSquareOutline, false));
            // Create Game!
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
