using DartTournament.Domain.Entities;
using DartTournament.WPF.NotifyPropertyChange;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Screens.TeamScreen
{
    internal class TeamScreenVM : NotifyPropertyChanged
    {
        public TeamScreenVM() 
        {
            int count = 16;
            for (int i = 0; i < count; i++) 
            {
                Teams.Add(new Team($"Team {i + 1}"));
            }
        }

        Team selectedTeam;
        ObservableCollection<Team> teams = new ObservableCollection<Team>();

        public Team SelectedTeam { get => selectedTeam; set => SetProperty(ref selectedTeam, value); }
        public ObservableCollection<Team> Teams { get => teams; set => SetProperty(ref teams, value); }
    }
}
