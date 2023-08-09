using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
namespace OData.Lite;

internal class Internal { }

public static class Logging
{
    public static void InitConsole()
    {
        Init(builder => builder.AddConsole());
    }

    public static void Init(Action<ILoggingBuilder> config)
    {
        var loggerFactory = LoggerFactory.Create(config);

        Log = loggerFactory.CreateLogger<Internal>();
    }

    public static ILogger Log { get; private set; } = NullLogger<Internal>.Instance;
}
