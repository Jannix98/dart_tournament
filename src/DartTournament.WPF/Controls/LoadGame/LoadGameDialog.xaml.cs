using System.Windows.Controls;

namespace DartTournament.WPF.Controls.LoadGame
{
    /// <summary>
    /// Interaction logic for LoadGameDialog.xaml
    /// </summary>
    public partial class LoadGameDialog : UserControl
    {
        public LoadGameDialog()
        {
            InitializeComponent();
            this.Loaded += LoadGameDialog_Loaded;
        }

        private async void LoadGameDialog_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.DataContext is LoadGameDialogVM vm)
            {
                await vm.LoadGamesAsync();
            }
        }
    }
}