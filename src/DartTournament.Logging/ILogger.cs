namespace DartTournament.Logging
{
    /// <summary>
    /// Abstraction for logging functionality that can be implemented by different logging frameworks
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs a trace message
        /// </summary>
        /// <param name="message">The message to log</param>
        void Trace(string message);

        /// <summary>
        /// Logs a trace message with exception
        /// </summary>
        /// <param name="exception">The exception to log</param>
        /// <param name="message">The message to log</param>
        void Trace(Exception exception, string message);

        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">The message to log</param>
        void Debug(string message);

        /// <summary>
        /// Logs a debug message with exception
        /// </summary>
        /// <param name="exception">The exception to log</param>
        /// <param name="message">The message to log</param>
        void Debug(Exception exception, string message);

        /// <summary>
        /// Logs an info message
        /// </summary>
        /// <param name="message">The message to log</param>
        void Info(string message);

        /// <summary>
        /// Logs an info message with exception
        /// </summary>
        /// <param name="exception">The exception to log</param>
        /// <param name="message">The message to log</param>
        void Info(Exception exception, string message);

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="message">The message to log</param>
        void Warn(string message);

        /// <summary>
        /// Logs a warning message with exception
        /// </summary>
        /// <param name="exception">The exception to log</param>
        /// <param name="message">The message to log</param>
        void Warn(Exception exception, string message);

        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">The message to log</param>
        void Error(string message);

        /// <summary>
        /// Logs an error message with exception
        /// </summary>
        /// <param name="exception">The exception to log</param>
        /// <param name="message">The message to log</param>
        void Error(Exception exception, string message);

        /// <summary>
        /// Logs a fatal message
        /// </summary>
        /// <param name="message">The message to log</param>
        void Fatal(string message);

        /// <summary>
        /// Logs a fatal message with exception
        /// </summary>
        /// <param name="exception">The exception to log</param>
        /// <param name="message">The message to log</param>
        void Fatal(Exception exception, string message);

        /// <summary>
        /// Checks if trace logging is enabled
        /// </summary>
        bool IsTraceEnabled { get; }

        /// <summary>
        /// Checks if debug logging is enabled
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// Checks if info logging is enabled
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        /// Checks if warn logging is enabled
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>
        /// Checks if error logging is enabled
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// Checks if fatal logging is enabled
        /// </summary>
        bool IsFatalEnabled { get; }
    }
}
