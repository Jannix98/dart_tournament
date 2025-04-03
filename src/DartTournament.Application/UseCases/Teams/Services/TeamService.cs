using DartTournament.Application.UseCases.Teams.Services.Interfaces;
using DartTournament.Domain.Entities;
using DartTournament.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Application.UseCases.Teams.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<Team> CreateTeamAsync(string name)
        {
            var team = new Team(name);
            await _teamRepository.AddAsync(team);
            return team;
        }

        public async Task<List<Team>> GetTeamsAsync()
        {
            return await _teamRepository.GetAllAsync();
        }
    }
}
