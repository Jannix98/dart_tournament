using DartTournament.WPF.Controls.TournamentTree;
using DartTournament.WPF.Models;
using System.Windows.Controls;

namespace DartTournament.WPF.Controls.GameTreeControl
{
    public partial class GameTreeControl : UserControl
    {
        public GameTreeControl()
        {
            InitializeComponent();
        }

        internal static GameTreeControl CreateGame(int maxPlayer, List<DartPlayerUI> players)
        {
            if(maxPlayer != players.Count)
            {
                throw new ArgumentException($"The number of players must be {maxPlayer}.");
            }

            int roundCount = RoundCalculator.GetRoundCount(maxPlayer);

            RoundViewModel firstRound = new RoundViewModel();
            List<MatchViewModel> matches = new List<MatchViewModel>();
            int firsrtRoundMatchIndex = 0;
            for (int i = 0; i < players.Count; i+=2)
            {
                matches.Add(new MatchViewModel
                {
                    Team1Name = players[i].Name,
                    Team2Name = players[i + 1].Name,
                    RoundIndex = 0,
                    MatchIndex = firsrtRoundMatchIndex++
                });
            }
            firstRound.Matches = new System.Collections.ObjectModel.ObservableCollection<MatchViewModel>(matches);

            List<RoundViewModel> rounds = new List<RoundViewModel>();
            rounds.Add(firstRound);

            int matchIndex = 0;
            for (int i = 1; i < roundCount; i++)
            {
                RoundViewModel round = new RoundViewModel();
                for (int j = 0; j < matches.Count / (i*2); j++)
                {
                    round.Matches.Add(new MatchViewModel() 
                    { 
                        RoundIndex = i,
                        MatchIndex = matchIndex++,
                    });
                }
                rounds.Add(round);
            }

            GameTreeControl gameTreeControl = new GameTreeControl();
            gameTreeControl.DataContext = new GameTreeViewModel
            {
                Rounds = new System.Collections.ObjectModel.ObservableCollection<RoundViewModel>(rounds)
            };
            return gameTreeControl;
        }
    }
}