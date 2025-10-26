using DartTournament.WPF.Controls.GameTreeControl;
using DartTournament.WPF.Models;
using System;
using System.Windows.Controls;

namespace DartTournament.WPF.Controls.Game
{
    /// <summary>
    /// Interaction logic for SelectWinnerDialog.xaml
    /// </summary>
    public partial class SelectWinnerDialog : UserControl
    {
        public SelectWinnerDialog()
        {
            InitializeComponent();
        }

        public SelectWinnerDialog(MatchViewModel match)
        {
            InitializeComponent();
            if (this.DataContext is SelectWinnerDialogVM vm)
            {
                vm.InitializeMatch(match);
            }
        }
    }
}