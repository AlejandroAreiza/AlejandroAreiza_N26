namespace Config.Infraestructure.DriverFactory;

public class DriverFactory
{
    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    public static AppiumDriver CreateDriver(AppiumConfig config)
    {
        if (config == null)
            throw new ArgumentNullException(nameof(config), "AppiumConfig cannot be null.");

        _logger.Info($"Creating Appium driver for platform: {config.PlatformName}");

        var options = new AppiumOptions
        {
            PlatformName = config.PlatformName.ToString(),
            DeviceName = config.DeviceName,
            AutomationName = config.AutomationName
            // You can add PlatformVersion and App if you add those props in DTO
        };

        // Add additional capabilities dynamically if they are set
        if (!string.IsNullOrEmpty(config.AppPackage))
            options.AddAdditionalAppiumOption("appPackage", config.AppPackage);

        if (!string.IsNullOrEmpty(config.AppActivity))
            options.AddAdditionalAppiumOption("appActivity", config.AppActivity);

        if (config.NoReset)
            options.AddAdditionalAppiumOption("noReset", config.NoReset);

        if (config.NewCommandTimeout > 0)
            options.AddAdditionalAppiumOption("newCommandTimeout", config.NewCommandTimeout);

        options.AddAdditionalAppiumOption("udid", config.Udid);

        return config.PlatformName switch
        {
            DTOs.PlatformType.Android => new AndroidDriver(new Uri("http://localhost:4723"), options),
            DTOs.PlatformType.iOS => new IOSDriver(new Uri("http://localhost:4723/wd/hub"), options),
            _ => throw new NotSupportedException($"Platform {config.PlatformName} is not supported.")
        };
    }

}