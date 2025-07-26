using LogLevel = NLog.LogLevel;

namespace Config.Infraestructure;

public static class LoggerManager
{
    public static void Setup(string logFolder, string logFileName)
    {
        Directory.CreateDirectory(logFolder);
        var config = new LoggingConfiguration();
        var fullPath = Path.Combine(logFolder, $"{logFileName}.log");
        var logfile = new FileTarget("logfile")
        {
            FileName = fullPath,
            Layout = "${longdate} ${level} ${message}  ${exception}"
        };
        var logconsole = new ConsoleTarget("logconsole")
        {
            Layout = "${longdate} ${level} ${message}  ${exception}"
        };
        config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
        config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
        LogManager.Configuration = config;
    }

    public static void Shutdown()
    {
        LogManager.Shutdown();
    }
}