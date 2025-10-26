using DartTournament.Application.UseCases.Player.Services.Interfaces;
using DartTournament.WPF.Models; // Updated namespace import
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
using DartTournament.WPF.Dialogs.Base;
using DartTournament.WPF.Dialogs.AddPlayer;
using DartTournament.Presentation.Base.Services;
using DartTournament.Application.DTO.Player;
using System.Text.Json.Serialization;

namespace DartTournament.WPF.Controls.PlayerOverview
{
    internal class PlayerOverviewVM : NotifyPropertyChanged
    {
        IPlayerPresentationService _playerService;
        IDialogManager _dialogManager;
        DartPlayerUI _selectedPlayer;
        ObservableCollection<DartPlayerUI> _playerCollection = new ObservableCollection<DartPlayerUI>();
        private bool _isLoading = true;
        ICommand _addPlayerCommand;
        private ICommand _editPlayerCommand;
        private ICommand _savePlayerCommand;
        private ICommand _deletePlayerCommand;
        private bool _editIsEnabled;

        public PlayerOverviewVM()
        {
            _playerService = SM.ServiceManager.Instance.GetRequiredService<IPlayerPresentationService>();
            _dialogManager = SM.ServiceManager.Instance.GetRequiredService<IDialogManager>();
            AddPlayerCommand = new RelayCommand(() => AddPlayer());
            EditPlayerCommand = new RelayCommand<DartPlayerUI>(player => EditPlayer(player));
            SavePlayerCommand = new RelayCommand(() => SavePlayer());
            DeletePlayerCommand = new RelayCommand<DartPlayerUI>(async player => await DeletePlayerAsync(player));
            EditIsEnabled = false;
        }

        private async void SavePlayer()
        {
            if (SelectedPlayer == null)
                return;

            var updateDto = new DartPlayerUpdateDto
            {
                Id = SelectedPlayer.Id,
                Name = SelectedPlayer.Name
            };

            try
            {
                await _playerService.UpdatePlayerAsync(updateDto);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., logging, displaying a message)
            }
            finally
            {
                EditIsEnabled = false;
            }
        }

        private void EditPlayer(DartPlayerUI player)
        {
            if (player != null)
            {
                SelectedPlayer = player;
            }
            EditIsEnabled = true;
        }

        private async void AddPlayer()
        {
            AddPlayerResult? result = _dialogManager.ShowDialog<IAddPlayerView>() as AddPlayerResult;
            if (result?.DialogResult != true || result.Player == null)
                return;

            var insertDto = new DartPlayerInsertDto
            {
                Name = result.Player.Name
            };

            try
            {
                var playerDto = await _playerService.CreatePlayerAsync(insertDto);
                var playerUI = new DartPlayerUI(playerDto.Id, playerDto.Name);
                PlayerCollection.Add(playerUI);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., logging, displaying a message)
            }
        }

        private async Task DeletePlayerAsync(DartPlayerUI player)
        {
            try
            {
                Guid id = player.Id;
                await _playerService.DeletePlayerAsync(id);
                var playerToRemove = PlayerCollection.FirstOrDefault(p => p.Id == id);
                if (playerToRemove != null)
                    PlayerCollection.Remove(playerToRemove);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., logging, displaying a message)
            }
        }

        public DartPlayerUI SelectedPlayer { get => _selectedPlayer; set => SetProperty(ref _selectedPlayer, value); }
        public ObservableCollection<DartPlayerUI> PlayerCollection { get => _playerCollection; set => SetProperty(ref _playerCollection, value); }
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }
        public ICommand AddPlayerCommand { get => _addPlayerCommand; set => _addPlayerCommand = value; }
        public ICommand EditPlayerCommand { get => _editPlayerCommand; set => _editPlayerCommand = value; }
        public ICommand SavePlayerCommand { get => _savePlayerCommand; set => _savePlayerCommand = value; }
        public ICommand DeletePlayerCommand { get => _deletePlayerCommand; set => _deletePlayerCommand = value; }
        public bool EditIsEnabled { get => _editIsEnabled; set => SetProperty(ref _editIsEnabled, value); }

        public async Task LoadPlayersAsync()
        {
            try
            {
                IsLoading = true;
                var data = await _playerService.GetPlayerAsync();
                var players = data.Select(dto => new DartPlayerUI(dto.Id, dto.Name)).ToList();
                PlayerCollection = new ObservableCollection<DartPlayerUI>(players);
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