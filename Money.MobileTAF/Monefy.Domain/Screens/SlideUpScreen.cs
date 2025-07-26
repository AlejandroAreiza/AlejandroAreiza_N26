namespace Monefy.Domain.Screens;

public class SlideUpScreen
{
    private readonly IMobileDriver _driver;
    public SlideUpScreen(IMobileDriver driver)
    {
        _driver = driver;
    }

    public IMobileElement SortingModeButton => _driver.MobileElement("Sorting Mode Button", MobileBy.Id("com.monefy.app.lite:id/buttonChooseListSortingMode"));

    public IMobileElements GroupingTransactions => _driver.MobileElements("Transaction Group", MobileBy.Id("com.monefy.app.lite:id/transaction_group_header"));

    public IMobileElements GroupingTransactionsAmounts => _driver.MobileElements("Transaction Amounts", MobileBy.Id("com.monefy.app.lite:id/textViewTransactionAmount"));
    public IMobileElement DeleteIcon => _driver.MobileElement("Delete Icon", MobileBy.Id("com.monefy.app.lite:id/delete"));
    
}
