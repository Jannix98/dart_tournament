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
using DartTournament.Domain.Entities;
using DartTournament.WPF.Dialogs.DialogManagement;

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

        public override CreateGameViewResult ShowDialog()
        {
            var baseResult = base.ShowDialog();
            List<Team> selectedTeams = new List<Team>();
            if (baseResult.DialogResult == true && this.DataContext is CreateGameVM vm)
                selectedTeams = vm.GetSelectedTeams();

            return new CreateGameViewResult(baseResult, selectedTeams);
        }
    }

    public class CreateGameViewResult : BaseDialogResult
    {
        public List<Team> SelectedTeams { get; private set;  }

        public CreateGameViewResult(BaseDialogResult baseDialogResult, List<Team> selectedTeams) : base(baseDialogResult)
        {
            SelectedTeams = selectedTeams;
        }
    }
}
