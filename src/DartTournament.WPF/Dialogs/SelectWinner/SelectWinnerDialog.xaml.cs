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
        Guid _looserId;
        string _looserName;

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

        internal void SetWinner(Guid winnerId, string winnerName, Guid looserId, string looserName)
        {
            _winnerId = winnerId;
            _winnerName = winnerName;
            _looserId = looserId;
            _looserName = looserName;
            this.DialogResult = true;
            this.Close();
        }

        internal override BaseDialogResult ShowDialog()
        {
            this.DataContext = _match;
            var baseResult = base.ShowDialog();
            if (baseResult.DialogResult == true)
            {
                return new SelectWinnerResult(_winnerId, _winnerName, _looserId, _looserName, true);
            }
            return new SelectWinnerResult(Guid.Empty, null, Guid.Empty, null, false);
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            // TODO guid
            SetWinner(Guid.Empty, _match.Team1Name, Guid.Empty, _match.Team2Name);
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            // TODO guid
            SetWinner(Guid.Empty, _match.Team2Name, Guid.Empty, _match.Team1Name);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }

    public class SelectWinnerResult : BaseDialogResult
    {
        public Guid WinnderId { get; set; }
        public string WinnerName { get; set; }

        public Guid LooserId { get; set; }
        public string LooserName { get; set; }

        public SelectWinnerResult(Guid winnderId, string winnerName, Guid looserId, string looserName, bool dialogResult) 
            : base(dialogResult)
        {
            WinnderId = winnderId;
            WinnerName = winnerName;
            LooserId = looserId;
            LooserName = looserName;
        }
    }

    public interface ISelectWinnerDialog
    {
    }
}
