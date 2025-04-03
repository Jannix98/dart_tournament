using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DartTournament.WPF.Navigator
{
    public class ScreenNavigator : IScreenNavigator
    {
        private Frame _navigationFrame;

        private Frame NavigationFrame {
            get
            {
                if (_navigationFrame == null)
                    throw new ArgumentNullException($"Call {nameof(IScreenNavigator.Initialize)} first!");

                return _navigationFrame;

            }
            set => _navigationFrame = value;
        }

        public void NavigateTo(UserControl page)
        {
            NavigationFrame.Content = page;
        }

        public void Initialize(Frame navigationFrame)
        {
            NavigationFrame = navigationFrame;
        }
    }

    public interface IScreenNavigator
    {
        void NavigateTo(UserControl page);
        void Initialize(Frame navigationFrame);
    }
}
