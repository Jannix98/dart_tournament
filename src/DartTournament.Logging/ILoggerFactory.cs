namespace DartTournament.Logging
{
    /// <summary>
    /// Factory interface for creating logger instances
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Creates a logger with the specified name
        /// </summary>
        /// <param name="name">The name of the logger</param>
        /// <returns>A new logger instance</returns>
        ILogger CreateLogger(string name);

        /// <summary>
        /// Creates a logger for the specified type
        /// </summary>
        /// <param name="type">The type to use for the logger name</param>
        /// <returns>A new logger instance</returns>
        ILogger CreateLogger(Type type);

        /// <summary>
        /// Creates a logger for the specified generic type
        /// </summary>
        /// <typeparam name="T">The type to use for the logger name</typeparam>
        /// <returns>A new logger instance</returns>
        ILogger CreateLogger<T>();
    }
}