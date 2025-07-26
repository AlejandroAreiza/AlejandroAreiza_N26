using OpenQA.Selenium.Appium;

namespace Monefy.Domain.Scenario;

public class Scenario
{
    private readonly AppiumDriver _driver;
    public Scenario(AppiumDriver driver)
    {
        _driver = driver;
    }
    public LoginScreen LoginScreen => new(_driver);
    public RingWidgetScreen RingWidgetScreen => new(_driver);
}