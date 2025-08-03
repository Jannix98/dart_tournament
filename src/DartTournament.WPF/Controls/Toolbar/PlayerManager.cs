using DartTournament.WPF.Dialogs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.WPF.Controls.Toolbar
{
    internal class PlayerManager
    {
        public void ShowPlayerDialog()
        {
            PlayerView playerView = new PlayerView();
            playerView.ShowDialog();
        }
    }
}
