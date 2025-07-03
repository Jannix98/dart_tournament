using DartTournament.WPF.Controls.TournamentTree;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DartTournament.WPF.Controls
{
    /// <summary>
    /// Interaction logic for TournamentTree.xaml
    /// </summary>
    public partial class TournamentTreeControl : UserControl
    {
        LineManager _lineManager;
        TreePainter _treePainter;
        TournamentTreeControlParameter _parameter;

        internal TournamentTreeControl(TournamentTreeControlParameter parameter)
        {
            InitializeComponent();
            Loaded += TournamentTreeControl_Loaded;
            _parameter = parameter;
        }

        private void TournamentTreeControl_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        public void Load()
        {
            _treePainter = new TreePainter();
            var uiElements = _treePainter.CreateTournamentTree(_parameter.MaxPlayers, _parameter.Players);
            foreach (var element in uiElements) 
            {
                myStack.Children.Add(element);
            }
            _lineManager = new LineManager(myGrid, myCanvas);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            myCanvas.Children.Clear();
            _lineManager.CreateLinesBetweenMatches(myStack.Children);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (zoomTransform != null)
            {
                zoomTransform.ScaleX = e.NewValue;
                zoomTransform.ScaleY = e.NewValue;
            }
        }
    }
}
