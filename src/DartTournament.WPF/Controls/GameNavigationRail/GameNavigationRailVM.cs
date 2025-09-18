using CommunityToolkit.Mvvm.Input;
using DartTournament.WPF.Controls.Toolbar;
using DartTournament.WPF.NotifyPropertyChange;
using DartTournament.WPF.SM;
using DartTournament.WPF.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DartTournament.WPF.Controls.GameNavigationRail
{
    internal class GameNavigationRailVM : NotifyPropertyChanged
    {
        private ObservableCollection<GameNavigationItem> _items;
        private GameNavigationItem _selectedItem;
        GameCreator _gameCreator;
        GameLoader _gameLoader;
        PlayerManager _playerManager;

        public GameNavigationRailVM()
        {
            Items = new ObservableCollection<GameNavigationItem>();

            Mediator.Subscribe("AddMenuItem", arg =>
            {
                if (arg is GameNavigationItem item)
                    Items.Add(item);
            });

            _gameCreator = ServiceManager.Instance.GetRequiredService<GameCreator>();
            _playerManager = new PlayerManager();
            _gameLoader = ServiceManager.Instance.GetRequiredService<GameLoader>();

            // Initialize commands
            AddGameCommand = new RelayCommand(AddGame);
            LoadGamesCommand = new RelayCommand(LoadGames);
            PeopleCommand = new RelayCommand(ShowPeopleDialog);
        }

        public ObservableCollection<GameNavigationItem> Items { get => _items; set => SetProperty(ref _items, value); }
        public GameNavigationItem SelectedItem { get => _selectedItem; set => SetProperty(ref _selectedItem, value); }

        public ICommand AddGameCommand { get; }
        public ICommand LoadGamesCommand { get; }
        public ICommand PeopleCommand { get; }

        private void AddGame()
        {
            _gameCreator.CreateGame();
        }

        private void LoadGames()
        {
            _gameLoader.LoadGame();
        }

        private void ShowPeopleDialog()
        {
            _playerManager.ShowPlayerDialog();
        }
    }
}
