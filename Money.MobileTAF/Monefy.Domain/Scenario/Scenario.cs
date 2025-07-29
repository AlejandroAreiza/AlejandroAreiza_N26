namespace Monefy.Domain.Scenario;

public class Scenario
{
    private readonly IMobileDriver _driver;
    public Scenario(IMobileDriver driver)
    {
        _driver = driver;
    }
    public LoginScreen LoginScreen => new(_driver);
    public RingWidgetScreen RingWidgetScreen => new(_driver);
    public SlideUpScreen SlideUpScreen => new(_driver);
    public OptionsScreen Options => new(_driver);
}