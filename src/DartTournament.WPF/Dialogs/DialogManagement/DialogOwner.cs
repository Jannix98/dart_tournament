using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DartTournament.WPF.Dialogs.DialogManagement
{
    internal class DialogOwner : IDialogOwner
    {
        public Window GetApplicationMainWindow()
        {
            return System.Windows.Application.Current.MainWindow;
        }
    }

    public interface IDialogOwner
    {
        Window GetApplicationMainWindow();
    }
}
