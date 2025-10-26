using CommunityToolkit.Mvvm.Input;
using DartTournament.WPF.Controls.GameTreeControl;
using DartTournament.WPF.NotifyPropertyChange;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows.Input;

namespace DartTournament.WPF.Controls.Game
{
    internal class SelectWinnerDialogVM : NotifyPropertyChange.NotifyPropertyChanged
    {
        private MatchViewModel _match;
        private string _player1Name = string.Empty;
        private string _player2Name = string.Empty;

        public SelectWinnerDialogVM()
        {
            SelectPlayer1Command = new RelayCommand(SelectPlayer1, () => _match != null);
            SelectPlayer2Command = new RelayCommand(SelectPlayer2, () => _match != null);
            CancelCommand = new RelayCommand(Cancel);
        }

        public string Player1Name 
        { 
            get => _player1Name; 
            set => SetProperty(ref _player1Name, value); 
        }

        public string Player2Name 
        { 
            get => _player2Name; 
            set => SetProperty(ref _player2Name, value); 
        }

        public ICommand SelectPlayer1Command { get; }
        public ICommand SelectPlayer2Command { get; }
        public ICommand CancelCommand { get; }

        public void InitializeMatch(MatchViewModel match)
        {
            _match = match ?? throw new ArgumentNullException(nameof(match));
            Player1Name = _match.Player1Name;
            Player2Name = _match.Player2Name;
            
            // Update command states
            ((RelayCommand)SelectPlayer1Command).NotifyCanExecuteChanged();
            ((RelayCommand)SelectPlayer2Command).NotifyCanExecuteChanged();
        }

        private void SelectPlayer1()
        {
            if (_match == null) return;

            var result = new SelectWinnerDialogResult
            {
                DialogResult = true,
                WinnerId = _match.IdGameEntityA,
                WinnerName = _match.Player1Name,
                LooserId = _match.IdGameEntityB,
                LooserName = _match.Player2Name
            };

            DialogHost.Close("RootDialogHost", result);
        }

        private void SelectPlayer2()
        {
            if (_match == null) return;

            var result = new SelectWinnerDialogResult
            {
                DialogResult = true,
                WinnerId = _match.IdGameEntityB,
                WinnerName = _match.Player2Name,
                LooserId = _match.IdGameEntityA,
                LooserName = _match.Player1Name
            };

            DialogHost.Close("RootDialogHost", result);
        }

        private void Cancel()
        {
            DialogHost.Close("RootDialogHost", null);
        }
    }

    // Result class for DialogHost
    public class SelectWinnerDialogResult
    {
        public bool DialogResult { get; set; }
        public Guid WinnerId { get; set; }
        public string WinnerName { get; set; } = string.Empty;
        public Guid LooserId { get; set; }
        public string LooserName { get; set; } = string.Empty;
    }
}