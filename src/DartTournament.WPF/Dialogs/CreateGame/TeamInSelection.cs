using DartTournament.Domain.Entities;
using DartTournament.WPF.Models;
using DartTournament.WPF.NotifyPropertyChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Dialogs.CreateGame
{
    internal class TeamInSelection : NotifyPropertyChanged
    {
        private bool _isChecked;

        public DartPlayerUI Team { get; set; }

        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        public TeamInSelection(DartPlayerUI team)
        {
            Team = team;
            IsChecked = false; // Standardmäßig nicht ausgewählt
        }
    }
}
