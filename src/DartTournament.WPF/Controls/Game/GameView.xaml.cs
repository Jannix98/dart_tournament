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
using DartTournament.WPF.Controls.Game.GameMenue;
using DartTournament.WPF.Controls.TeamOverview;
using DartTournament.WPF.Navigator;

namespace DartTournament.WPF.Controls.Game
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        private bool hasBeenLoaded = false;

        public GameView()
        {
            InitializeComponent();
            this.Loaded += GameView_Loaded;
        }

        private void GameView_Loaded(object sender, RoutedEventArgs e)
        {
            if (hasBeenLoaded)
                return;

            hasBeenLoaded = true;
            IScreenNavigator navigator = new ScreenNavigator();
            navigator.Initialize(GameFrame);
            GameFrame.Navigate(new GameMenueView(navigator));
        }
    }
}
