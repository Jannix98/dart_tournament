using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartTournament.Application.DTO.Player;
using DartTournament.Application.UseCases.Player.Services;
using DartTournament.Domain.Entities;
using DartTournament.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DartTournament.Application.UnitTest
{
    [TestClass]
    public class PlayerServiceTests
    {
        private Mock<IDartPlayerRepository> _playerRepositoryMock = null!;
        private PlayerService _playerService = null!;

        [TestInitialize]
        public void Setup()
        {
            _playerRepositoryMock = new Mock<IDartPlayerRepository>();
            _playerService = new PlayerService(_playerRepositoryMock.Object);
        }

        [TestMethod]
        public async Task CreatePlayerAsync_Should_Map_And_Save_Entity()
        {
            // Arrange
            var insertDto = new DartPlayerInsertDto { Name = "Test Player" };
            DartPlayer? savedPlayer = null;
            _playerRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<DartPlayer>()))
                .Callback<DartPlayer>(p => savedPlayer = p)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _playerService.CreatePlayerAsync(insertDto);

            // Assert
            Assert.IsNotNull(savedPlayer);
            Assert.AreEqual(insertDto.Name, savedPlayer!.Name);
            Assert.AreEqual(savedPlayer.Id, result.Id);
            Assert.AreEqual(savedPlayer.Name, result.Name);
            _playerRepositoryMock.Verify(r => r.AddAsync(It.IsAny<DartPlayer>()), Times.Once);
        }

        [TestMethod]
        public async Task GetPlayerAsync_Should_Map_Entities_To_Dtos()
        {
            // Arrange
            var players = new List<DartPlayer>
            {
                new DartPlayer("Alice"),
                new DartPlayer("Bob")
            };
            _playerRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(players);

            // Act
            var result = await _playerService.GetPlayerAsync();

            // Assert
            Assert.AreEqual(players.Count, result.Count);
            foreach (var dto in result)
            {
                var entity = players.FirstOrDefault(p => p.Id == dto.Id);
                Assert.IsNotNull(entity);
                Assert.AreEqual(entity!.Name, dto.Name);
            }
        }

        [TestMethod]
        public async Task UpdatePlayerAsync_Should_Not_Throw_If_Not_Found()
        {
            // Arrange
            var updateDto = new DartPlayerUpdateDto { Id = Guid.NewGuid(), Name = "Doesn't Matter" };
            _playerRepositoryMock.Setup(r => r.GetByIdAsync(updateDto.Id)).ReturnsAsync((DartPlayer?)null);

            // Act & Assert
            await _playerService.UpdatePlayerAsync(updateDto);
            _playerRepositoryMock.Verify(r => r.Update(It.IsAny<DartPlayer>()), Times.Never);
        }
    }
}