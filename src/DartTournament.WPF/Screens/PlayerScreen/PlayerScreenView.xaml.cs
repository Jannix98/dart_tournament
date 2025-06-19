using DartTournament.WPF.Navigator;
using DartTournament.WPF.Screens.Base;
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

namespace DartTournament.WPF.Screens.PlayerScreen
{
    /// <summary>
    /// Interaction logic for TeamScreenView.xaml
    /// </summary>
    public partial class PlayerScreenView : ScreenBase
    {
        public PlayerScreenView(IScreenNavigator navigator) : base(navigator)
        {
            InitializeComponent();
        }
    }
}
