namespace Config.Infraestructure;

public class AppManager
{
    private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
    private string _solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName
    ?? Directory.GetCurrentDirectory();
    private string _scriptRoot => Path.Combine(_solutionRoot, "Config.Infraestructure", "Env", "scripts");
    public AppConfig appConfig { get; }

    public AppManager(AppConfig appConfig)
    {
        this.appConfig = appConfig;
    }
    public void Install()
    {
        var apkFolder = Path.Combine(_solutionRoot, "apks");
        RunScript("install_apks.sh", apkFolder);
    }

    public void Uninstall()
    {
        RunScript("uninstall_apks.sh", appConfig.AppName);
    }
    private void RunScript(string scriptName, string argument)
    {
        var fullPath = Path.Combine(_scriptRoot, scriptName);

        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"Script not found: {fullPath}");

        var psi = new ProcessStartInfo
        {
            FileName = "bash",
            Arguments = $"\"{fullPath}\" \"{argument}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(psi);
        if (process == null)
            throw new Exception($"Failed to start: bash {fullPath}");

        string line;
        while ((line = process.StandardOutput.ReadLine()) != null)
        {
            if (line.Trim() == "Success")
                continue;
            _logger.Info($"{line}");
        }

        while ((line = process.StandardError.ReadLine()) != null)
        {
            _logger.Error($"{line}");
        }

        process.WaitForExit();

        if (process.ExitCode != 0)
            throw new Exception($"Script failed with code {process.ExitCode}: {scriptName}");
    }
}