namespace Config.Infrastructure.Logging;

public static class LoggerManager
{
    public static void Setup(string configFilePath, string logFolder,string logFileName)
    {
        if (!File.Exists(configFilePath))
            throw new FileNotFoundException("NLog configuration file not found.", configFilePath);

        var config = new XmlLoggingConfiguration(configFilePath);

        var fileTarget = config.FindTargetByName<FileTarget>("logfile");
        if (fileTarget == null)
            throw new Exception("Target 'logfile' not found in nlog.config");

        fileTarget.FileName = $"{logFolder}/{logFileName}.log";

        LogManager.Configuration = config;
    }

    public static void Shutdown()
    {
        LogManager.Shutdown();
    }
}

