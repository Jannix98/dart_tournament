using DartTournament.WPF.Controls.GameTreeControl;
using DartTournament.WPF.Dialogs.Base;
using DartTournament.WPF.Models;
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

namespace DartTournament.WPF.Dialogs.SelectWinner
{
    /// <summary>
    /// Interaction logic for SelectWinnerDialog.xaml
    /// </summary>
    public partial class SelectWinnerDialog : BaseDialog, ISelectWinnerDialog
    {
        MatchViewModel _match;
        Guid _winnerId;
        string _winnerName;

        public SelectWinnerDialog(IDialogOwner dialogOwner) : base(dialogOwner)
        {
            InitializeComponent();
            this.Loaded += SelectWinnerDialog_Loaded;
        }

        private void SelectWinnerDialog_Loaded(object sender, RoutedEventArgs e)
        {
            _match = (this.Input as SelectWinnerInput)?.Match;
            if (_match == null)
            {
                throw new ArgumentNullException(nameof(_match), "Match cannot be null");
            }
            btn1.Content = _match.Team1Name;
            btn2.Content = _match.Team2Name;
        }

        internal void SetWinner(Guid winnerId, string winnerName)
        {
            _winnerId = winnerId;
            _winnerName = winnerName;
            this.DialogResult = true;
            this.Close();
        }

        internal override BaseDialogResult ShowDialog()
        {
            this.DataContext = _match;
            var baseResult = base.ShowDialog();
            if (baseResult.DialogResult == true)
            {
                return new SelectWinnerResult(true, _winnerId, _winnerName);
            }
            return new SelectWinnerResult(false, Guid.Empty, null);
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            // TODO guid
            SetWinner(Guid.Empty, _match.Team1Name);
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            // TODO guid
            SetWinner(Guid.Empty, _match.Team2Name);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }

    internal class SelectWinnerResult : BaseDialogResult
    {
        public Guid WinnderId { get; set; }
        public string WinnerName { get; set; } 

        public SelectWinnerResult(bool dialogResult, Guid id, string name) : base(dialogResult)
        {
            WinnderId = id;
            WinnerName = name;
        }

    }

    public interface ISelectWinnerDialog
    {
    }
}
