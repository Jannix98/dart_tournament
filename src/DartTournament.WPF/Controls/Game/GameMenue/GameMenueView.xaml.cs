using DartTournament.WPF.Navigator;
using System.Windows;
using System.Windows.Controls;

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
            
        }


    }
}
