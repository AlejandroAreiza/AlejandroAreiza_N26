using OpenQA.Selenium.DevTools.V131.FedCm;

namespace Monefy.Domain.Screens;

public class AccountsScreen
{
    private readonly IMobileDriver _driver;

    public AccountsScreen(IMobileDriver driver)
    {
        _driver = driver;
    }

    public IMobileElements Accounts => _driver.MobileElements("Accounts", MobileBy.Id("com.monefy.app.lite:id/relativeLayoutManageCategoriesListItem"));
    public IMobileElement AccountsPanel => _driver.MobileElement("Accounts", MobileBy.Id("com.monefy.app.lite:id/accounts_panel"));
    public IMobileElement DeleteButton => _driver.MobileElement("Delete Button", MobileBy.Id("com.monefy.app.lite:id/delete"));
    public IMobileElement OkButton => _driver.MobileElement("Delete Button", MobileBy.XPath("//android.widget.Button[@text='OK']"));
    
    

    public void DeleteAccount(AccountCategory accountName)
    {
        AccountsPanel.Click();
        Accounts.Items.First().Click();
        DeleteButton.Click();
        OkButton.Click();

    }
}