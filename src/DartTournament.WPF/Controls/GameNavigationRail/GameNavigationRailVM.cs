using DartTournament.WPF.NotifyPropertyChange;
using DartTournament.WPF.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Controls.GameNavigationRail
{
    internal class GameNavigationRailVM : NotifyPropertyChanged
    {
        private ObservableCollection<GameNavigationItem> _items;
        private GameNavigationItem _selectedItem;

        public GameNavigationRailVM()
        {
            Items = new ObservableCollection<GameNavigationItem>();

            Mediator.Subscribe("AddMenuItem", arg =>
            {
                if (arg is GameNavigationItem item)
                    Items.Add(item);
            });
        }

        public ObservableCollection<GameNavigationItem> Items { get => _items; set => SetProperty(ref _items, value); }
        public GameNavigationItem SelectedItem { get => _selectedItem; set => SetProperty(ref _selectedItem, value); }
    }
}
