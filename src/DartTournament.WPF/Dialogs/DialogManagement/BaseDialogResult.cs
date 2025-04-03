using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Dialogs.DialogManagement
{
    public class BaseDialogResult
    {
        private bool? _dialogResult;

        public BaseDialogResult(bool? dialogResult)
        {
            DialogResult = dialogResult;
        }

        public BaseDialogResult(BaseDialogResult baseDialogResult)
        {
            DialogResult = baseDialogResult.DialogResult;
        }

        public bool? DialogResult { get => _dialogResult; private set => _dialogResult = value; }
    }
}
