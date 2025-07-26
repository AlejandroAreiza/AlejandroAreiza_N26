namespace Monefy.Domain.Screens;

public class LoginScreen
{
    private readonly IMobileDriver _driver;

    public LoginScreen(IMobileDriver driver)
    {
        _driver = driver;
    }

    public IMobileElement GetStartedButton => _driver.MobileElement("Start Button", MobileBy.Id("com.monefy.app.lite:id/buttonContinue"));
    public IMobileElement AmazingButton => _driver.MobileElement("Amazing Button", MobileBy.Id("com.monefy.app.lite:id/buttonContinue"));
    public IMobileElement YesPleaseButton => _driver.MobileElement("Yes Please Button", MobileBy.Id("com.monefy.app.lite:id/buttonContinue"));
    public IMobileElement AllowButton => _driver.MobileElement("Allow Button", MobileBy.Id("com.android.permissioncontroller:id/permission_allow_button"));
    public IMobileElement DontAllowButton => _driver.MobileElement("Dont Allow Button", MobileBy.Id("com.android.permissioncontroller:id/permission_deny_button"));
    public IMobileElement ImReadyButton => _driver.MobileElement("Im Ready Button", MobileBy.Id("com.monefy.app.lite:id/buttonContinue"));
    public IMobileElement CloseButton => _driver.MobileElement("Close Button", MobileBy.Id("com.monefy.app.lite:id/buttonClose"));
    public IMobileElement ExpenseToolTip => _driver.MobileElement("Expense Tool Tip", MobileBy.AndroidUIAutomator("new UiSelector().text(\"Tap to add a new expense record\")"));
    public IMobileElement IconCategoryToolTip => _driver.MobileElement("Category Tool Tip", MobileBy.AndroidUIAutomator("new UiSelector().text(\"Or tap the category icon to add a record faster\")"));
    public IMobileElement TransferToolTip => _driver.MobileElement("Transfer Tool Tip", MobileBy.AndroidUIAutomator("new UiSelector().text(\"Tap the 'Transfer' button to move money between accounts\")"));
    public IMobileElement CurrencyToolTip => _driver.MobileElement("Currency Tool Tip", By.XPath("//android.widget.TextView[@text=\"Main currency can be changed here\"]"));
    public IMobileElement OptionsButton => _driver.MobileElement("Options Button", MobileBy.Id("com.monefy.app.lite:id/overflow"));
    public IMobileElement OptionsPanel => _driver.MobileElement("Options Button", MobileBy.Id("com.monefy.app.lite:id/right_drawer"));
    

    public void CompleteOnboardingFlow()
    {
        if (GetStartedButton.IsDisplayed()) GetStartedButton.Click();
        if (AmazingButton.IsDisplayed()) AmazingButton.Click();
        if (YesPleaseButton.IsDisplayed()) YesPleaseButton.Click();
        if (AllowButton.IsDisplayed()) AllowButton.Click();
        if (ImReadyButton.IsDisplayed()) ImReadyButton.Click();
        if (CloseButton.IsDisplayed()) CloseButton.Click();
        if (ExpenseToolTip.IsDisplayed()) ExpenseToolTip.Click();
        if (IconCategoryToolTip.IsDisplayed()) IconCategoryToolTip.Click();
        if (TransferToolTip.IsDisplayed()) TransferToolTip.Click();
        if (CurrencyToolTip.IsDisplayed())
        {
            CurrencyToolTip.Click();
            OptionsButton.Click();
        }
        
    }
}