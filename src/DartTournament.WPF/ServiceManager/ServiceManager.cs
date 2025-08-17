using DartTournament.Application.UseCases.Game.Services;
using DartTournament.Application.UseCases.Game.Services.Interfaces;
using DartTournament.Application.UseCases.Match.Services;
using DartTournament.Application.UseCases.Match.Services.Interfaces;
using DartTournament.Application.UseCases.Player.Services;
using DartTournament.Application.UseCases.Player.Services.Interfaces;
using DartTournament.Domain.Interfaces;
using DartTournament.Infrastructure.JSON.Persistence;
using DartTournament.Presentation.Base.Services;
using DartTournament.Presentation.Services;
using DartTournament.WPF.Controls.PlayerOverview;
using DartTournament.WPF.Controls.Toolbar;
using DartTournament.WPF.Dialogs.AddPlayer;
using DartTournament.WPF.Dialogs.Base;
using DartTournament.WPF.Dialogs.SelectWinner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DartTournament.WPF.SM
{
    internal interface IServiceManager
    {
        T GetRequiredService<T>();
    }

    public class ServiceManager : IServiceManager
    {
        private static readonly Lazy<ServiceManager> _instance = new Lazy<ServiceManager>(() => new ServiceManager());

        public static ServiceManager Instance => _instance.Value;

        private readonly ServiceProvider _serviceProvider;

        private ServiceManager()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            // Let the presentation layer register its own services
            var initService = new InitializePresentationService();
            initService.Initialize(services);

            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            // Repositories
            services.AddSingleton<IDartPlayerRepository, DartPlayerRepository>();
            services.AddSingleton<IMatchRepository, MatchRepository>();
            services.AddSingleton<IRoundRepository, RoundRepository>();
            services.AddSingleton<IGameRepository, GameRepository>();
            services.AddSingleton<IGameParentRepository, GameParentRepository>();

            // Application Services
            services.AddSingleton<IPlayerService, PlayerService>();
            services.AddSingleton<IMatchService, MatchService>();
            services.AddSingleton<IGameService, GameService>();

            // Presentation Services
            services.AddSingleton<IPlayerPresentationService, PlayerPresentationService>();
            services.AddSingleton<IMatchPresentationService, MatchPresentationService>();
            services.AddSingleton<IGamePresentationService, GamePresentationService>();

            // Dialog services
            services.AddSingleton<IDialogManager, DialogManager>();
            services.AddTransient<IAddPlayerView, AddPlayerView>();
            services.AddTransient<ISelectWinnerDialog, SelectWinnerDialog>();
            services.AddTransient<IDialogOwner, DialogOwner>();

            // Utilities
            services.AddSingleton<GameCreator>();
            services.AddSingleton<GameLoader>();
        }

        public T GetRequiredService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }

    }
}
