using DartTournament.WPF.Controls.TournamentTree;
using DartTournament.WPF.Models;
using DartTournament.WPF.Utils.MatchCreator;
using DartTournament.WPF.Utils.MatchHandler;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DartTournament.WPF.Controls.GameTreeControl
{
    public partial class GameTreeControl : UserControl
    {
        GameMatchHandler _gameMatchHandler;

        public GameTreeControl(GameMatchHandler gameMatchHandler)
        {
            InitializeComponent();
            _gameMatchHandler = gameMatchHandler;
            this.DataContext = new GameTreeViewModel(gameMatchHandler);
        }

        
        private GameTreePanel FindGameTreePanel(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is GameTreePanel panel)
                    return panel;
                var result = FindGameTreePanel(child);
                if (result != null)
                    return result;
            }
            return null;
        }

        internal static GameTreeControl CreateGame(GameMatchHandler gameMatchHandler)
        {
            GameTreeControl gameTreeControl = new GameTreeControl(gameMatchHandler);
            return gameTreeControl;
        }
    }
}