using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DartTournament.WPF.Dialogs.DialogManagement;
using DartTournament.WPF.Models;

namespace DartTournament.WPF.Dialogs.CreateGame
{
    /// <summary>
    /// Interaction logic for CreateGameView.xaml
    /// </summary>
    public partial class CreateGameView : BaseDialog
    {
        public CreateGameView(IDialogOwner dialogOwner) : base(dialogOwner)
        {
            InitializeComponent();
            if (this.DataContext is CreateGameVM vm)
                vm.Dialog = this;
        }

        internal override CreateGameViewResult ShowDialog()
        {
            var baseResult = base.ShowDialog();
            List<DartPlayerUI> selectedTeams = new List<DartPlayerUI>();
            if (baseResult.DialogResult == true && this.DataContext is CreateGameVM vm)
                selectedTeams = vm.GetSelectedTeams();

            return new CreateGameViewResult(baseResult, selectedTeams);
        }
    }

    internal class CreateGameViewResult : BaseDialogResult
    {
        public List<DartPlayerUI> SelectedTeams { get; private set;  }

        public CreateGameViewResult(BaseDialogResult baseDialogResult, List<DartPlayerUI> selectedTeams) : base(baseDialogResult)
        {
            SelectedTeams = selectedTeams;
        }
    }
}
