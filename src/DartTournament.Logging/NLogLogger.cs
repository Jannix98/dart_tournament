using NLog;

namespace DartTournament.Logging
{
    /// <summary>
    /// NLog implementation of the ILogger interface
    /// </summary>
    public class NLogLogger : ILogger
    {
        private readonly Logger _nlogLogger;

        /// <summary>
        /// Initializes a new instance of NLogLogger with the specified logger name
        /// </summary>
        /// <param name="name">The name of the logger</param>
        public NLogLogger(string name)
        {
            _nlogLogger = NLog.LogManager.GetLogger(name);
        }

        /// <summary>
        /// Initializes a new instance of NLogLogger with the specified type
        /// </summary>
        /// <param name="type">The type to use for the logger name</param>
        public NLogLogger(Type type)
        {
            _nlogLogger = NLog.LogManager.GetLogger(type.FullName);
        }

        /// <summary>
        /// Initializes a new instance of NLogLogger with the current class name
        /// </summary>
        public NLogLogger()
        {
            _nlogLogger = NLog.LogManager.GetCurrentClassLogger();
        }

        public void Trace(string message)
        {
            _nlogLogger.Trace(message);
        }

        public void Trace(Exception exception, string message)
        {
            _nlogLogger.Trace(exception, message);
        }

        public void Debug(string message)
        {
            _nlogLogger.Debug(message);
        }

        public void Debug(Exception exception, string message)
        {
            _nlogLogger.Debug(exception, message);
        }

        public void Info(string message)
        {
            _nlogLogger.Info(message);
        }

        public void Info(Exception exception, string message)
        {
            _nlogLogger.Info(exception, message);
        }

        public void Warn(string message)
        {
            _nlogLogger.Warn(message);
        }

        public void Warn(Exception exception, string message)
        {
            _nlogLogger.Warn(exception, message);
        }

        public void Error(string message)
        {
            _nlogLogger.Error(message);
        }

        public void Error(Exception exception, string message)
        {
            _nlogLogger.Error(exception, message);
        }

        public void Fatal(string message)
        {
            _nlogLogger.Fatal(message);
        }

        public void Fatal(Exception exception, string message)
        {
            _nlogLogger.Fatal(exception, message);
        }

        public bool IsTraceEnabled => _nlogLogger.IsTraceEnabled;
        public bool IsDebugEnabled => _nlogLogger.IsDebugEnabled;
        public bool IsInfoEnabled => _nlogLogger.IsInfoEnabled;
        public bool IsWarnEnabled => _nlogLogger.IsWarnEnabled;
        public bool IsErrorEnabled => _nlogLogger.IsErrorEnabled;
        public bool IsFatalEnabled => _nlogLogger.IsFatalEnabled;
    }
}