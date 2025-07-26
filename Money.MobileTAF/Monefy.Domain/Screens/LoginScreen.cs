namespace Monefy.Domain.Screens;

public class LoginScreen
{
    private readonly AppiumDriver _driver;
    private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    public LoginScreen(AppiumDriver driver)
    {
        _driver = driver;
    }

    // Element Getters
    private AppiumElement GetStartedButton => _driver.FindElement(By.Id("com.monefy.app.lite:id/buttonContinue"));
    private AppiumElement AmazingButton => _driver.FindElement(By.Id("com.monefy.app.lite:id/buttonContinue"));
    private AppiumElement YesPleaseButton => _driver.FindElement(By.Id("com.monefy.app.lite:id/buttonContinue"));
    private AppiumElement AllowButton => _driver.FindElement(By.Id("com.android.permissioncontroller:id/permission_allow_button"));
    private AppiumElement DontAllowButton => _driver.FindElement(By.Id("com.android.permissioncontroller:id/permission_deny_button"));
    private AppiumElement ImReadyButton => _driver.FindElement(By.Id("com.monefy.app.lite:id/buttonContinue"));
    private AppiumElement CloseButton => _driver.FindElement(By.Id("com.monefy.app.lite:id/buttonClose"));

    // Actions
    public void TapGetStarted() => Tap(GetStartedButton, nameof(GetStartedButton));
    public void TapAmazing() => Tap(AmazingButton, nameof(AmazingButton));
    public void TapYesPlease() => Tap(YesPleaseButton, nameof(YesPleaseButton));
    public void TapAllow() => Tap(AllowButton, nameof(AllowButton));
    public void TapDontAllow() => Tap(DontAllowButton, nameof(DontAllowButton));
    public void TapImReady() => Tap(ImReadyButton, nameof(ImReadyButton));
    public void TapClose() => Tap(CloseButton, nameof(CloseButton));

    // Helper
    private void Tap(AppiumElement element, string name)
    {
        try
        {
            element.Click();
            _logger.Info($"{name} tapped.");
        }
        catch (NoSuchElementException)
        {
            _logger.Error($"{name} not found.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Failed to tap {name}");
            throw;
        }
    }

    // Full sequence if needed
    public void SkipAuthenticationFlow()
    {
        TapGetStarted();
        TapAmazing();
        TapYesPlease();
        TapAllow();
        TapImReady();
        TapClose();
    }
}