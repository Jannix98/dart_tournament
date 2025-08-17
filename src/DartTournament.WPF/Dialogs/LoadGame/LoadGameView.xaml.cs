using DartTournament.WPF.Dialogs.Base;
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
using System.Windows.Shapes;

namespace DartTournament.WPF.Dialogs.LoadGame
{
    /// <summary>
    /// Interaction logic for LoadGameView.xaml
    /// </summary>
    public partial class LoadGameView : BaseDialog
    {
        public LoadGameView(IDialogOwner dialogOwner) : base(dialogOwner)
        {
            InitializeComponent();
            
            if (this.DataContext is LoadGameVM vm)
            {
                vm.Dialog = this;
                this.Loaded += LoadGameView_Loaded;
            }
        }

        private async void LoadGameView_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is LoadGameVM vm)
            {
                await vm.LoadGamesAsync();
            }
        }

        public LoadGameViewResult ShowDialog()
        {
            var baseResult = base.ShowDialog();
            var vm = this.DataContext as LoadGameVM;
            
            return new LoadGameViewResult(baseResult.DialogResult, vm?.SelectedGame);
        }
    }

    public class LoadGameViewResult
    {
        public bool? DialogResult { get; }
        public DartTournament.Application.DTO.Game.GameResult SelectedGame { get; }

        public LoadGameViewResult(bool? dialogResult, DartTournament.Application.DTO.Game.GameResult selectedGame)
        {
            DialogResult = dialogResult;
            SelectedGame = selectedGame;
        }
    }
}
