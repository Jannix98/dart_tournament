using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using DartTournament.WPF.Models;
using DartTournament.WPF.NotifyPropertyChange;

namespace DartTournament.WPF.Dialogs.CreateGame;

internal class CreateTournamentSessionViewModel : NotifyPropertyChanged
{
    
        private string _tournamentName = string.Empty;
        private bool _advancedMode;
        private TournamentPlayerCount _selectedTotalPlayers;
        private ObservableCollection<SelectableDartPlayerUI> _players;
        private int _maxPlayers = 8;

        public CreateTournamentSessionViewModel()
        {
            TotalPlayersOptions = new ObservableCollection<TournamentPlayerCount>
            {
                TournamentPlayerCount.Four,
                TournamentPlayerCount.Eight,
                TournamentPlayerCount.Sixteen,
                TournamentPlayerCount.ThirtyTwo
            };

            Players = new ObservableCollection<SelectableDartPlayerUI>
            {
                new SelectableDartPlayerUI(new DartPlayerUI("Player 1")),
                new SelectableDartPlayerUI(new DartPlayerUI("Player 2")),
                new SelectableDartPlayerUI(new DartPlayerUI("Player 3")),
                new SelectableDartPlayerUI (new DartPlayerUI("Player 4")),
                new SelectableDartPlayerUI (new DartPlayerUI("Player 5")),
            };

        foreach (var p in Players)
        {
            p.PropertyChanged += Player_PropertyChanged;
        }

            CreateSessionCommand = new RelayCommand(CreateSession);

            SelectedTotalPlayers = TournamentPlayerCount.Eight; // Default selection
    }

    private void Player_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SelectableDartPlayerUI.IsSelected))
        {
            OnPropertyChanged(nameof(SelectedPlayersCount));
        }
    }

    public string TournamentName
        {
            get => _tournamentName;
            set => SetProperty(ref _tournamentName, value);
        }

        public bool AdvancedMode
        {
            get => _advancedMode;
            set => SetProperty(ref _advancedMode, value);
        }

        public TournamentPlayerCount SelectedTotalPlayers
        {
            get => _selectedTotalPlayers;
            set => SetProperty(ref _selectedTotalPlayers, value);
        }

        public ObservableCollection<TournamentPlayerCount> TotalPlayersOptions { get; }

        public ObservableCollection<SelectableDartPlayerUI> Players
        {
            get => _players;
            set => SetProperty(ref _players, value);
        }

        public int SelectedPlayersCount => Players?.Count(p => p.IsSelected) ?? 0;

        public int MaxPlayers
        {
            get => _maxPlayers;
            set => SetProperty(ref _maxPlayers, value);
        }

        public ICommand CreateSessionCommand { get; }

        private void CreateSession()
        {
            // Implement session creation logic here
        }
    }

internal enum TournamentPlayerCount
{
    Four = 4,
    Eight = 8,
    Sixteen = 16,
    ThirtyTwo = 32,
}


