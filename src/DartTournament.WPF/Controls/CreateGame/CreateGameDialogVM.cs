using CommunityToolkit.Mvvm.Input;
using DartTournament.Presentation.Base.Services;
using DartTournament.WPF.Models;
using DartTournament.WPF.Models.Enums;
using DartTournament.WPF.NotifyPropertyChange;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DartTournament.WPF.Controls.CreateGame
{
    internal class CreateGameDialogVM : NotifyPropertyChanged
    {
        IPlayerPresentationService _playerService;
        private string _tournamentName = string.Empty;
        private bool _advancedMode;
        private TournamentPlayerCount _selectedTotalPlayers;
        private ObservableCollection<SelectableDartPlayerUI> _players;
        private int _maxPlayers = 8;
        private bool _isLoading = true;

        public CreateGameDialogVM()
        {
            _playerService = SM.ServiceManager.Instance.GetRequiredService<IPlayerPresentationService>();
            TotalPlayersOptions = new ObservableCollection<TournamentPlayerCount>
            {
                TournamentPlayerCount.Four,
                TournamentPlayerCount.Eight,
                TournamentPlayerCount.Sixteen,
                TournamentPlayerCount.ThirtyTwo
            };

            CreateSessionCommand = new RelayCommand(CreateSession);
            CancelCommand = new RelayCommand(Cancel);

            SelectedTotalPlayers = TournamentPlayerCount.Eight; // Default selection
        }

        private void Player_PropertyChanged(object? sender, PropertyChangedEventArgs e)
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
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }

        private void CreateSession()
        {
            bool flowControl = ValidateProperties();
            if (!flowControl)
            {
                return;
            }

            // Create result object with selected data
            var result = new CreateGameDialogResult
            {
                DialogResult = true,
                TournamentName = TournamentName,
                AdvancedMode = AdvancedMode,
                SelectedPlayers = GetSelectedTeams()
            };

            // Close DialogHost with result
            DialogHost.Close("RootDialogHost", result);
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
            // Close DialogHost with null result
            DialogHost.Close("RootDialogHost", null);
        }

        internal List<DartPlayerUI> GetSelectedTeams()
        {
            return Players
                .Where(p => p.IsSelected)
                .Select(p => p.Player)
                .ToList();
        }

        public async Task LoadPlayersAsync()
        {
            IsLoading = true;
            try
            {
                var data = await _playerService.GetPlayerAsync();
                var loadedPlayers = data.Select(dto => new DartPlayerUI(dto.Id, dto.Name)).ToList();
                Players = new ObservableCollection<SelectableDartPlayerUI>(
                    loadedPlayers.Select(p => new SelectableDartPlayerUI(p))
                );

                foreach (var p in Players)
                {
                    p.PropertyChanged += Player_PropertyChanged;
                }
            }
            catch (Exception ex)
            {
                // TODO: Handle exception properly
                System.Diagnostics.Debug.WriteLine($"Error loading players: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }

    // Result class for DialogHost
    public class CreateGameDialogResult
    {
        public bool DialogResult { get; set; }
        public string TournamentName { get; set; } = string.Empty;
        public bool AdvancedMode { get; set; }
        public List<DartPlayerUI> SelectedPlayers { get; set; } = new List<DartPlayerUI>();
    }
}