using DartTournament.WPF.Navigator;
using DartTournament.WPF.Screens.StartScreen;
using System.Windows;
using DartTournament.WPF.ServiceManager;

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
            navigator = new ScreenNavigator();
            navigator.Initialize(MainFrame);
            navigator.NavigateTo(new StartScreenView(navigator));
        }


    }
}