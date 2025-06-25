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
using DartTournament.WPF.Models;

namespace DartTournament.WPF.Dialogs.AddPlayer
{
    /// <summary>
    /// Interaction logic for AddTeamView.xaml
    /// </summary>
    public partial class AddPlayerView : BaseDialog, IAddPlayerView
    {
        public AddPlayerView(IDialogOwner dialogOwner) : base(dialogOwner)
        {
            InitializeComponent();
        }

        internal override BaseDialogResult ShowDialog()
        {
            var baseResult = base.ShowDialog();
            if (baseResult.DialogResult == true)
            {
                if (this.DataContext is AddPlayerVM vm)
                {
                    return new AddPlayerResult(true, vm.Player);
                }
            }
            return new AddPlayerResult(false, null);
        }
    }

    internal class AddPlayerResult : BaseDialogResult
    {
        public DartPlayerUI _player;

        public AddPlayerResult(bool? dialogResult, DartPlayerUI player) : base(dialogResult)
        {
            _player = player;
        }

        public DartPlayerUI Player { get => _player; set => _player = value; }
    }

    public interface IAddPlayerView
    {

    }
}
