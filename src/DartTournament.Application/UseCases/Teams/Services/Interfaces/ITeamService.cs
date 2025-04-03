using DartTournament.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Application.UseCases.Teams.Services.Interfaces
{
    public interface ITeamService
    {
        Task<Team> CreateTeamAsync(string name);
        Task<List<Team>> GetTeamsAsync();
    }
}
