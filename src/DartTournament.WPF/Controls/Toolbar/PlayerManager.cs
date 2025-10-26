using DartTournament.WPF.Controls.PlayerOverview;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Controls.Toolbar
{
    internal class PlayerManager
    {
        public async void ShowPlayerDialog()
        {
            // Create the dialog content
            var playerDialog = new PlayerOverviewDialog();

            // Show the dialog using DialogHost
            var result = await DialogHost.Show(playerDialog, "RootDialogHost");

            // Handle result if needed (DialogHost returns the result when closed)
            // The result will be null if closed via CloseDialogCommand or ESC
        }
    }
}
