using CommunityToolkit.Mvvm.Input;
using DartTournament.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using DartTournament.WPF.NotifyPropertyChange;

namespace DartTournament.WPF.Dialogs.AddTeam
{
    internal class AddTeamVM : NotifyPropertyChanged
    {
        private string _teamName;
        public string TeamName { get => _teamName; set => SetProperty(ref _teamName, value); }
    
        
        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }


        public AddTeamVM()
        {
            ConfirmCommand = new RelayCommand(OnConfirm);
            CancelCommand = new RelayCommand(OnCancel);
        }

        private void OnConfirm()
        {
            // Rückgabe der Team-Instanz
            Team = new DartPlayer(TeamName);
            CloseDialog(true);
        }

        private void OnCancel()
        {
            CloseDialog(false);
        }

        public DartPlayer Team { get; private set; }

        private void CloseDialog(bool dialogResult)
        {
            // Schließt den Dialog, wenn ein Resultat übergeben wird
            var dialog = System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            if (dialog != null)
            {
                dialog.DialogResult = dialogResult;
                dialog.Close();
            }
        }

    }

}
