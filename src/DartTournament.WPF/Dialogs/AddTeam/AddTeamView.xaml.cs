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
    public partial class AddTeamView : BaseDialog, IAddPlayerView
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
                    return new AddPlayerResult(true, vm.Team);
                }
            }
            return new AddPlayerResult(false, null);
        }
    }

    public class AddPlayerResult : BaseDialogResult
    {
        public DartPlayer _team;

        public AddPlayerResult(bool? dialogResult, DartPlayer team) : base(dialogResult)
        {
            _team = team;
        }

        public DartPlayer Team { get => _team; set => _team = value; }
    }

    public interface IAddPlayerView
    {

    }
}
