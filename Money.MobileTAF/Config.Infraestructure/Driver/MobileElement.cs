using OpenQA.Selenium.BiDi.Modules.Input;
using OpenQA.Selenium.Interactions;


namespace Config.Infraestructure.Driver;

public class MobileElement : IMobileElement
{
    public string Name { get; set; }
    public By By { get; set; }

    private AppiumElement _appiumElement;
    private AppiumDriver _driver;
    private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
    private bool foundElement = false;

    public MobileElement(string name, By by, AppiumDriver driver)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        By = by ?? throw new ArgumentNullException(nameof(by));
        _driver = driver ?? throw new ArgumentNullException(nameof(driver));
    }
    public MobileElement(string name, By by, AppiumDriver driver, AppiumElement element)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        By = by ?? throw new ArgumentNullException(nameof(by));
        _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        _appiumElement = element ?? throw new ArgumentNullException(nameof(element));
    }

    private bool FindElement(double time = 5, int poolingTime = 500)
    {
        var timeout = TimeSpan.FromSeconds(time);
        var pollingInterval = TimeSpan.FromMilliseconds(poolingTime);
        try
        {
            var wait = new DefaultWait<AppiumDriver>(_driver)
            {
                Timeout = timeout,
                PollingInterval = pollingInterval
            };

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            _appiumElement = wait.Until(d =>
            {
                var element = d.FindElement(By);
                return (element != null && (element.Displayed || element.Enabled)) ? element : null;
            });

            foundElement = _appiumElement != null;
            return foundElement;
        }
        catch
        {
            foundElement = false;
            return false;
        }
    }

    public void Click()
    {
        try
        {

            if (!FindElement()) throw new ArgumentException($"Mobile Element '{Name}' not found");
            _logger.Info($"Clicking on '{Name}'");
            _appiumElement.Click();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Failed to click on '{Name}'");
            throw;
        }
    }

    public void Type(string text)
    {
        try
        {
            if (!FindElement()) throw new ArgumentException($"Mobile Element '{Name}' not found");
            _logger.Info($"Sending keys to '{Name}': {text}");
            _appiumElement.SendKeys(text);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Failed to send keys to '{Name}'");
            throw;
        }
    }

    public string Text
    {
        get
        {
            try
            {
                if (!FindElement()) throw new ArgumentException($"Mobile Element '{Name}' not found");
                var text = _appiumElement.Text;
                _logger.Info($"'{Name} Text is {text}'");
                return text;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Failed to get Text from '{Name}'");
                throw;
            }
        }
    }

    public bool IsDisplayed()
    {
        try
        {
            FindElement(1);
            if (_appiumElement == null)
            {
                _logger.Info($"Mobile Element '{Name}' is not displayed.");
                return false;
            }
            var displayed = _appiumElement.Displayed;
            _logger.Info($"'{Name}' is displayed: {displayed}");
            return displayed;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Mobile Element '{Name}' not displayed or not found");
            return false;
        }
    }

    public void WaitUntilItDissapear()
    {
        var wait = new DefaultWait<AppiumDriver>(_driver)
        {
            Timeout = TimeSpan.FromSeconds(5),
            PollingInterval = TimeSpan.FromMilliseconds(500)
        };

        wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

        try
        {
            wait.Until(driver =>
            {
                try
                {
                    var elements = driver.FindElements(By);
                    return elements.Count == 0 || !elements[0].Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
            });

            _logger.Info($"Element '{Name}' has disappeared");
        }
        catch (WebDriverTimeoutException ex)
        {
            _logger.Error(ex, $"Element '{Name}' did not disappear in the expected time");
            throw new TimeoutException($"Timeout waiting for element '{Name}' to disappear");
        }
    }

    public void ClickAndHold()
    {
        var actions = new Actions(_driver);
        try
        {
            if (!FindElement()) throw new ArgumentException($"Mobile Element '{Name}' not found");
            actions
            .ClickAndHold(_appiumElement)
            .Pause(TimeSpan.FromMilliseconds(1000))
            .Release()
            .Perform();
            _logger.Info($"Element '{Name}' clicked and hold");

        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error clicking and Holding Element '{Name}'");
            throw;
        }
    }
}
