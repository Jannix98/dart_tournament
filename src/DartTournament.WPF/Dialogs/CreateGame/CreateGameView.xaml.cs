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
using DartTournament.WPF.Controls.Game.GameMenue;
using DartTournament.WPF.Dialogs.Base;
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
            if(this.DataContext is not CreateGameVM vm)
            {
                throw new InvalidOperationException("DataContext must be of type CreateGameVM");
            }
            vm.Dialog = this;
        }

        internal override CreateGameViewResult ShowDialog()
        {
            var baseResult = base.ShowDialog();
            List<DartPlayerUI> selectedTeams = new List<DartPlayerUI>();
            var vm = this.DataContext as CreateGameVM;
            if(vm == null)
                throw new InvalidOperationException("DataContext must be of type CreateGameVM");

            if (baseResult.DialogResult == true)
                selectedTeams = vm.GetSelectedTeams();

            return new CreateGameViewResult(baseResult, selectedTeams, vm.AdvancedMode, vm.TournamentName);
        }
    }

    
}
