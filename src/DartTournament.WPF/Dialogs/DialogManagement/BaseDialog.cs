using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DartTournament.WPF.Dialogs.DialogManagement
{
    public class BaseDialog : Window, IBaseDialog
    {
        public BaseDialog(IDialogOwner dialogOwner)
        {
            this.Owner = dialogOwner?.GetApplicationMainWindow();
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }


        internal virtual new BaseDialogResult ShowDialog()
        {
            bool? result = base.ShowDialog();
            return new BaseDialogResult(result);
        }

        public void CloseWindow(bool result)
        {
            this.DialogResult = result;
            this.Close();
        }
    }

    public interface IBaseDialog
    {
        void CloseWindow(bool result);
    }
}
