using DartTournament.WPF.Models;
using DartTournament.WPF.Utils.GameData;
using DartTournament.WPF.Utils.MatchCreator;
using DartTournament.WPF.Utils.MatchGenerator;
using DartTournament.WPF.Utils.MatchHandler;
using DartTournament.Helper.RoundCalculator;
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
using DartTournament.Application.DTO.Game;

namespace DartTournament.WPF.Controls.GameSession
{
    /// <summary>
    /// Interaction logic for GameSessionControl.xaml
    /// </summary>
    public partial class GameSessionControl : UserControl
    {
        private readonly GameSessionVM _viewModel;
        private readonly List<DartPlayerUI> _players;
        private readonly GameResult _gameResult;

        public GameSessionControl(GameResult gameResult)
        {
            _gameResult = gameResult;
            InitializeComponent();

            _viewModel = new GameSessionVM(gameResult.Name, gameResult.LooserGame != null);
            _players = null;

            DataContext = _viewModel;
            Loaded += GameSessionControl_Loaded_new;
        }

        private void GameSessionControl_Loaded_new(object sender, RoutedEventArgs e)
        {
            if (_viewModel.ShowLooserRound)
            {
                // Create game content from loaded GameResult with looser round
                var mainGameRounds = MatchGenerator.FromGameDTO(_gameResult.MainGame);
                var looserGameRounds = MatchGenerator.FromGameDTO(_gameResult.LooserGame);
                
                var matches = MatchGenerator.FromRounds(mainGameRounds);
                var looserMatches = MatchGenerator.FromRounds(looserGameRounds);

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
                // Create normal game content from loaded GameResult
                var mainGameRounds = MatchGenerator.FromGameDTO(_gameResult.MainGame);
                var matches = MatchGenerator.FromRounds(mainGameRounds);
                
                var gameControlMatchHandler = new GameMatchHandler(matches, null);
                var control = GameTreeControl.GameTreeControl.CreateGame(gameControlMatchHandler);

                _viewModel.SetGameContent(control);
            }
        }
    }
}
