using DartTournament.WPF.Models;
using System.Windows.Controls;

namespace DartTournament.WPF.Controls.PlayerOverview
{
    /// <summary>
    /// Interaction logic for AddOrEditPlayerDialog.xaml
    /// </summary>
    public partial class AddOrEditPlayerDialog : UserControl
    {
        public AddOrEditPlayerDialog(DartPlayerUI player)
        {
            InitializeComponent();
            if (this.DataContext is not AddOrEditPlayerDialogVM vm)
            {
                throw new System.InvalidOperationException("DataContext must be of type AddOrEditPlayerDialogVM");
            }
                
            if(player == null)
            {
                throw new System.ArgumentNullException(nameof(player), "Player cannot be null");
            }

            vm.Player = player;
            vm.IsEditMode = true;
        }
    }
}