using CommunityToolkit.Mvvm.Input;
using DartTournament.Application.DTO.Match;
using DartTournament.Presentation.Base.Services;
using DartTournament.Presentation.Services;
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
        private IMatchPresentationService _matchPresentationService;

        public GameTreeViewModel(GameMatchHandler gameMatchHandler)
        {
            _matchHandler = gameMatchHandler;
            var matches = gameMatchHandler.Matches;
            AllMatches = new ObservableCollection<MatchViewModel>(matches);
            SelectWinnerCommand = new RelayCommand<MatchViewModel>((match) => SelectWinner(match));
            _dialogManager = SM.ServiceManager.Instance.GetRequiredService<IDialogManager>();
            _matchPresentationService = SM.ServiceManager.Instance.GetRequiredService<IMatchPresentationService>();
        }

        private async void SelectWinner(MatchViewModel? match)
        {
            SelectWinnerInput input = new SelectWinnerInput(match);

            SelectWinnerResult? result = _dialogManager.ShowDialog<ISelectWinnerDialog>(input) as SelectWinnerResult;
            if (result?.DialogResult == false)
                return;

            await _matchHandler.SetWinnerToNextMatch(match.RoundIndex, match.MatchIndex, result);
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
        
        private Guid _idGameEntityA;
        public Guid IdGameEntityA { get => _idGameEntityA; set => SetProperty(ref _idGameEntityA, value); }
        
        private Guid _idGameEntityB;
        public Guid IdGameEntityB { get => _idGameEntityB; set => SetProperty(ref _idGameEntityB, value); }
        
        private string _team1Name = string.Empty;
        public string Player1Name { get => _team1Name; set => SetProperty(ref _team1Name, value); }
        private string _team2Name = string.Empty;
        public string Player2Name { get => _team2Name; set => SetProperty(ref _team2Name, value); }
        public int RoundIndex { get; set; }
        public int MatchIndex { get; set; }

        private Guid _winnerId = Guid.Empty;
        public Guid WinnerId { get => _winnerId; set => SetProperty(ref _winnerId, value); }

        public MatchViewModel(Guid id, Guid idGameEntityA, Guid idGameEntityB, string player1Name, string player2Name, int roundIndex, int matchIndex, Guid winnerId)
        {
            Id = id;
            IdGameEntityA = idGameEntityA;
            IdGameEntityB = idGameEntityB;
            Player1Name = player1Name;
            Player2Name = player2Name;
            RoundIndex = roundIndex;
            MatchIndex = matchIndex;
            WinnerId = winnerId;
        }
    }
}