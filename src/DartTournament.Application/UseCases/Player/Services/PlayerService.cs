using DartTournament.Application.DTO.Player;
using DartTournament.Application.UseCases.Player.Mappers;
using DartTournament.Application.UseCases.Player.Services.Interfaces;
using DartTournament.Domain.Entities;
using DartTournament.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DartTournament.Application.UseCases.Player.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IDartPlayerRepository _playerRepository;

        public PlayerService(IDartPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<DartPlayerGetDto> CreatePlayerAsync(DartPlayerInsertDto insertDto)
        {
            var player = DartPlayerMapper.ToEntity(insertDto);
            await _playerRepository.AddAsync(player);
            return DartPlayerMapper.ToGetDto(player);
        }

        public async Task<List<DartPlayerGetDto>> GetPlayerAsync()
        {
            var players = await _playerRepository.GetAllAsync();
            return players.Select(DartPlayerMapper.ToGetDto).ToList();
        }

        public async Task<DartPlayerGetDto> GetPlayerByIdAsync(Guid id)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            return player != null ? DartPlayerMapper.ToGetDto(player) : null;
        }

        public async Task UpdatePlayerAsync(DartPlayerUpdateDto updateDto)
        {
            var player = await _playerRepository.GetByIdAsync(updateDto.Id);
            if (player != null)
            {
                // TODO: with an SQL / Entity Framework, this would not work
                var convertedPlayer = DartPlayerMapper.UpdateEntity(updateDto);
                await _playerRepository.Update(convertedPlayer);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            // Call repository to delete
            await _playerRepository.DeleteAsync(id);
        }
    }
}