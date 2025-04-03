using DartTournament.Application.UseCases.Teams.Services.Interfaces;
using DartTournament.Domain.Entities;
using DartTournament.WPF.NotifyPropertyChange;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DartTournament.WPF.Dialogs.DialogManagement;
using DartTournament.WPF.Dialogs.AddTeam;

namespace DartTournament.WPF.Controls.TeamOverview
{
    internal class TeamOverviewVM : NotifyPropertyChanged
    {
        ITeamService _teamService;
        IDialogManager _dialogManager;
        Team _selectedTeam;
        ObservableCollection<Team> _teams = new ObservableCollection<Team>();
        private bool _isLoading = true;
        ICommand _addTeamCommand;

        public TeamOverviewVM()
        {
            _teamService = ServiceManager.ServiceManager.Instance.GetRequiredService<ITeamService>();
            _dialogManager = ServiceManager.ServiceManager.Instance.GetRequiredService<IDialogManager>();
            AddTeamCommand = new RelayCommand(() => AddTeam());
        }

        private async void AddTeam()
        {
            AddTeamResult? result = _dialogManager.ShowDialog<IAddTeamView>() as AddTeamResult;
            if (result?.DialogResult != true)
                return;
            var team = await _teamService.CreateTeamAsync(result.Team.Name);
            Teams.Add(team);
        }

        public Team SelectedTeam { get => _selectedTeam; set => SetProperty(ref _selectedTeam, value); }
        public ObservableCollection<Team> Teams { get => _teams; set => SetProperty(ref _teams, value); }
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }
        public ICommand AddTeamCommand { get => _addTeamCommand; set => _addTeamCommand = value; }


        public async Task LoadTeamsAsync()
        {
            try
            {
                // TODO this async shit is not working. Fix it
                IsLoading = true;
                var data = await _teamService.GetTeamsAsync();
                Teams = new ObservableCollection<Team>(data);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., logging, displaying a message)
            }
            finally
            {
                IsLoading = false;
            }
        }

    }
}
