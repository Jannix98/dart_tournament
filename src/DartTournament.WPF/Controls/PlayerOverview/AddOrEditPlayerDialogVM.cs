using CommunityToolkit.Mvvm.Input;
using DartTournament.WPF.Models;
using DartTournament.WPF.NotifyPropertyChange;
using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.Numerics;
using System.Windows.Input;

namespace DartTournament.WPF.Controls.PlayerOverview
{
    internal class AddOrEditPlayerDialogVM : NotifyPropertyChanged
    {
        private DartPlayerUI _player;
        private bool _isEditMode = false;

        public AddOrEditPlayerDialogVM()
        {
            ConfirmCommand = new RelayCommand(OnConfirm, CanConfirm);
            CancelCommand = new RelayCommand(OnCancel);

            // Listen to property changes to update command state
            //PropertyChanged += (s, e) =>
            //{
            //    if (e.PropertyName == nameof(Player))
            //    {
            //        ((RelayCommand)ConfirmCommand).NotifyCanExecuteChanged();
            //    }
            //};
            PropertyChanged += AddOrEditPlayerDialogVM_PropertyChanged;
        }

        private void AddOrEditPlayerDialogVM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(Player))
            {
                Player.PropertyChanged += (s, ev) =>
                {
                    if (ev.PropertyName == nameof(DartPlayerUI.Name))
                    {
                        ((RelayCommand)ConfirmCommand).NotifyCanExecuteChanged();
                    }
                };
            }
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
        public DartPlayerUI Player { get => _player; set => SetProperty(ref _player, value); }

        private void OnConfirm()
        {
            if (string.IsNullOrWhiteSpace(Player?.Name))
                return;

            // Create result object with player data
            var result = new AddOrEditPlayerDialogResult
            {
                DialogResult = true,
                Player = this.Player,
                IsEditMode = IsEditMode
            };

            // Close DialogHost with result using NestedDialogHost identifier
            DialogHost.Close("NestedDialogHost", result);
        }

        private bool CanConfirm()
        {
            return !string.IsNullOrWhiteSpace(Player?.Name);
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