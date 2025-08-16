using DartTournament.WPF.Models;
using DartTournament.WPF.Utils.GameData;
using DartTournament.WPF.Utils.MatchCreator;
using DartTournament.WPF.Utils.MatchGenerator;
using DartTournament.WPF.Utils.MatchHandler;
using DartTournament.WPF.Utils.RoundCalculator;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly GameSessionVM _viewModel;
        private readonly List<DartPlayerUI> _players;

        public GameSessionControl(string title, bool showLooserRound, List<DartPlayerUI> players)
        {
            InitializeComponent();
            
            _viewModel = new GameSessionVM(title, showLooserRound);
            _players = players;
            
            DataContext = _viewModel;
            Loaded += GameSessionControl_Loaded;
        }

        private void GameSessionControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(_viewModel.ShowLooserRound)
            {
                // Create looser game content
                GameDataCreator looserGameCreator = new GameDataCreator(new LooserRoundCalculator());
                LooserMatchCreator looserMatchCreator = new LooserMatchCreator(looserGameCreator);
                var looserGameData = looserMatchCreator.Create(_players.Count, _players);

                var matches = MatchGenerator.FromRounds(looserGameData.GameRounds);
                var looserMatches = MatchGenerator.FromRounds(looserGameData.LooserRounds);

                var looserMatchHandler = new LooserGameMatchHandler(looserMatches);
                var gameControlMatchHandler = new GameMatchHandler(matches, looserMatchHandler);
                var looserGameControlMatchHandler = new GameMatchHandler(looserMatches, null);

                var gameControl = GameTreeControl.GameTreeControl.CreateGame(gameControlMatchHandler);
                var looserControl = GameTreeControl.GameTreeControl.CreateGame(looserGameControlMatchHandler);
                
                _viewModel.SetGameContent(gameControl);
                _viewModel.SetLooserRoundContent(looserControl);
            }
            else
            {
                // Create normal game content
                GameDataCreator normalGameCreator = new GameDataCreator(new NormalRoundCalculator());
                NormalMatchCreator normalMatchCreator = new NormalMatchCreator(normalGameCreator);
                var normalGameData = normalMatchCreator.Create(_players.Count, _players);

                var matches = MatchGenerator.FromRounds(normalGameData.GameRounds);
                var gameControlMatchHandler = new GameMatchHandler(matches, null);
                var control = GameTreeControl.GameTreeControl.CreateGame(gameControlMatchHandler);

                _viewModel.SetGameContent(control);
            }
        }
    }
}
