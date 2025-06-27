using CommunityToolkit.Mvvm.Input;
using DartTournament.WPF.Dialogs.Base;
using DartTournament.WPF.Models;
using DartTournament.WPF.Models.Enums;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace DartTournament.WPF.Dialogs.CreateGame
{
    internal class CreateGameVM : BaseDialogVM
    {
        private string _tournamentName = string.Empty;
        private bool _advancedMode;
        private TournamentPlayerCount _selectedTotalPlayers;
        private ObservableCollection<SelectableDartPlayerUI> _players;
        private int _maxPlayers = 8;

        public CreateGameVM()
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
                new SelectableDartPlayerUI (new DartPlayerUI("Player 5")),
                new SelectableDartPlayerUI (new DartPlayerUI("Player 5")),
                new SelectableDartPlayerUI (new DartPlayerUI("Player 5")),
                new SelectableDartPlayerUI (new DartPlayerUI("Player 5")),
                new SelectableDartPlayerUI (new DartPlayerUI("Player 5")),
                new SelectableDartPlayerUI (new DartPlayerUI("Player 5")),
                new SelectableDartPlayerUI (new DartPlayerUI("Player 5")),
                new SelectableDartPlayerUI (new DartPlayerUI("Player 5")),
                new SelectableDartPlayerUI (new DartPlayerUI("Player 5")),
                new SelectableDartPlayerUI (new DartPlayerUI("Player 5")),
                new SelectableDartPlayerUI (new DartPlayerUI("Player 5")),
                new SelectableDartPlayerUI (new DartPlayerUI("Player 5")),
                new SelectableDartPlayerUI (new DartPlayerUI("Player 5")),
                new SelectableDartPlayerUI (new DartPlayerUI("Player 5")),
                new SelectableDartPlayerUI (new DartPlayerUI("Player 5")),
                new SelectableDartPlayerUI (new DartPlayerUI("Player 5")),
            };

            foreach (var p in Players)
            {
                p.PropertyChanged += Player_PropertyChanged;
            }

            CreateSessionCommand = new RelayCommand(CreateSession);
            CancelCommand = new RelayCommand(Cancel);

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
        public ICommand CancelCommand { get; }

        private void CreateSession()
        {
            bool flowControl = ValidateProperties();
            if (!flowControl)
            {
                return;
            }
            Dialog?.CloseWindow(true);
        }

        private bool ValidateProperties()
        {
            // TODO: Implement validation logic with xaml error handling and snackbars instead of message boxes
            // TODO: remove mvvm validation and use xaml error handling with snackbars
            int maxPlayers = (int)SelectedTotalPlayers;
            if (SelectedPlayersCount < maxPlayers)
            {
                MessageBox.Show($"Please select at least {maxPlayers} players.", "Insufficient Players", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(TournamentName))
            {
                MessageBox.Show("Please enter a tournament name.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (Players.Count(p => p.IsSelected) > maxPlayers)
            {
                MessageBox.Show($"You can only select up to {maxPlayers} players.", "Too Many Players", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void Cancel()
        {
            Dialog?.CloseWindow(false);
        }

        internal List<DartPlayerUI> GetSelectedTeams()
        {
            return Players
                .Where(p => p.IsSelected)
                .Select(p => p.Player)
                .ToList();
        }
    }
}
