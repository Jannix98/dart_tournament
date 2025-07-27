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

namespace DartTournament.WPF.Controls.GameTreeControl.MatchControl
{
    /// <summary>
    /// Interaction logic for MatchControl.xaml
    /// </summary>
    public partial class MatchControl : UserControl
    {
        public static readonly DependencyProperty MatchProperty =
            DependencyProperty.Register(
                nameof(Match),
                typeof(MatchViewModel),
                typeof(MatchControl),
                new PropertyMetadata(null, OnMatchChanged));

        public MatchViewModel Match
        {
            get => (MatchViewModel)GetValue(MatchProperty);
            set => SetValue(MatchProperty, value);
        }

        private static void OnMatchChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public MatchControl()
        {
            InitializeComponent();
        }

    }
}
