namespace DartTournament.Logging
{
    /// <summary>
    /// Generic logger interface for type-specific logging
    /// </summary>
    /// <typeparam name="T">The type that the logger is for</typeparam>
    public interface ILogger<T> : ILogger
    {
    }

    /// <summary>
    /// Generic logger implementation that creates type-specific loggers
    /// </summary>
    /// <typeparam name="T">The type that the logger is for</typeparam>
    public class GenericLogger<T> : ILogger<T>
    {
        private readonly ILogger _logger;

        public GenericLogger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void Trace(string message) => _logger.Trace(message);
        public void Trace(Exception exception, string message) => _logger.Trace(exception, message);
        public void Debug(string message) => _logger.Debug(message);
        public void Debug(Exception exception, string message) => _logger.Debug(exception, message);
        public void Info(string message) => _logger.Info(message);
        public void Info(Exception exception, string message) => _logger.Info(exception, message);
        public void Warn(string message) => _logger.Warn(message);
        public void Warn(Exception exception, string message) => _logger.Warn(exception, message);
        public void Error(string message) => _logger.Error(message);
        public void Error(Exception exception, string message) => _logger.Error(exception, message);
        public void Fatal(string message) => _logger.Fatal(message);
        public void Fatal(Exception exception, string message) => _logger.Fatal(exception, message);

        public bool IsTraceEnabled => _logger.IsTraceEnabled;
        public bool IsDebugEnabled => _logger.IsDebugEnabled;
        public bool IsInfoEnabled => _logger.IsInfoEnabled;
        public bool IsWarnEnabled => _logger.IsWarnEnabled;
        public bool IsErrorEnabled => _logger.IsErrorEnabled;
        public bool IsFatalEnabled => _logger.IsFatalEnabled;
    }
}