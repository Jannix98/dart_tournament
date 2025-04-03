using DartTournament.WPF.Navigator;
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
using DartTournament.WPF.Dialogs.CreateGame;
using DartTournament.WPF.Dialogs.DialogManagement;

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
            _navigator.NavigateTo(new TournamentTreeControl());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IDialogOwner owner = new DialogOwner(); // TODO: implement interface of CreateGameView
            CreateGameView createGameView = new CreateGameView(owner);
            var result = createGameView.ShowDialog();

            if (result.DialogResult == false)
                return;

            // Create Game!
        }
    }
}
