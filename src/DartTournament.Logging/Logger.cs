using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Logging
{
    public class Logger : ILogger
    {
        NLog.Logger _logger;

        public Logger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void InitLogger(string configFilePath)
        {
            var config = new NLog.Config.LoggingConfiguration();
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Nlog.config");
            if (File.Exists(filePath))
            {
                config = new NLog.Config.XmlLoggingConfiguration(filePath);
            }
            else
            {
                // If no config file found, setup a basic configuration
                var logconsole = new NLog.Targets.ConsoleTarget("logconsole");
                config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            }

            // Apply config           
            NLog.LogManager.Configuration = config; 
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }

        public void LogError(string message, Exception ex)
        {
            _logger.Error(ex, message);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogWarning(string message)
        {
            _logger.Warn(message);
        }
    }
}
