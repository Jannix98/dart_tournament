using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DartTournament.WPF.Dialogs.Base
{
    internal interface IDialogManager
    {
        BaseDialogResult ShowDialog<T>();
    }

    internal class DialogManager : IDialogManager
    {
        public BaseDialogResult ShowDialog<T>()
        {
            var instance = ServiceManager.ServiceManager.Instance.GetRequiredService<T>();
            if (!(instance is BaseDialog window))
                throw new ArgumentException(nameof(instance));

            return window.ShowDialog();
        }
    }
}
