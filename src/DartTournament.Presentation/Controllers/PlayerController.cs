using DartTournament.Application.UseCases.Player.Services.Interfaces;
using DartTournament.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Presentation.Controllers
{
    // TODO: what does this class do? 
    [ApiController]
    [Route("api/player")]
    internal class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService teamService)
        {
            _playerService = teamService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody] string name)
        {
            var playerId = await _playerService.CreatePlayerAsync(name);
            return CreatedAtAction(nameof(GetAllPlayers), new { id = playerId }, playerId);
        }

        [HttpGet]
        public async Task<ActionResult<List<DartPlayer>>> GetAllPlayers()
        {
            var players = await _playerService.GetPlayerAsync();
            return Ok(players);
        }
    }
}
