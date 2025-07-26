using DartTournament.WPF.Controls.TournamentTree;
using DartTournament.WPF.Models;
using DartTournament.WPF.Utils.MatchCreator;
using DartTournament.WPF.Utils.MatchHandler;
using System.Windows.Controls;

namespace DartTournament.WPF.Controls.GameTreeControl
{
    public partial class GameTreeControl : UserControl
    {
        public GameTreeControl()
        {
            InitializeComponent();
        }

        internal static GameTreeControl CreateGame(GameMatchHandler gameMatchHandler)
        {
            GameTreeControl gameTreeControl = new GameTreeControl();
            gameTreeControl.DataContext = new GameTreeViewModel(gameMatchHandler);
            return gameTreeControl;
        }
    }
}