using DartTournament.WPF.Navigator;
using DartTournament.WPF.Screens.StartScreen;

using DartTournament.WPF.ServiceManager;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;
using System.Windows;

namespace TestDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IScreenNavigator navigator;
        
        public MainWindow()
        {
            InitializeComponent();
            App app = (App)Application.Current;
            
            navigator = new ScreenNavigator();
            navigator.Initialize(MainFrame);
            navigator.NavigateTo(new StartScreenView(navigator));

            SetPurpleThemeLight();
        }

        /// <summary>
        /// i didn't like the color of the purple theme, so I changed it.
        /// this is a temporary solution, but it works.
        /// </summary>
        /// <param name="paletteHelper"></param>
        /// <param name="theme"></param>
        private static void SetPurpleThemeLight()
        {
            //var swatches = new SwatchesProvider().Swatches;
            //var deepPurple = swatches.FirstOrDefault(x => x.Name == "deeppurple");
            ////Color color = Colors.Dee
            //PaletteHelper paletteHelper = new PaletteHelper();
            //Theme theme = paletteHelper.GetTheme();
            //var c700 = deepPurple.PrimaryHues.FirstOrDefault(x => x.Name == "Primary400");
            //theme.PrimaryLight = c700.Color.Lighten();
            //theme.PrimaryMid = c700.Color;
            //theme.PrimaryDark = c700.Color.Darken();
            //paletteHelper.SetTheme(theme);   aaa
        }
    }

}