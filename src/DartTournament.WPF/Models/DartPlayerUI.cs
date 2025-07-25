using System;
using DartTournament.WPF.NotifyPropertyChange;

namespace DartTournament.WPF.Models
{
    public class DartPlayerUI : NotifyPropertyChanged
    {
        private Guid _id;
        private string _name;

        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public DartPlayerUI() { }

        public DartPlayerUI(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public DartPlayerUI(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}