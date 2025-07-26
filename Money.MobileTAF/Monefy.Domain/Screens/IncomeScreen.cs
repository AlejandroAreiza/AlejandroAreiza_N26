namespace Monefy.Domain.Screens;

public class IncomeScreen
{
    private readonly IMobileDriver _driver;

    public IncomeScreen(IMobileDriver driver)
    {
        _driver = driver;
    }
    public IMobileElement AmounInput => _driver.MobileElement("Amoun Input", MobileBy.Id("com.monefy.app.lite:id/amount_text"));
    public IMobileElement TextViewNote => _driver.MobileElement("Text View Note", MobileBy.Id("com.monefy.app.lite:id/textViewNote"));
    public IMobileElement KeyPad(string number) => _driver.MobileElement($"Key Pad {number}", MobileBy.Id($"com.monefy.app.lite:id/buttonKeyboard{number}"));

    public IMobileElement ChooseCategoryButton => _driver.MobileElement("Choose Category Button", MobileBy.Id($"com.monefy.app.lite:id/keyboard_action_button"));

    public IMobileElement IncomeCategory(IncomeCategory incomeCategory) =>
        _driver.MobileElement($"Income {incomeCategory} Category", MobileBy.AndroidUIAutomator($"new UiSelector().text(\"{incomeCategory}\")"));



    public void AddIncome(int amount, IncomeCategory incomeCategory, string? note = null)
    {
        SendAmount(amount);
        if (!string.IsNullOrEmpty(note)) TextViewNote.Type(note);
        ChooseCategoryButton.Click();
        IncomeCategory(incomeCategory).Click();
    }

    private void SendAmount(int amount)
    {
        foreach (var n in amount.ToString())
        {
            KeyPad(n.ToString()).Click();
        }
    }

}
