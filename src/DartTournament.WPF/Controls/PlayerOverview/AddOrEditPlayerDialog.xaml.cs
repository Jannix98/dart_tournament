using System.Windows.Controls;

namespace DartTournament.WPF.Controls.PlayerOverview
{
    /// <summary>
    /// Interaction logic for AddOrEditPlayerDialog.xaml
    /// </summary>
    public partial class AddOrEditPlayerDialog : UserControl
    {
        public AddOrEditPlayerDialog()
        {
            InitializeComponent();
        }

        public AddOrEditPlayerDialog(string existingPlayerName = null)
        {
            InitializeComponent();
            if (this.DataContext is AddOrEditPlayerDialogVM vm && !string.IsNullOrEmpty(existingPlayerName))
            {
                vm.PlayerName = existingPlayerName;
                vm.IsEditMode = true;
            }
        }
    }
}