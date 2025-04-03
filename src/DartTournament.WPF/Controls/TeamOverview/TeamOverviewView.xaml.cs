using System.Windows;
using System.Windows.Controls;
using DartTournament.Application.UseCases.Teams.Services.Interfaces;

namespace DartTournament.WPF.Controls.TeamOverview
{
    /// <summary>
    /// Interaction logic for TeamOverviewView.xaml
    /// </summary>
    public partial class TeamOverviewView : UserControl
    {
        public TeamOverviewView()
        {
            InitializeComponent();
            this.Loaded += TeamOverviewView_Loaded;
        }


        private async void TeamOverviewView_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is TeamOverviewVM vm)
            {
                await vm.LoadTeamsAsync();
            }
        }
    }
}
