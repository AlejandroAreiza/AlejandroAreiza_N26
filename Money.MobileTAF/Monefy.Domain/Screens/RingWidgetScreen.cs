using System.Security.Cryptography.X509Certificates;

namespace Monefy.Domain.Screens;

public class RingWidgetScreen
{
    private readonly IMobileDriver _driver;
    public string Name { get; } = "com.monefy.activities.main.MainActivity_";

    public RingWidgetScreen(IMobileDriver driver)
    {
        _driver = driver;
    }

    public IMobileElement LeftMenuButton =>
        _driver.MobileElement("Left Menu Button", MobileBy.Id("com.monefy.app.lite:id/leftLinesImageView"));

    public IMobileElement RightMenuButton =>
        _driver.MobileElement("Right Menu Button", MobileBy.Id("com.monefy.app.lite:id/rightLinesImageView"));

    public IMobileElement BalanceAmount =>
        _driver.MobileElement("Balance Amount Container", MobileBy.Id("com.monefy.app.lite:id/balance_amount"));

    public IMobileElement ExpenseButton =>
        _driver.MobileElement("Expense Button", MobileBy.Id("com.monefy.app.lite:id/expense_button"));

    public IMobileElement ExpenseButtonTitle =>
        _driver.MobileElement("Expense Button Title", MobileBy.Id("com.monefy.app.lite:id/expense_button_title"));

    public IMobileElement IncomeButton =>
        _driver.MobileElement("Income Button", MobileBy.Id("com.monefy.app.lite:id/income_button"));

    public IMobileElement IncomeButtonTitle =>
        _driver.MobileElement("Income Button Title", MobileBy.Id("com.monefy.app.lite:id/income_button_title"));

    public IMobileElement AddExpenseInput =>
        _driver.MobileElement("Add Expense Input", By.XPath("//*[contains(@text, 'Tap to add a new expense record')]"));


    public IMobileElement LeftMenuButton_Alternate =>
        _driver.MobileElement("Left Menu Button Alternate", MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.ImageView\").instance(0)"));


    public IMobileElement IncomeButton_Alternate =>
        _driver.MobileElement("Income Button Alternate", MobileBy.AndroidUIAutomator("new UiSelector().resourceId(\"com.monefy.app.lite:id/income_button\")"));

    public IMobileElement SnackBarCancelButton => _driver.MobileElement("Snack Bar Cancel Button", MobileBy.Id("com.monefy.app.lite:id/snackbar_action"));
    public IMobileElement SnackBar => _driver.MobileElement("Snack Bar Cancel Button", MobileBy.Id("com.monefy.app.lite:id/snackbar_text"));

    public IMobileElement TotalIncomeAmount => _driver.MobileElement("Total Income Amount", MobileBy.Id("com.monefy.app.lite:id/income_amount_text"));

    public IMobileElement TotalExpenseAmount => _driver.MobileElement("Total Expense Amount", MobileBy.Id("com.monefy.app.lite:id/expense_amount_text"));
    public IMobileElement OptionsButton => _driver.MobileElement("Options Button", MobileBy.Id("com.monefy.app.lite:id/overflow"));

    IMobileElement MonefyPremiumToolTip => _driver.MobileElement("Transfer Tool Tip", MobileBy.AndroidUIAutomator("new UiSelector().text(\"Recurring records are now available in Monefy Premium!\")"));

    private AppiumElement recycler => _driver.Driver.FindElement(MobileBy.Id("com.monefy.app.lite:id/pts_main"));

    public IMobileElement MonthElement => new MobileElement("MonthItem",
            MobileBy.ClassName("android.widget.TextView"),
            _driver.Driver,
            recycler.FindElement(MobileBy.ClassName("android.widget.TextView"))
        );



    public void AddIncome(int amount, IncomeCategory incomeCategory, string? note = null)
    {
        IncomeButton.Click();
        var incomeScreen = new IncomeScreen(_driver);
        if (MonefyPremiumToolTip.IsDisplayed()) MonefyPremiumToolTip.Click();
        incomeScreen.AddIncome(amount, incomeCategory, note);
        SnackBar.WaitUntilItDissapear();

    }
    public void AddExpense(int amount, ExpenseCategory incomeCategory, string? note = null, string date=null)
    {
        ExpenseButton.Click();
        var expenseScreen = new ExpenseScreen(_driver);
        if (MonefyPremiumToolTip.IsDisplayed()) MonefyPremiumToolTip.Click();
        expenseScreen.AddExpense(amount, incomeCategory, note, date);
        SnackBar.WaitUntilItDissapear();
    }
}