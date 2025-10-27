namespace DartTournament.Logging.Examples
{
    /// <summary>
    /// Example class demonstrating how to use the logging abstraction
    /// </summary>
    public class LoggingExample
    {
        private static readonly ILogger _logger = LoggerManager.GetLogger<LoggingExample>();

        public void DemonstrateLogging()
        {
            _logger.Info("Starting logging demonstration");

            try
            {
                _logger.Debug("About to perform some operation");
                
                // Simulate some work
                PerformOperation();
                
                _logger.Info("Operation completed successfully");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Operation failed");
                throw;
            }
        }

        private void PerformOperation()
        {
            _logger.Trace("Entering PerformOperation method");

            // Check if debug logging is enabled before expensive operations
            if (_logger.IsDebugEnabled)
            {
                _logger.Debug($"Current timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            }

            // Simulate potential warning condition
            if (DateTime.Now.Second % 2 == 0)
            {
                _logger.Warn("Even second detected - this might be important");
            }

            _logger.Trace("Exiting PerformOperation method");
        }

        /// <summary>
        /// Example of switching to a different logger implementation
        /// </summary>
        public static void DemonstrateSwitchingLoggers()
        {
            // Save current factory
            var originalFactory = LoggerManager.GetLoggerFactory();

            try
            {
                // Switch to a mock logger factory for testing
                LoggerManager.SetLoggerFactory(new MockLoggerFactory());
                
                var example = new LoggingExample();
                example.DemonstrateLogging();
            }
            finally
            {
                // Restore original factory
                LoggerManager.SetLoggerFactory(originalFactory);
            }
        }
    }

    /// <summary>
    /// Example mock logger factory for testing purposes
    /// </summary>
    public class MockLoggerFactory : ILoggerFactory
    {
        public ILogger CreateLogger(string name) => new MockLogger(name);
        public ILogger CreateLogger(Type type) => new MockLogger(type?.Name ?? "Unknown");
        public ILogger CreateLogger<T>() => new MockLogger(typeof(T).Name);
    }

    /// <summary>
    /// Example mock logger that writes to console instead of using NLog
    /// </summary>
    public class MockLogger : ILogger
    {
        private readonly string _name;

        public MockLogger(string name)
        {
            _name = name;
        }

        public void Trace(string message) => Console.WriteLine($"[TRACE] {_name}: {message}");
        public void Trace(Exception exception, string message) => Console.WriteLine($"[TRACE] {_name}: {message} - {exception.Message}");
        public void Debug(string message) => Console.WriteLine($"[DEBUG] {_name}: {message}");
        public void Debug(Exception exception, string message) => Console.WriteLine($"[DEBUG] {_name}: {message} - {exception.Message}");
        public void Info(string message) => Console.WriteLine($"[INFO] {_name}: {message}");
        public void Info(Exception exception, string message) => Console.WriteLine($"[INFO] {_name}: {message} - {exception.Message}");
        public void Warn(string message) => Console.WriteLine($"[WARN] {_name}: {message}");
        public void Warn(Exception exception, string message) => Console.WriteLine($"[WARN] {_name}: {message} - {exception.Message}");
        public void Error(string message) => Console.WriteLine($"[ERROR] {_name}: {message}");
        public void Error(Exception exception, string message) => Console.WriteLine($"[ERROR] {_name}: {message} - {exception.Message}");
        public void Fatal(string message) => Console.WriteLine($"[FATAL] {_name}: {message}");
        public void Fatal(Exception exception, string message) => Console.WriteLine($"[FATAL] {_name}: {message} - {exception.Message}");

        public bool IsTraceEnabled => true;
        public bool IsDebugEnabled => true;
        public bool IsInfoEnabled => true;
        public bool IsWarnEnabled => true;
        public bool IsErrorEnabled => true;
        public bool IsFatalEnabled => true;
    }
}