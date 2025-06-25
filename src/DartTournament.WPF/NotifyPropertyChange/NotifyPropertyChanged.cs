using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.NotifyPropertyChange
{
    internal class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<Tp>(ref Tp variable, Tp value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<Tp>.Default.Equals(variable, value)) return false;

            variable = value;
            OnPropertyChanged(propertyName);

            return true;
        }
    }
}
