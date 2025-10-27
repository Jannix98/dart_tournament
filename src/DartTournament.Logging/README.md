# DartTournament.Logging

This library provides an abstraction layer for logging functionality using NLog as the default implementation. The abstraction allows for easy switching between different logging frameworks.

## Features

- **ILogger Interface**: Clean abstraction that can be implemented by any logging framework
- **NLog Implementation**: Default implementation using NLog 6.0.5
- **Factory Pattern**: Easy creation of logger instances
- **Dependency Injection Support**: Full support for DI containers with generic logger support
- **Swappable Implementation**: Easy to switch to different logging frameworks

## Quick Start

### Basic Usage
using DartTournament.Logging;

// Create a logger for the current class
var logger = LoggerManager.GetCurrentClassLogger();

// Log messages at different levels
logger.Info("Application started");
logger.Debug("Debug information");
logger.Warn("This is a warning");
logger.Error("An error occurred");

// Log with exceptions
try
{
    // Some code that might throw
}
catch (Exception ex)
{
    logger.Error(ex, "An error occurred while processing");
}
### Dependency Injection Usage

When using with DI containers (like Microsoft.Extensions.DependencyInjection):
// Register logging services
services.AddSingleton<ILoggerFactory, NLogLoggerFactory>();
services.AddTransient<ILogger>(provider => 
{
    var factory = provider.GetRequiredService<ILoggerFactory>();
    return factory.CreateLogger("ApplicationDefault");
});
services.AddTransient(typeof(ILogger<>), typeof(GenericLogger<>));

// Use in your classes
public class MyService
{
    private readonly ILogger<MyService> _logger;

    public MyService(ILogger<MyService> logger)
    {
        _logger = logger;
    }

    public void DoSomething()
    {
        _logger.Info("Doing something important");
    }
}

// Or use non-generic logger
public class AnotherService
{
    private readonly ILogger _logger;

    public AnotherService(ILogger logger)
    {
        _logger = logger;
    }
}
### Creating Loggers
// Create logger by name
var logger1 = LoggerManager.GetLogger("MyLogger");

// Create logger by type
var logger2 = LoggerManager.GetLogger(typeof(MyClass));

// Create logger using generic type
var logger3 = LoggerManager.GetLogger<MyClass>();

// Create logger for current class (most common)
var logger4 = LoggerManager.GetCurrentClassLogger();
### Using the Factory Pattern
// Get the current factory
ILoggerFactory factory = LoggerManager.GetLoggerFactory();

// Create loggers using the factory
ILogger logger = factory.CreateLogger("MyLogger");
### Switching Logging Frameworks

To switch to a different logging framework, implement the `ILogger` and `ILoggerFactory` interfaces:
// Example: Custom logging implementation
public class CustomLogger : ILogger
{
    // Implement all ILogger methods
}

public class CustomLoggerFactory : ILoggerFactory
{
    public ILogger CreateLogger(string name) => new CustomLogger(name);
    public ILogger CreateLogger(Type type) => new CustomLogger(type);
    public ILogger CreateLogger<T>() => new CustomLogger(typeof(T));
}

// Switch to your custom implementation
LoggerManager.SetLoggerFactory(new CustomLoggerFactory());

// Or in DI registration
services.AddSingleton<ILoggerFactory, CustomLoggerFactory>();
## Log Levels

The following log levels are supported:

- **Trace**: Very detailed logs, typically only of interest when diagnosing problems
- **Debug**: Internal system events that aren't necessarily observable from the outside
- **Info**: General information about application execution
- **Warn**: Potentially harmful situations that don't prevent the application from continuing
- **Error**: Error events that might still allow the application to continue
- **Fatal**: Very severe error events that might cause the application to terminate

## Performance Considerations

Check if logging is enabled before creating expensive log messages:
if (logger.IsDebugEnabled)
{
    logger.Debug($"Expensive computation result: {ExpensiveOperation()}");
}
## NLog Configuration

Create an `NLog.config` file in your application root to configure NLog behavior:
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

  <targets>
    <target xsi:type="File" 
            name="fileTarget"
            fileName="logs/darttournament-${shortdate}.log"
            layout="${longdate} ${uppercase:${level}} ${logger} ${message} ${exception:format=tostring}" />
    
    <target xsi:type="Console" 
            name="consoleTarget"
            layout="${time} [${uppercase:${level}}] ${logger}: ${message}" />
  </targets>

  <rules>
    <logger name="*" minlevel="Info" writeTo="consoleTarget" />
    <logger name="*" minlevel="Debug" writeTo="fileTarget" />
  </rules>
</nlog>
## Dependencies

- .NET 8.0
- NLog 6.0.5