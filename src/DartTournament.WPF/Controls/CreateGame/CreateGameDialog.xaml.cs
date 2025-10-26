using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DartTournament.WPF.Models;

namespace DartTournament.WPF.Controls.CreateGame
{
    /// <summary>
    /// Interaction logic for CreateGameDialog.xaml
    /// </summary>
    public partial class CreateGameDialog : UserControl
    {
        public CreateGameDialog()
        {
            InitializeComponent();
            this.Loaded += CreateGameDialog_Loaded;
        }

        private async void CreateGameDialog_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is CreateGameDialogVM vm)
            {
                await vm.LoadPlayersAsync();
            }
        }

        private void PlayerListItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem item && item.DataContext is SelectableDartPlayerUI player)
            {
                player.IsSelected = !player.IsSelected;
            }
        }

        private void PlayerList_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            // Prevent automatic scrolling
            e.Handled = true;
        }
    }
}