using DartTournament.Logging;
using DartTournament.Presentation.Base.Services;
using DartTournament.WPF.SM;
using System.Configuration;
using System.Data;
using System.Windows;

namespace TestDemo
{
    /// <summary>  
    /// Interaction logic for App.xaml  
    /// </summary>  
    public partial class App : Application
    {
        DartTournament.Logging.ILogger _logger;
        public App()
        {
            // Initialize NLog configuration before any logging occurs
            InitializeNLogConfiguration();
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void InitializeNLogConfiguration()
        {
            // Configure NLog to use the NLog.config file in the application directory
            var configFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NLog.config");
            if (System.IO.File.Exists(configFile))
            {
                NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(configFile);
            }
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            _logger?.Error(e.Exception, "An unhandled exception occurred");
            MessageBox.Show($"An unhandled exception occurred: {e.Exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _logger = ServiceManager.Instance.GetRequiredService<DartTournament.Logging.ILogger>();
            _logger.Info("--- Application started ---");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _logger = ServiceManager.Instance.GetRequiredService<DartTournament.Logging.ILogger>();
            _logger?.Info("--- Application Exited ---");
        }
    }

}
