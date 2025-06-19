using DartTournament.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DartTournament.Application.UseCases.Player.Services;
using DartTournament.Application.UseCases.Player.Services.Interfaces;
using DartTournament.Infrastructure.JSON.Persistence;
using DartTournament.WPF.Controls.PlayerOverview;
using DartTournament.WPF.Dialogs.AddTeam;
using DartTournament.WPF.Dialogs.DialogManagement;
using DartTournament.WPF.Navigator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IDartPlayerRepository, PlayerRepository>();
            services.AddSingleton<IPlayerService, PlayerService>();
            services.AddSingleton<IDialogManager, DialogManager>();
            // TODO: Dialog Factory to pass the "Application.Current.MainWindow"
            services.AddTransient<IAddPlayerView, AddTeamView>();
            services.AddTransient<IDialogOwner, DialogOwner>();
        }

        public T GetRequiredService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }

    }
}
