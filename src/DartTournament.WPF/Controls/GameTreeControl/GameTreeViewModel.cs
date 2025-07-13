using CommunityToolkit.Mvvm.Input;
using DartTournament.WPF.NotifyPropertyChange;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DartTournament.WPF.Controls.GameTreeControl
{
    public class GameTreeViewModel : NotifyPropertyChanged
    {
        public GameTreeViewModel()
        {
            SelectWinnerCommand = new RelayCommand<MatchViewModel>((match) => SelectWinner(match));
        }

        private void SelectWinner(MatchViewModel? match)
        {
            
        }

        public ObservableCollection<RoundViewModel> Rounds { get; set; } = new();

        public ObservableCollection<MatchViewModel> AllMatches
        {
            get
            {
                var all = new ObservableCollection<MatchViewModel>();
                for (int round = 0; round < Rounds.Count; round++)
                {
                    for (int match = 0; match < Rounds[round].Matches.Count; match++)
                    {
                        var m = Rounds[round].Matches[match];
                        all.Add(m);
                    }
                }
                return all;
            }
        }

        public ICommand SelectWinnerCommand { get; set; }
    }

    public class RoundViewModel
    {
        public ObservableCollection<MatchViewModel> Matches { get; set; } = new();
    }

    public class MatchViewModel
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Team1Name { get; set; }
        public string Team2Name { get; set; }
        public int RoundIndex { get; set; }
        public int MatchIndex { get; set; }
    }
}