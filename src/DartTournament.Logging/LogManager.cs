namespace DartTournament.Logging
{
    /// <summary>
    /// Static helper class for easy logger creation and configuration
    /// </summary>
    public static class LoggerManager
    {
        private static ILoggerFactory _loggerFactory = new NLogLoggerFactory();

        /// <summary>
        /// Sets the logger factory to use for creating loggers
        /// </summary>
        /// <param name="factory">The logger factory to use</param>
        public static void SetLoggerFactory(ILoggerFactory factory)
        {
            _loggerFactory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the current logger factory
        /// </summary>
        /// <returns>The current logger factory</returns>
        public static ILoggerFactory GetLoggerFactory()
        {
            return _loggerFactory;
        }

        /// <summary>
        /// Creates a logger with the specified name
        /// </summary>
        /// <param name="name">The name of the logger</param>
        /// <returns>A new logger instance</returns>
        public static ILogger GetLogger(string name)
        {
            return _loggerFactory.CreateLogger(name);
        }

        /// <summary>
        /// Creates a logger for the specified type
        /// </summary>
        /// <param name="type">The type to use for the logger name</param>
        /// <returns>A new logger instance</returns>
        public static ILogger GetLogger(Type type)
        {
            return _loggerFactory.CreateLogger(type);
        }

        /// <summary>
        /// Creates a logger for the specified generic type
        /// </summary>
        /// <typeparam name="T">The type to use for the logger name</typeparam>
        /// <returns>A new logger instance</returns>
        public static ILogger GetLogger<T>()
        {
            return _loggerFactory.CreateLogger<T>();
        }

        /// <summary>
        /// Creates a logger for the calling class
        /// </summary>
        /// <returns>A new logger instance</returns>
        public static ILogger GetCurrentClassLogger()
        {
            var frame = new System.Diagnostics.StackFrame(1, false);
            var method = frame.GetMethod();
            var type = method?.DeclaringType;
            
            if (type != null)
            {
                return _loggerFactory.CreateLogger(type);
            }
            
            return _loggerFactory.CreateLogger("Unknown");
        }
    }
}