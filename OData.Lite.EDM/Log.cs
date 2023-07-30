namespace OData.Lite;

using System.Xml.Serialization;

// public static class Log
// {
//     [XmlIgnore]
//     public static Microsoft.Extensions.Logging.ILogger Logger { get; internal set; } = NullLogger.Instance;

//     public static void AddConsole()
//     {
//         AddConsole(options =>
//         {
//             options.IncludeScopes = true;
//             options.SingleLine = true;
//             options.TimestampFormat = "HH:mm:ss ";
//         });
//     }

//     public static void AddConsole(Action<Microsoft.Extensions.Logging.Console.SimpleConsoleFormatterOptions> configure)
//     {
//         var loggerFactory = LoggerFactory.Create(builder =>
//         builder.AddSimpleConsole(configure));

//         Log.Logger = loggerFactory.CreateLogger("log");
//     }
// }