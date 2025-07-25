using CommunityToolkit.Mvvm.Input;
using DartTournament.WPF.Dialogs.AddPlayer;
using DartTournament.WPF.Dialogs.Base;
using DartTournament.WPF.Dialogs.SelectWinner;
using DartTournament.WPF.NotifyPropertyChange;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DartTournament.WPF.Controls.GameTreeControl
{
    public class GameTreeViewModel : NotifyPropertyChanged
    {
        IDialogManager _dialogManager;
        public GameTreeViewModel(ObservableCollection<RoundViewModel> rounds)
        {
            Rounds = rounds;
            SelectWinnerCommand = new RelayCommand<MatchViewModel>((match) => SelectWinner(match));
            _dialogManager = ServiceManager.ServiceManager.Instance.GetRequiredService<IDialogManager>();
        }

        private void SelectWinner(MatchViewModel? match)
        {
            SelectWinnerInput input = new SelectWinnerInput(match);

            SelectWinnerResult? result = _dialogManager.ShowDialog<ISelectWinnerDialog>(input) as SelectWinnerResult;
            if (result?.DialogResult == false)
                return;

            // we have a winner!
            var nextMatch = FindNextMatch(match.RoundIndex, match.MatchIndex);
            if (match.MatchIndex % 2 == 0)
            {
                nextMatch.Team1Name = result.WinnerName;
            }
            else
            {
                nextMatch.Team2Name = result.WinnerName;
            }
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

        private MatchViewModel FindNextMatch(int currentRoundIndex, int currentMatchIndex)
        {
            int newRoundIndex = currentRoundIndex + 1;
            if (newRoundIndex >= Rounds.Count)
                return null;

            int nextMatchIndex = GetNextIndex(currentMatchIndex);
            var newMatch = AllMatches.Where(m => m.RoundIndex == newRoundIndex && m.MatchIndex == nextMatchIndex)
                .FirstOrDefault();

            if(newMatch == null)
                throw new InvalidOperationException($"No match found for Round {newRoundIndex}, Match {nextMatchIndex}.");

            return newMatch;

        }

        int GetNextIndex(int inputIndex)
        {
            return inputIndex / 2;
        }
    }

    public class RoundViewModel
    {
        public ObservableCollection<MatchViewModel> Matches { get; set; } = new();
    }

    public class MatchViewModel : NotifyPropertyChanged
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        private string _team1Name = string.Empty;
        public string Team1Name { get => _team1Name; set => SetProperty(ref _team1Name, value); }
        private string _team2Name = string.Empty;
        public string Team2Name { get => _team2Name; set => SetProperty(ref _team2Name, value); }
        public int RoundIndex { get; set; }
        public int MatchIndex { get; set; }
    }
}