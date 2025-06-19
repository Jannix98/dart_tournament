using DartTournament.Application.UseCases.Player.Services;
using DartTournament.Domain.Entities;
using DartTournament.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.UnitTest.Services
{
    internal class PlayerServiceTests
    {
        [TestMethod]
        public async Task CreatePlayer_ShouldReturnNewTeamId()
        {
            var mockRepo = new Mock<IDartPlayerRepository>();
            var service = new PlayerService(mockRepo.Object);

            var result = await service.CreatePlayerAsync("Test Team");

            Assert.AreNotEqual(Guid.Empty, result.Id);
            mockRepo.Verify(repo => repo.AddAsync(It.IsAny<DartPlayer>()), Times.Once);
        }

        [TestMethod]
        public async Task GetPlayer_ShouldReturnTeams()
        {
            var mockRepo = new Mock<IDartPlayerRepository>();
            var players = new List<DartPlayer> { new DartPlayer("Team 1"), new DartPlayer("Team 2") };

            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(players);

            var service = new PlayerService(mockRepo.Object);
            var result = await service.GetPlayerAsync();

            Assert.AreEqual(2, result.Count);
        }
    }
}
