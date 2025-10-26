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
using DartTournament.WPF.Controls.PlayerOverview;

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

        bool _isLightMode = true;

        public ToolbarControl()
        {
            InitializeComponent();
            _gameCreator = ServiceManager.Instance.GetRequiredService<GameCreator>();
            _playerManager = new PlayerManager();
            _gameLoader = ServiceManager.Instance.GetRequiredService<GameLoader>();
            //themeToggleButton.IsChecked = _isLightMode;
        }

        private void AddGameBtn_Click(object sender, RoutedEventArgs e)
        {
            _gameCreator.CreateGame();
        }

        private async void LoadGamesBtn_Click(object sender, RoutedEventArgs e)
        {
            _gameLoader.LoadGame();   
        }

        private async void PeopleBtn_Click(object sender, RoutedEventArgs e)
        {
            _playerManager.ShowPlayerDialog();
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void themeToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            SetTheme();
        }

        private void SetTheme()
        {
            //_isLightMode = themeToggleButton.IsChecked == true;

            //var paletteHelper = new PaletteHelper();
            //var theme = paletteHelper.GetTheme();

            //theme.SetBaseTheme(_isLightMode ? BaseTheme.Light : BaseTheme.Dark);
            //paletteHelper.SetTheme(theme);
        }

        private void themeToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            SetTheme();
        }
    }
}
