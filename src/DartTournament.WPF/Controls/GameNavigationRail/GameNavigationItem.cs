using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DartTournament.WPF.Controls.GameNavigationRail
{
    public class GameNavigationtem
    {
        public string Text { get; set; }
        public UserControl Content { get; set; }
        public PackIconKind SelectedIcon { get; set; }
        public PackIconKind UnselectedIcon { get; set; }
        public bool IsBottomSection { get; set; } // NEW

        public GameNavigationtem(string text, UserControl content, PackIconKind selectedIcon, PackIconKind unselectedIcon, bool isBottomSection = false)
        {
            Text = text;
            Content = content;
            SelectedIcon = selectedIcon;
            UnselectedIcon = unselectedIcon;
            IsBottomSection = isBottomSection;
        }
    }
}
