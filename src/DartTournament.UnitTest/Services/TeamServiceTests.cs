using DartTournament.Application.UseCases.Teams.Services;
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
    internal class TeamServiceTests
    {
        [TestMethod]
        public async Task CreateTeam_ShouldReturnNewTeamId()
        {
            var mockRepo = new Mock<ITeamRepository>();
            var service = new TeamService(mockRepo.Object);

            var result = await service.CreateTeamAsync("Test Team");

            Assert.AreNotEqual(Guid.Empty, result.Id);
            mockRepo.Verify(repo => repo.AddAsync(It.IsAny<Team>()), Times.Once);
        }

        [TestMethod]
        public async Task GetTeams_ShouldReturnTeams()
        {
            var mockRepo = new Mock<ITeamRepository>();
            var teams = new List<Team> { new Team("Team 1"), new Team("Team 2") };

            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(teams);

            var service = new TeamService(mockRepo.Object);
            var result = await service.GetTeamsAsync();

            Assert.AreEqual(2, result.Count);
        }
    }
}
