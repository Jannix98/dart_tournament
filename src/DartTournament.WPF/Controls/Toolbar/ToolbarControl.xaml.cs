using DartTournament.Presentation.Base.Services;
using DartTournament.WPF.SM;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DartTournament.WPF.Controls.Toolbar
{
    /// <summary>
    /// Interaction logic for ToolbarControl.xaml
    /// </summary>
    public partial class ToolbarControl : UserControl
    {
        GameCreator _gameCreator;
        GameLoader _gameLoader;
        PlayerManager _playerManager;

        public ToolbarControl()
        {
            InitializeComponent();
            _gameCreator = ServiceManager.Instance.GetRequiredService<GameCreator>();
            _playerManager = new PlayerManager();
            _gameLoader = ServiceManager.Instance.GetRequiredService<GameLoader>();
        }


        private void AddGameBtn_Click(object sender, RoutedEventArgs e)
        {
            _gameCreator.CreateGame();
        }

        private void LoadGamesBtn_Click(object sender, RoutedEventArgs e)
        {
            _gameLoader.LoadGame();   
        }

        private void PeopleBtn_Click(object sender, RoutedEventArgs e)
        {
            _playerManager.ShowPlayerDialog();
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
