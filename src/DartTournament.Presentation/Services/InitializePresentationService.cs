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
        private ServiceProvider _serviceProvider;

        public void Initialize(object services)
        {
            //var services = new ServiceCollection();
            if (!(services is ServiceCollection serviceCollection))
                return; // TODO can be better

            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IDartPlayerRepository, DartPlayerRepository>();
            services.AddSingleton<IPlayerService, PlayerService>();

            // Register PlayerPresentationService
            //services.AddSingleton<IPlayerPresentationService, PlayerPresentationService>();
        }
    }
}
