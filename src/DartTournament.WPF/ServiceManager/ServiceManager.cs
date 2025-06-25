using DartTournament.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DartTournament.Infrastructure.JSON.Persistence;
using DartTournament.WPF.Controls.PlayerOverview;
using DartTournament.WPF.Dialogs.AddPlayer;
using DartTournament.WPF.Dialogs.DialogManagement;
using DartTournament.WPF.Navigator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using DartTournament.Presentation.Base.Services;
using DartTournament.Presentation.Services;

namespace DartTournament.WPF.ServiceManager
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
            services.AddSingleton<IDartPlayerRepository, DartPlayerRepository>();
            services.AddSingleton<IDialogManager, DialogManager>();
            // TODO: Dialog Factory to pass the "Application.Current.MainWindow"
            services.AddTransient<IAddPlayerView, AddPlayerView>();
            services.AddTransient<IDialogOwner, DialogOwner>();

            // Register PlayerPresentationService
            //services.AddSingleton<IInitializePresentationService, InitializePresentationService>();
            //services.AddSingleton<IPlayerPresentationService, PlayerPresentationService>();
        }

        public T GetRequiredService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }

    }
}
