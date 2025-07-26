using DartTournament.WPF.Models;
using DartTournament.WPF.Utils.GameData;
using DartTournament.WPF.Utils.MatchCreator;
using DartTournament.WPF.Utils.MatchGenerator;
using DartTournament.WPF.Utils.MatchHandler;
using DartTournament.WPF.Utils.RoundCalculator;
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
            
            if(_showLooserRound)
            {
                // TODO: move those tabs into a separate control
                // add loser control
                
                GameDataCreator looserGameCreator = new GameDataCreator(new LooserRoundCalculator());
                LooserMatchCreator looserMatchCreator = new LooserMatchCreator(looserGameCreator);
                var looserGameData = looserMatchCreator.Create(_players.Count, _players);

                this.TabControl.Visibility = Visibility.Visible;

                var matches = MatchGenerator.FromRounds(looserGameData.GameRounds);
                var looserMatches = MatchGenerator.FromRounds(looserGameData.LooserRounds);

                var looserMatchHandler = new LooserGameMatchHandler(looserMatches);
                var gameControlMatchHandler = new GameMatchHandler(matches, looserMatchHandler);
                var looserGameControlMatchHandler = new GameMatchHandler(looserMatches, null);

                var control = GameTreeControl.GameTreeControl.CreateGame(gameControlMatchHandler);
                var looserControl = GameTreeControl.GameTreeControl.CreateGame(looserGameControlMatchHandler);
                GameTab.Content = control;
                LooserRoundTab.Content = looserControl;
            }
            else
            {
                GameDataCreator normalGameCreator = new GameDataCreator(new NormalRoundCalculator());
                NormalMatchCreator normalMatchCreator = new NormalMatchCreator(normalGameCreator);
                var normalGameData = normalMatchCreator.Create(_players.Count, _players);

                var matches = MatchGenerator.FromRounds(normalGameData.GameRounds);

                var gameControlMatchHandler = new GameMatchHandler(matches, null);

                var control = GameTreeControl.GameTreeControl.CreateGame(gameControlMatchHandler);

                this.TabControl.Visibility = Visibility.Collapsed;
                GameContent.Content = control;
            }
        }
    }
}
