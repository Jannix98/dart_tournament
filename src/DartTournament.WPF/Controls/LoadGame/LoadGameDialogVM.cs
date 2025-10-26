using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using DartTournament.Application.DTO.Game;
using DartTournament.Presentation.Base.Services;
using DartTournament.WPF.SM;
using MaterialDesignThemes.Wpf;

namespace DartTournament.WPF.Controls.LoadGame
{
    public class LoadGameDialogVM : INotifyPropertyChanged
    {
        private ObservableCollection<GameResult> _games;
        private GameResult _selectedGame;
        private bool _isLoading;
        private IGamePresentationService _gamePresentationService;

        public LoadGameDialogVM()
        {
            Games = new ObservableCollection<GameResult>();
            EditCommand = new RelayCommand<GameResult>(EditGame);
            DeleteCommand = new RelayCommand<GameResult>(DeleteGame);
            OpenCommand = new RelayCommand(OpenGame, CanOpenGame);
            CloseCommand = new RelayCommand(CloseDialog);
            LoadGamesCommand = new RelayCommand(async () => await LoadGamesAsync());
            _gamePresentationService = ServiceManager.Instance.GetRequiredService<IGamePresentationService>();
        }

        public ObservableCollection<GameResult> Games
        {
            get => _games;
            set
            {
                _games = value;
                OnPropertyChanged();
            }
        }

        public GameResult SelectedGame
        {
            get => _selectedGame;
            set
            {
                _selectedGame = value;
                OnPropertyChanged();
                ((RelayCommand)OpenCommand).RaiseCanExecuteChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand LoadGamesCommand { get; }

        public async Task LoadGamesAsync()
        {
            IsLoading = true;
            try
            {
                Games.Clear();
                var games = await _gamePresentationService.GetAllGames();
                Games = new ObservableCollection<GameResult>(games);
            }
            catch (Exception ex)
            {
                // TODO: Handle exception properly
                System.Diagnostics.Debug.WriteLine($"Error loading games: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void EditGame(GameResult game)
        {
            // TODO: Implement edit functionality
            System.Diagnostics.Debug.WriteLine($"Edit game: {game?.Name}");
        }

        private void DeleteGame(GameResult game)
        {
            // TODO: Implement delete functionality
            System.Diagnostics.Debug.WriteLine($"Delete game: {game?.Name}");
        }

        private void OpenGame()
        {
            if (SelectedGame != null)
            {
                // Close DialogHost with the selected game as result
                DialogHost.Close("RootDialogHost", SelectedGame);
            }
        }

        private bool CanOpenGame()
        {
            return SelectedGame != null;
        }

        private void CloseDialog()
        {
            // Close DialogHost with null result
            DialogHost.Close("RootDialogHost", null);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke((T)parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}