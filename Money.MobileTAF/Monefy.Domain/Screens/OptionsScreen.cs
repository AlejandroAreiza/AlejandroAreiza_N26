namespace Monefy.Domain.Screens;

public class OptionsScreen
{
    private readonly IMobileDriver _driver;

    public OptionsScreen(IMobileDriver driver)
    {
        _driver = driver;
    }

    public IMobileElement AccountsButton => _driver.MobileElement("Accounts", MobileBy.Id("com.monefy.app.lite:id/accounts_button"));
    public AccountsScreen Accounts => new AccountsScreen(_driver);


}