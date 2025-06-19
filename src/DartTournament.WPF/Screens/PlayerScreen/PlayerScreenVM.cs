using DartTournament.Domain.Entities;
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
                PlayerCollection.Add(new DartPlayer($"Team {i + 1}"));
            }
        }

        DartPlayer selectedPlayer;
        ObservableCollection<DartPlayer> teams = new ObservableCollection<DartPlayer>();

        public DartPlayer SelectedPlayer { get => selectedPlayer; set => SetProperty(ref selectedPlayer, value); }
        public ObservableCollection<DartPlayer> PlayerCollection { get => teams; set => SetProperty(ref teams, value); }
    }
}
