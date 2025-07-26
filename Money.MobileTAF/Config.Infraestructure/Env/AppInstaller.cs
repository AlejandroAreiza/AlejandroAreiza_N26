namespace Config.Infraestructure.Env;
public static class AppInstaller
{
    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    private static readonly string _scriptRoot = Path.Combine(
        Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName 
        ?? Directory.GetCurrentDirectory(),
        "Config.Infraestructure", "Env", "scripts"
    );

    public static void Install()
    {
        _logger.Info("Installing app...");
        RunScript("install_monefy.sh");
        _logger.Info("Installation done.");
    }

    public static void Uninstall()
    {
        _logger.Info("Uninstalling app...");
        RunScript("uninstall_monefy.sh");
        _logger.Info("Uninstallation done.");
    }

    private static void RunScript(string scriptName)
    {
        var fullPath = Path.Combine(_scriptRoot, scriptName);

        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"Script not found: {fullPath}");

        var psi = new ProcessStartInfo
        {
            FileName = "bash",
            Arguments = fullPath,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(psi);
        if (process == null)
            throw new Exception($"Failed to start: bash {fullPath}");

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        process.WaitForExit();
        _logger.Info(output);

        if (!string.IsNullOrWhiteSpace(error))
            _logger.Warn($"Script error output: {error}");

        if (process.ExitCode != 0)
            throw new Exception($"Script failed with code {process.ExitCode}: {scriptName}");
    }
}