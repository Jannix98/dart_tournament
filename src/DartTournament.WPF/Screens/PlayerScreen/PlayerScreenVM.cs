using DartTournament.WPF.Models;
using DartTournament.WPF.NotifyPropertyChange;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Screens.PlayerScreen
{
    internal class PlayerScreenVM : NotifyPropertyChanged
    {
        public PlayerScreenVM() 
        {
            int count = 16;
            for (int i = 0; i < count; i++) 
            {
                PlayerCollection.Add(new DartPlayerUI($"Team {i + 1}"));
            }
        }

        DartPlayerUI selectedPlayer;
        ObservableCollection<DartPlayerUI> teams = new ObservableCollection<DartPlayerUI>();

        public DartPlayerUI SelectedPlayer { get => selectedPlayer; set => SetProperty(ref selectedPlayer, value); }
        public ObservableCollection<DartPlayerUI> PlayerCollection { get => teams; set => SetProperty(ref teams, value); }
    }
}
