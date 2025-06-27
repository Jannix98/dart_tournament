using DartTournament.WPF.NotifyPropertyChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Dialogs.Base
{
    internal class BaseDialogVM : NotifyPropertyChanged
    {
        private IBaseDialog _dialog;

        public IBaseDialog Dialog { get => _dialog; set => _dialog = value; }
    }
}
