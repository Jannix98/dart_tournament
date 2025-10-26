using CommunityToolkit.Mvvm.Input;
using DartTournament.WPF.Models;
using DartTournament.WPF.NotifyPropertyChange;
using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.Windows.Input;

namespace DartTournament.WPF.Controls.PlayerOverview
{
    internal class AddOrEditPlayerDialogVM : NotifyPropertyChanged
    {
        private string _playerName = string.Empty;
        private bool _isEditMode = false;

        public AddOrEditPlayerDialogVM()
        {
            ConfirmCommand = new RelayCommand(OnConfirm, CanConfirm);
            CancelCommand = new RelayCommand(OnCancel);
            
            // Listen to property changes to update command state
            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(PlayerName))
                {
                    ((RelayCommand)ConfirmCommand).NotifyCanExecuteChanged();
                }
            };
        }

        public string PlayerName 
        { 
            get => _playerName; 
            set => SetProperty(ref _playerName, value); 
        }

        public bool IsEditMode 
        { 
            get => _isEditMode; 
            set 
            {
                SetProperty(ref _isEditMode, value);
                OnPropertyChanged(nameof(DialogTitle));
            }
        }

        public string DialogTitle => IsEditMode ? "Spieler bearbeiten" : "Neuer Spieler";

        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }

        private void OnConfirm()
        {
            if (string.IsNullOrWhiteSpace(PlayerName))
                return;

            // Create result object with player data
            var result = new AddOrEditPlayerDialogResult
            {
                DialogResult = true,
                Player = new DartPlayerUI(PlayerName),
                IsEditMode = IsEditMode
            };

            // Close DialogHost with result using NestedDialogHost identifier
            DialogHost.Close("NestedDialogHost", result);
        }

        private bool CanConfirm()
        {
            return !string.IsNullOrWhiteSpace(PlayerName);
        }

        private void OnCancel()
        {
            // Close DialogHost with null result using NestedDialogHost identifier
            DialogHost.Close("NestedDialogHost", null);
        }
    }

    // Result class for DialogHost
    public class AddOrEditPlayerDialogResult
    {
        public bool DialogResult { get; set; }
        public DartPlayerUI Player { get; set; }
        public bool IsEditMode { get; set; }
    }
}