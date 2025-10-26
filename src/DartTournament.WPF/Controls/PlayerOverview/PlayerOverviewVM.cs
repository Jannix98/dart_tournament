using DartTournament.Application.UseCases.Player.Services.Interfaces;
using DartTournament.WPF.Models;
using DartTournament.WPF.NotifyPropertyChange;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using DartTournament.Presentation.Base.Services;
using DartTournament.Application.DTO.Player;
using MaterialDesignThemes.Wpf;

namespace DartTournament.WPF.Controls.PlayerOverview
{
    internal class PlayerOverviewVM : NotifyPropertyChanged
    {
        IPlayerPresentationService _playerService;
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
            AddPlayerCommand = new RelayCommand(async () => await AddPlayerAsync());
            EditPlayerCommand = new RelayCommand<DartPlayerUI>(async player => await EditPlayerAsync(player));
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

        private async Task EditPlayerAsync(DartPlayerUI player)
        {
            if (player == null) return;

            // Create edit dialog with existing player name
            var editDialog = new AddOrEditPlayerDialog(player.Name);
            
            // Show the dialog using a nested DialogHost identifier
            var result = await DialogHost.Show(editDialog, "NestedDialogHost");
            
            if (result is AddOrEditPlayerDialogResult dialogResult && 
                dialogResult.DialogResult && 
                dialogResult.Player != null)
            {
                // Update the player's name
                player.Name = dialogResult.Player.Name;
                
                // Update in backend
                var updateDto = new DartPlayerUpdateDto
                {
                    Id = player.Id,
                    Name = player.Name
                };

                try
                {
                    await _playerService.UpdatePlayerAsync(updateDto);
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., logging, displaying a message)
                }
            }
        }

        private async Task AddPlayerAsync()
        {
            // Create add dialog
            var addDialog = new AddOrEditPlayerDialog();
            
            // Show the dialog using a nested DialogHost identifier
            var result = await DialogHost.Show(addDialog, "NestedDialogHost");
            
            if (result is AddOrEditPlayerDialogResult dialogResult && 
                dialogResult.DialogResult && 
                dialogResult.Player != null)
            {
                var insertDto = new DartPlayerInsertDto
                {
                    Name = dialogResult.Player.Name
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