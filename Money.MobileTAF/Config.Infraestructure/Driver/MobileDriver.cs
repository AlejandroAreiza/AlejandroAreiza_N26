namespace Config.Infraestructure.Driver;

public class MobileDriver : IMobileDriver
{
    private readonly AppiumDriver _driver;
    private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
    private AppManager appManager;
    private TestLaunchConfig _testLaunchConfigData;

    public MobileDriver(TestLaunchConfig testLaunchConfigData)
    {
        _testLaunchConfigData = testLaunchConfigData;
        _driver = new DriverFactory().CreateDriver(testLaunchConfigData.DesiredCapabilities);
        appManager = new AppManager(testLaunchConfigData.AppConfig);
    }
    public AppiumDriver Driver => _driver;

    public IMobileElement MobileElement(string name, By by) => new MobileElement(name, by, _driver);
    public IMobileElements MobileElements(string name, By by) => new MobileElements(name, by, _driver);

    public void InstallApp()
    {
        try
        {
            appManager.Install();
            _logger.Info("App Installed");
        }
        catch (Exception ex)
        {
            _logger.Error($"Installing App failed with message {ex.Message}");
        }
    }
    public void Uninstall()
    {
        try
        {
            appManager.Uninstall();
            _logger.Info("App Uninstalled");
        }
        catch (Exception ex)
        {
            _logger.Error($"Uninstalling App failed with message {ex.Message}");
        }
    }

    public void TerminateApp()
    {
        try
        {
            _driver.TerminateApp(appManager.appConfig.AppName);
            _logger.Info("Closing app...");
        }
        catch (Exception ex)
        {
            _logger.Error($"Error Closing app with message: {ex.Message}");
        }

    }

    public void Dispose()
    {
        _logger.Info("Disposing driver...");
        _driver?.Quit();
        _driver?.Dispose();
    }

    public void GoToScreen(string screenName)
    {
        switch (_testLaunchConfigData.DesiredCapabilities.PlatformName)
        {
            case DTOs.PlatformType.Android:
                GoToAndroidScreen(screenName);
                break;
            case DTOs.PlatformType.iOS:
                throw new NotImplementedException("iOS direct screen navigation not supported yet");
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void GoToAndroidScreen(string activityName)
    {
        var fullComponent = $"{_testLaunchConfigData.DesiredCapabilities.AppPackage}/{activityName}";
        var args = new Dictionary<string, object>
        {
            ["command"] = "am",
            ["args"] = new List<string> { "start", "-n", fullComponent }
        };
        try
        {
            _driver.ExecuteScript("mobile: shell", args);
            _logger.Info($"{args["appActivity"]} Screen loaded");
        }
        catch (Exception ex)
        {
            _logger.Error($"Failed loading Screen {args["appActivity"]} with message: {ex.Message}");
            throw new ArgumentException($"Failed loading Screen {args["appActivity"]} with message: {ex.Message}");
        }
    }
}
