using CommunityToolkit.Mvvm.Input;
using DartTournament.Application.UseCases.Teams.Services.Interfaces;
using DartTournament.WPF.NotifyPropertyChange;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DartTournament.Domain.Entities;
using DartTournament.WPF.Dialogs.DialogManagement;

namespace DartTournament.WPF.Dialogs.CreateGame
{
    internal class CreateGameVM : NotifyPropertyChanged
    {
        private readonly ITeamService _teamService;
        private ObservableCollection<TeamInSelection> _teamsInSelection;
        private bool _selectAllIsSelected;
        private TournamentSize _selectedTournamentSize = TournamentSize.X16;
        private IBaseDialog _dialog;

        public ObservableCollection<TeamInSelection> TeamsInSelection
        {
            get => _teamsInSelection;
            set => SetProperty(ref _teamsInSelection, value);
        }

        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }

        public bool SelectAllIsSelected
        {
            get => _selectAllIsSelected;
            set
            {
                if (SetProperty(ref _selectAllIsSelected, value))
                    ChangeIsSelectedOfAllTeams(_selectAllIsSelected);
            }
        }

        public TournamentSize SelectedTournamentSize { get => _selectedTournamentSize; set => SetProperty(ref _selectedTournamentSize, value); }

        public IBaseDialog Dialog { get => _dialog; set => SetProperty(ref _dialog, value); }


        public CreateGameVM()
        {
            _teamService = ServiceManager.ServiceManager.Instance.GetRequiredService<ITeamService>();

            ConfirmCommand = new RelayCommand(ConfirmSelection);
            CancelCommand = new RelayCommand(CancelSelection);

            LoadTeams();
        }

        private async void LoadTeams()
        {
            TeamsInSelection =
                new ObservableCollection<TeamInSelection>();
            var teams = await _teamService.GetTeamsAsync();
            foreach (var team in teams)
            {
                var selectionTeam = new TeamInSelection(team);
                TeamsInSelection.Add(selectionTeam);
                selectionTeam.PropertyChanged += SelectionTeam_PropertyChanged;
            }
        }

        private void SelectionTeam_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TeamInSelection.IsChecked))
                CheckAllIsSelectedValues();
        }

        private void CheckAllIsSelectedValues()
        {
            bool? result = TeamsInSelection?.Any(x => x.IsChecked == false);
            if (result == true)
            {
                SetProperty(ref _selectAllIsSelected, false, nameof(SelectAllIsSelected));
            }
            else
            {
                SetProperty(ref _selectAllIsSelected, true, nameof(SelectAllIsSelected));
            }
        }

        private void ConfirmSelection()
        {
            int count = GetSelectedTeams().Count();
            int neededTeamSize = (int)SelectedTournamentSize;

            if (count != neededTeamSize)
                return; // TODO Show Error with correct abstraction

            Dialog.CloseWindow(true);
        }

        private void CancelSelection()
        {
            Dialog.CloseWindow(false);
        }

        private void ChangeIsSelectedOfAllTeams(bool value)
        {
            foreach (var teamInSelection in TeamsInSelection)
            {
                teamInSelection.IsChecked = value;
            }
        }

        public List<Team> GetSelectedTeams()
        {
            var selectedTeams = TeamsInSelection.Where(t => t.IsChecked).Select(t => t.Team).ToList();
            return selectedTeams;
        }
    }
}
