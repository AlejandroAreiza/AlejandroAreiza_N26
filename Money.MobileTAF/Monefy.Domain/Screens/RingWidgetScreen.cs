namespace Monefy.Domain.Screens;

public class RingWidgetScreen
{
    private readonly AppiumDriver _driver;
    private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    public RingWidgetScreen(AppiumDriver driver)
    {
        _driver = driver;
    }

    // Primary locators using resource-id
    public AppiumElement LeftMenuButton =>
        _driver.FindElement(By.Id("com.monefy.app.lite:id/leftLinesImageView"));

    public AppiumElement RightMenuButton =>
        _driver.FindElement(By.Id("com.monefy.app.lite:id/rightLinesImageView"));

    public AppiumElement BalanceAmount =>
        _driver.FindElement(By.Id("com.monefy.app.lite:id/balance_amount"));

    public AppiumElement ExpenseButton =>
        _driver.FindElement(By.Id("com.monefy.app.lite:id/expense_button"));

    public AppiumElement ExpenseButtonTitle =>
        _driver.FindElement(By.Id("com.monefy.app.lite:id/expense_button_title"));

    public AppiumElement IncomeButton =>
        _driver.FindElement(By.Id("com.monefy.app.lite:id/income_button"));

    public AppiumElement IncomeButtonTitle =>
        _driver.FindElement(By.Id("com.monefy.app.lite:id/income_button_title"));

    public AppiumElement TapToAddExpenseText =>
        _driver.FindElement(By.XPath("//*[contains(@text, 'Tap to add a new expense record')]"));

    // Alternative locators using UIAutomator (if needed)
    // For example, assume you want the first ImageView in the sliding layout:
    public AppiumElement LeftMenuButton_Alternate =>
        _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.ImageView\").instance(0)"));

    // You can add additional alternative methods as needed:
    public AppiumElement IncomeButton_Alternate =>
        _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().resourceId(\"com.monefy.app.lite:id/income_button\")"));

    // Sample actions using primary locators
    public void OpenLeftMenu()
    {
        _logger.Info("Tapping left menu button");
        LeftMenuButton.Click();
    }

    public void OpenRightMenu()
    {
        _logger.Info("Tapping right menu button");
        RightMenuButton.Click();
    }

    public void TapExpenseButton()
    {
        _logger.Info("Tapping expense button");
        ExpenseButton.Click();
    }

    public void TapIncomeButton()
    {
        _logger.Info("Tapping income button");
        IncomeButton.Click();
    }
}