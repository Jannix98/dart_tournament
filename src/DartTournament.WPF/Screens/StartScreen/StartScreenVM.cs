using DartTournament.WPF.NotifyPropertyChange;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DartTournament.WPF.Controls.Game;
using DartTournament.WPF.Controls.TeamOverview;
using MaterialDesignThemes.Wpf;

namespace DartTournament.WPF.Screens.StartScreen
{
    internal class StartScreenVM : NotifyPropertyChanged
    {
        public StartScreenVM()
        {
            MenueItems = new ObservableCollection<ApplicationMenueItem>
            {
                new ApplicationMenueItem("Games", new GameView(), PackIconKind.ControllerClassicOutline, PackIconKind.ControllerClassic),
                new ApplicationMenueItem("Team", new TeamOverviewView(), PackIconKind.AccountGroupOutline, PackIconKind.AccountGroup),
                new ApplicationMenueItem("Settings", new UserControl(), PackIconKind.CogOutline, PackIconKind.Cog),
            };
        }

        public ObservableCollection<ApplicationMenueItem> MenueItems { get; set; }

        private ApplicationMenueItem _selectedMenuItem;

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

        public ApplicationMenueItem SelectedMenuItem { get => _selectedMenuItem; set => SetProperty(ref _selectedMenuItem, value); }
    }

    public class ApplicationMenueItem
    {
        public string Text { get; set; }
        public UserControl Content { get; set; }
        public PackIconKind SelectedIcon { get; set; }
        public PackIconKind UnselectedIcon { get; set; }

        public ApplicationMenueItem(string text, UserControl content, PackIconKind selectedIcon, PackIconKind unselectedIcon)
        {
            Text = text;
            Content = content;
            SelectedIcon = selectedIcon;
            UnselectedIcon = unselectedIcon;
        }
    }
}
