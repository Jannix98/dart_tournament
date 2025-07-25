using DartTournament.WPF.Models;
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

namespace DartTournament.WPF.Controls.GameSession
{
    /// <summary>
    /// Interaction logic for GameSessionControl.xaml
    /// </summary>
    public partial class GameSessionControl : UserControl
    {
        private string _title;
        private bool _showLooserRound;
        List<DartPlayerUI> _players;

        public GameSessionControl(string title, bool showLooserRound, List<DartPlayerUI> players)
        {
            InitializeComponent();
            _title = title;
            _showLooserRound = showLooserRound;
            TitleTextBlock.Text = _title;
            
            
            _title = title;
            _players = players;
            Loaded += GameSessionControl_Loaded;
        }

        private void GameSessionControl_Loaded(object sender, RoutedEventArgs e)
        {
            var control = GameTreeControl.GameTreeControl.CreateGame(_players.Count, _players);
            if(_showLooserRound)
            {
                // TODO: move those tabs into a separate control
                this.TabControl.Visibility = Visibility.Visible;
                GameTab.Content = control;
                // add loser control
                int looserCount = _players.Count / 2;
                List<DartPlayerUI> looserPlayers = new List<DartPlayerUI>();
                var looserControl = GameTreeControl.GameTreeControl.CreateGame(looserCount, new List<DartPlayerUI>());
                LooserRoundTab.Content = looserControl;
            }
            else
            {
                this.TabControl.Visibility = Visibility.Collapsed;
                GameContent.Content = control;
            }
        }
    }
}
