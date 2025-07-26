namespace Config.Infraestructure.Driver;

public class MobileElements : IMobileElements
{
    public string Name { get; set; }
    public By By { get; set; }

    private IList<MobileElement> _mobileElements;
    private readonly AppiumDriver _driver;
    private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    public MobileElements(string name, By by, AppiumDriver driver)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        By = by ?? throw new ArgumentNullException(nameof(by));
        _driver = driver ?? throw new ArgumentNullException(nameof(driver));

        try
        {
            var wait = new DefaultWait<AppiumDriver>(driver)
            {
                Timeout = TimeSpan.FromSeconds(5),
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            var appiumElements = wait.Until(d =>
            {
                var elements = d.FindElements(by);
                return elements.Count > 0 ? elements : null;
            });

            if (appiumElements == null || !appiumElements.Any())
                throw new TimeoutException($"No elements '{name}' found using locator: {by.Mechanism}");
                
            _mobileElements = appiumElements.Select(e => new MobileElement(name, By, driver, e)).ToList();

            _logger.Info($"{_mobileElements.Count} '{name}' elements found");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Failed to find elements '{name}' with {by}");
            throw;
        }
    }

    public int Count => _mobileElements.Count;

    public MobileElement this[int index] => _mobileElements[index];

    public IList<MobileElement> Items => _mobileElements;
}
