using DartTournament.Application.UseCases.Teams.Services.Interfaces;
using DartTournament.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Presentation.Controllers
{
    [ApiController]
    [Route("api/teams")]
    internal class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] string name)
        {
            var teamId = await _teamService.CreateTeamAsync(name);
            return CreatedAtAction(nameof(GetAllTeams), new { id = teamId }, teamId);
        }

        [HttpGet]
        public async Task<ActionResult<List<Team>>> GetAllTeams()
        {
            var teams = await _teamService.GetTeamsAsync();
            return Ok(teams);
        }
    }
}
