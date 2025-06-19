using System.Windows;
using System.Windows.Controls;
using DartTournament.Application.UseCases.Player.Services.Interfaces;

namespace DartTournament.WPF.Controls.PlayerOverview
{
    /// <summary>
    /// Interaction logic for TeamOverviewView.xaml
    /// </summary>
    public partial class PlayerOverviewView : UserControl
    {
        public PlayerOverviewView()
        {
            InitializeComponent();
            this.Loaded += TeamOverviewView_Loaded;
        }


        private async void TeamOverviewView_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is PlayerOverviewVM vm)
            {
                await vm.LoadTeamsAsync();
            }
        }
    }
}
