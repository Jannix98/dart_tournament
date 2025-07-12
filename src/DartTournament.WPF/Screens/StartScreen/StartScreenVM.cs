using DartTournament.WPF.NotifyPropertyChange;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DartTournament.WPF.Controls.Game;
using DartTournament.WPF.Controls.PlayerOverview;
using MaterialDesignThemes.Wpf;
using DartTournament.WPF.Utils;

namespace DartTournament.WPF.Screens.StartScreen
{
    internal class StartScreenVM : NotifyPropertyChanged
    {
        public StartScreenVM()
        {
            AllMenuItems = new ObservableCollection<ApplicationMenueItem>
            {
                new ApplicationMenueItem("Games", new GameView(), PackIconKind.ControllerClassicOutline, PackIconKind.ControllerClassic, false),
                new ApplicationMenueItem("Player", new PlayerOverviewView(), PackIconKind.AccountOutline, PackIconKind.Account, false),
                new ApplicationMenueItem("Settings", new UserControl(), PackIconKind.CogOutline, PackIconKind.Cog, true),
            };

            Mediator.Subscribe("AddMenuItem", arg =>
            {
                if (arg is ApplicationMenueItem item)
                    AllMenuItems.Add(item);
            });

            Mediator.Subscribe("AddMenuItem1", arg =>
            {
                if (arg is ApplicationMenueItem item)
                    AllMenuItems.Add(item);
            });
        }

        ObservableCollection<ApplicationMenueItem> _allMenuItems;
        
        private ApplicationMenueItem _selectedMenuItem;
        public ApplicationMenueItem SelectedMenuItem
        {
            get => _selectedMenuItem;
            set => SetProperty(ref _selectedMenuItem, value);
        }

        private UserControl _selectedContent;
        public UserControl SelectedContent
        {
            get => _selectedContent;
            set
            {
                _selectedContent = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ApplicationMenueItem> AllMenuItems { get => _allMenuItems; set => SetProperty(ref _allMenuItems, value); }
    }

    public class ApplicationMenueItem
    {
        public string Text { get; set; }
        public UserControl Content { get; set; }
        public PackIconKind SelectedIcon { get; set; }
        public PackIconKind UnselectedIcon { get; set; }
        public bool IsBottomSection { get; set; } // NEW

        public ApplicationMenueItem(string text, UserControl content, PackIconKind selectedIcon, PackIconKind unselectedIcon, bool isBottomSection = false)
        {
            Text = text;
            Content = content;
            SelectedIcon = selectedIcon;
            UnselectedIcon = unselectedIcon;
            IsBottomSection = isBottomSection;
        }
    }
}
