using DartTournament.WPF.Navigator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DartTournament.WPF.Screens.Base
{
    public class ScreenBase : UserControl
    {
        protected IScreenNavigator _navigator;

        public ScreenBase(IScreenNavigator navigator)
        {
            _navigator = navigator;
        }
    }
}
