using Microsoft.Extensions.Logging;
namespace OData.Lite;
using Microsoft.Extensions.Logging.Abstractions;

public static class Log
{
    public static Microsoft.Extensions.Logging.ILogger Logger = NullLogger.Instance;

    public static void AddConsole()
    {
        AddConsole(options =>
        {
            options.IncludeScopes = true;
            options.SingleLine = true;
            options.TimestampFormat = "HH:mm:ss ";
        });
    }

    public static void AddConsole(Action<Microsoft.Extensions.Logging.Console.SimpleConsoleFormatterOptions> configure)
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        builder.AddSimpleConsole(configure));

        Log.Logger = loggerFactory.CreateLogger("log");
    }
}