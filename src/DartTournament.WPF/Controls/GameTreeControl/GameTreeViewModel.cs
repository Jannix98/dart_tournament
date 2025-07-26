using CommunityToolkit.Mvvm.Input;
using DartTournament.WPF.Dialogs.AddPlayer;
using DartTournament.WPF.Dialogs.Base;
using DartTournament.WPF.Dialogs.SelectWinner;
using DartTournament.WPF.NotifyPropertyChange;
using DartTournament.WPF.Utils.MatchHandler;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DartTournament.WPF.Controls.GameTreeControl
{
    public class GameTreeViewModel : NotifyPropertyChanged
    {
        IDialogManager _dialogManager;
        ObservableCollection<MatchViewModel> _allMatches = new ObservableCollection<MatchViewModel>(); // Initialize to avoid null
        private GameMatchHandler _matchHandler;

        public GameTreeViewModel(GameMatchHandler gameMatchHandler)
        {
            _matchHandler = gameMatchHandler;
            var matches = gameMatchHandler.Matches;
            AllMatches = new ObservableCollection<MatchViewModel>(matches);
            SelectWinnerCommand = new RelayCommand<MatchViewModel>((match) => SelectWinner(match));
            _dialogManager = ServiceManager.ServiceManager.Instance.GetRequiredService<IDialogManager>();
        }

        private void SelectWinner(MatchViewModel? match)
        {
            SelectWinnerInput input = new SelectWinnerInput(match);

            SelectWinnerResult? result = _dialogManager.ShowDialog<ISelectWinnerDialog>(input) as SelectWinnerResult;
            if (result?.DialogResult == false)
                return;

            _matchHandler.SetToNextMatch(match.RoundIndex, match.MatchIndex, result);
        }

        public ICommand SelectWinnerCommand { get; set; }
        public ObservableCollection<MatchViewModel> AllMatches { get => _allMatches; set => SetProperty(ref _allMatches, value); }
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