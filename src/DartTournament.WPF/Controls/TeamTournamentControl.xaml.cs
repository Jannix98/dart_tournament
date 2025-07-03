using DartTournament.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DartTournament.WPF
{
    /// <summary>
    /// Interaction logic for TeamTournamentControl.xaml
    /// </summary>
    public partial class TeamTournamentControl : UserControl
    {
        //public TeamTournamentControl(TeamTournament teamTournament)
        //{
        //    InitializeComponent();
        //    teamName.Content = teamTournament?.Team?.Name;
        //    teamScore.Content = teamTournament?.Score;
        //}

        public TeamTournamentControl()
        {
            InitializeComponent();
            teamScore.Content = 4711;
        }

        internal void SetBottomTeamName(string name)
        {
            teamName.Content = name;
        }

        internal void SetTopTeamName(string name)
        {
            teamName.Content = name;
        }
    }
}
