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
            if(maxPlayer != players.Count && players.Count > 0)
            {
                throw new ArgumentException($"The number of players must be {maxPlayer}.");
            }

            int roundCount = RoundCalculator.GetRoundCount(maxPlayer);

            RoundViewModel firstRound = new RoundViewModel();
            List<MatchViewModel> matches = new List<MatchViewModel>();
            int firsrtRoundMatchIndex = 0;
            for (int i = 0; i < maxPlayer; i+=2)
            {
                string name1 = string.Empty;
                string name2 = string.Empty;
                if(players.Count > 0)
                {
                    name1 = players[i].Name;
                    name2 = players[i + 1].Name;
                }

                matches.Add(new MatchViewModel
                {
                    Team1Name = name1,
                    Team2Name = name2,
                    RoundIndex = 0,
                    MatchIndex = firsrtRoundMatchIndex++
                });
            }
            firstRound.Matches = new System.Collections.ObjectModel.ObservableCollection<MatchViewModel>(matches);

            List<RoundViewModel> rounds = new List<RoundViewModel>();
            rounds.Add(firstRound);

            for (int i = 1; i < roundCount; i++)
            {
                int matchIndex = 0;
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
            var roundCollection = new System.Collections.ObjectModel.ObservableCollection<RoundViewModel>(rounds);
            gameTreeControl.DataContext = new GameTreeViewModel(roundCollection);
            return gameTreeControl;
        }
    }
}