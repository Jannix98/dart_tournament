using DartTournament.Logging;
using DartTournament.Presentation.Base.Services;
using DartTournament.WPF.SM;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace TestDemo
{
    /// <summary>  
    /// Interaction logic for App.xaml  
    /// </summary>  
    public partial class App : Application
    {
        private ILogger _logger;

        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            _logger.LogError("An unhandled exception occurred.", e.Exception);
            MessageBox.Show($"An unhandled exception occurred: {e.Exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Nlog.config");
            _logger = ServiceManager.Instance.GetRequiredService<ILogger>();
            _logger.InitLogger(filePath);
            _logger.LogInfo("--- Application started ---");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _logger.LogInfo("--- Application exited ---");
        }
    }

}
