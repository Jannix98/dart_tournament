using System.Windows.Controls;

namespace DartTournament.WPF.Controls.GameTreeControl
{
    public partial class GameTreeControl : UserControl
    {
        public GameTreeControl()
        {
            InitializeComponent();
            // For demo: set DataContext here or bind from outside
            this.DataContext = new GameTreeViewModel
            {
                Rounds = new System.Collections.ObjectModel.ObservableCollection<RoundViewModel>() 
                {
                    new RoundViewModel
                    {
                        Matches = new System.Collections.ObjectModel.ObservableCollection<MatchViewModel>
                        {
                            new MatchViewModel { Team1Name = "Team A", Team2Name = "Team B", Team1Score = 10, Team2Score = 8 },
                            new MatchViewModel { Team1Name = "Team C", Team2Name = "Team D", Team1Score = 12, Team2Score = 15 },
                            new MatchViewModel { Team1Name = "Team E", Team2Name = "Team F", Team1Score = 10, Team2Score = 8 },
                            new MatchViewModel { Team1Name = "Team G", Team2Name = "Team H", Team1Score = 12, Team2Score = 15 }
                        }
                    },
                    new RoundViewModel
                    {
                        Matches = new System.Collections.ObjectModel.ObservableCollection<MatchViewModel>
                        {
                            new MatchViewModel { Team1Name = "Team A", Team2Name = "Team B", Team1Score = 10, Team2Score = 8 },
                            new MatchViewModel { Team1Name = "Team C", Team2Name = "Team D", Team1Score = 12, Team2Score = 15 }
                        }
                    },
                    new RoundViewModel
                    {
                        Matches = new System.Collections.ObjectModel.ObservableCollection<MatchViewModel>
                        {
                            new MatchViewModel { Team1Name = "Team E", Team2Name = "Team F", Team1Score = 9, Team2Score = 11 }
                        }
                    }
                }
            };
        }
    }
}