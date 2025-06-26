using DartTournament.Application.DTO.Player;
using DartTournament.Application.UseCases.Player.Services.Interfaces;
using DartTournament.Presentation.Base.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DartTournament.Presentation.Services
{
    public class PlayerPresentationService : IPlayerPresentationService
    {
        private readonly IPlayerService _playerService;

        public PlayerPresentationService(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public Task<DartPlayerGetDto> CreatePlayerAsync(DartPlayerInsertDto insertDto)
            => _playerService.CreatePlayerAsync(insertDto);

        public Task UpdatePlayerAsync(DartPlayerUpdateDto updateDto)
            => _playerService.UpdatePlayerAsync(updateDto);

        public Task<List<DartPlayerGetDto>> GetPlayerAsync()
            => _playerService.GetPlayerAsync();

        public async Task DeletePlayerAsync(Guid id)
        {
            // Call the underlying service/repository to delete the player by Id
            await _playerService.DeleteAsync(id);
        }
    }
}