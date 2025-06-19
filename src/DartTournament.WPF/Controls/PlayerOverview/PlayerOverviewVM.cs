using DartTournament.Application.UseCases.Player.Services.Interfaces;
using DartTournament.Domain.Entities;
using DartTournament.WPF.NotifyPropertyChange;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DartTournament.WPF.Dialogs.DialogManagement;
using DartTournament.WPF.Dialogs.AddPlayer;

namespace DartTournament.WPF.Controls.PlayerOverview
{
    internal class PlayerOverviewVM : NotifyPropertyChanged
    {
        IPlayerService _playerService;
        IDialogManager _dialogManager;
        DartPlayer _selectedPlayer;
        ObservableCollection<DartPlayer> _playerCollection = new ObservableCollection<DartPlayer>();
        private bool _isLoading = true;
        ICommand _addPlayerCommand;
        private ICommand _editPlayerCommand;
        private ICommand _savePlayerCommand;
        private bool _editIsEnabled;

        public PlayerOverviewVM()
        {
            _playerService = ServiceManager.ServiceManager.Instance.GetRequiredService<IPlayerService>();
            _dialogManager = ServiceManager.ServiceManager.Instance.GetRequiredService<IDialogManager>();
            AddPlayerCommand = new RelayCommand(() => AddPlayer());
            EditPlayerCommand = new RelayCommand(() => EditPlayer());
            SavePlayerCommand = new RelayCommand(() => SavePlayer());
            EditIsEnabled = false;
        }

        private void SavePlayer()
        {
            // TODO: fix when DTO is implemented
            _playerService.UpdatePlayerAsync(SelectedPlayer);
            EditIsEnabled = false;
        }

        private void EditPlayer()
        {
            EditIsEnabled = true;
        }

        private async void AddPlayer()
        {
            AddPlayerResult? result = _dialogManager.ShowDialog<IAddPlayerView>() as AddPlayerResult;
            if (result?.DialogResult != true)
                return;
            var player = await _playerService.CreatePlayerAsync(result.Team.Name);
            PlayerCollection.Add(player);
        }

        public DartPlayer SelectedPlayer { get => _selectedPlayer; set => SetProperty(ref _selectedPlayer, value); }
        public ObservableCollection<DartPlayer> PlayerCollection { get => _playerCollection; set => SetProperty(ref _playerCollection, value); }
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }
        public ICommand AddPlayerCommand { get => _addPlayerCommand; set => _addPlayerCommand = value; }
        public ICommand EditPlayerCommand { get => _editPlayerCommand; set => _editPlayerCommand = value; }
        public ICommand SavePlayerCommand { get => _savePlayerCommand; set => _savePlayerCommand = value; }
        public bool EditIsEnabled { get => _editIsEnabled; set => SetProperty(ref _editIsEnabled, value); }

        public async Task LoadTeamsAsync()
        {
            try
            {
                // TODO this async shit is not working. Fix it
                IsLoading = true;
                var data = await _playerService.GetPlayerAsync();
                PlayerCollection = new ObservableCollection<DartPlayer>(data);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., logging, displaying a message)
            }
            finally
            {
                IsLoading = false;
            }
        }

    }
}
