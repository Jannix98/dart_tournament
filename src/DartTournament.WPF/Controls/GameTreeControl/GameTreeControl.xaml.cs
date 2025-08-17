using DartTournament.WPF.Controls.TournamentTree;
using DartTournament.WPF.Models;
using DartTournament.WPF.Utils.MatchCreator;
using DartTournament.WPF.Utils.MatchHandler;
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
            _gameMatchHandler.NotifyMatchChange += GameMatchHandler_NotifyMatchChange;
            this.DataContext = new GameTreeViewModel(gameMatchHandler);
            Loaded += UserControl_Loaded;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Find the GameTreePanel in the visual tree
            Refresh();
        }

        /// <summary>
        /// there is a problem with the Data Binding of <see cref="MatchControl"/> and the <see cref="GameTreePanel"/>. Everytime the value
        /// of some <see cref="MatchViewModel"/> changes, the UI does not render the information. Therefore we have to implement this workaround
        /// which is refreshing the panel
        /// </summary>
        private void Refresh()
        {
            Console.WriteLine("Trigger refresh");
            var panel = FindGameTreePanel(this);
            panel?.InvalidateMeasure();
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

        private void GameMatchHandler_NotifyMatchChange(object? sender, EventArgs e)
        {
            Refresh();
        }
    }
}