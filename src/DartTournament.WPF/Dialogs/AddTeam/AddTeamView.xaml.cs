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

namespace DartTournament.WPF.Dialogs.AddTeam
{
    /// <summary>
    /// Interaction logic for AddTeamView.xaml
    /// </summary>
    public partial class AddTeamView : BaseDialog, IAddTeamView
    {
        public AddTeamView(IDialogOwner dialogOwner) : base(dialogOwner)
        {
            InitializeComponent();
        }

        public override BaseDialogResult ShowDialog()
        {
            var baseResult = base.ShowDialog();
            if (baseResult.DialogResult == true)
            {
                if (this.DataContext is AddTeamVM vm)
                {
                    return new AddTeamResult(true, vm.Team);
                }
            }
            return new AddTeamResult(false, null);
        }
    }

    public class AddTeamResult : BaseDialogResult
    {
        public Team _team;

        public AddTeamResult(bool? dialogResult, Team team) : base(dialogResult)
        {
            _team = team;
        }

        public Team Team { get => _team; set => _team = value; }
    }

    public interface IAddTeamView
    {

    }
}
