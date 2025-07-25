using DartTournament.WPF.Controls.TournamentTree;
using DartTournament.WPF.Models;
using DartTournament.WPF.Utils.MatchCreator;
using System.Windows.Controls;

namespace DartTournament.WPF.Controls.GameTreeControl
{
    public partial class GameTreeControl : UserControl
    {
        public GameTreeControl()
        {
            InitializeComponent();
        }

        internal static GameTreeControl CreateGame(ICollection<RoundViewModel> roundViewModels)
        {
            GameTreeControl gameTreeControl = new GameTreeControl();
            var roundCollection = new System.Collections.ObjectModel.ObservableCollection<RoundViewModel>(roundViewModels);
            gameTreeControl.DataContext = new GameTreeViewModel(roundCollection);
            return gameTreeControl;
        }
    }
}