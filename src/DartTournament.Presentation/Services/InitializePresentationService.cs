using DartTournament.Application.UseCases.Player.Services;
using DartTournament.Application.UseCases.Player.Services.Interfaces;
using DartTournament.Domain.Interfaces;
using DartTournament.Infrastructure.JSON.Persistence;
using DartTournament.Presentation.Base.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Presentation.Services
{
    public class InitializePresentationService : IInitializePresentationService
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddSingleton<IDartPlayerRepository, DartPlayerRepository>();
            services.AddSingleton<IPlayerService, PlayerService>();
            services.AddSingleton<IPlayerPresentationService, PlayerPresentationService>();
        }
    }
}
