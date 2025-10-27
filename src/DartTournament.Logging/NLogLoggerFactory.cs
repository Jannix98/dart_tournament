namespace DartTournament.Logging
{
    /// <summary>
    /// NLog implementation of the ILoggerFactory interface
    /// </summary>
    public class NLogLoggerFactory : ILoggerFactory
    {
        public ILogger CreateLogger(string name)
        {
            return new NLogLogger(name);
        }

        public ILogger CreateLogger(Type type)
        {
            return new NLogLogger(type);
        }

        public ILogger CreateLogger<T>()
        {
            return new NLogLogger(typeof(T));
        }
    }
}