using DartTournament.WPF.Controls;
using DartTournament.WPF.Navigator;
using DartTournament.WPF.Screens.Base;
using System.Windows;

namespace DartTournament.WPF.Screens.StartScreen
{
    /// <summary>
    /// Interaction logic for StartScreenView.xaml
    /// </summary>
    public partial class StartScreenView : ScreenBase
    {
        public StartScreenView(IScreenNavigator navigator) : base(navigator)
        {
            InitializeComponent();
        }
    }
}
