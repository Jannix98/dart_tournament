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
        BaseDialogResult ShowDialog<T>(BaseDialogInput input);
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

        private static BaseDialog GetBaseDialog<T>()
        {
            var instance = ServiceManager.ServiceManager.Instance.GetRequiredService<T>();
            if (!(instance is BaseDialog window))
                throw new ArgumentException(nameof(instance));
            return window;
        }

        // TODO: remove this and switch to factory pattern when creating the dialog!
        public BaseDialogResult ShowDialog<T>(BaseDialogInput input)
        {
            BaseDialog window = GetBaseDialog<T>();
            window.Input = input;
            return window.ShowDialog();
        }
    }
}
