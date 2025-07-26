namespace Config.Infraestructure.Driver;

public interface IMobileElement
{
    void Click();
    void Type(string text);
    public string Text { get; }
    bool IsDisplayed();
    void WaitUntilItDissapear();
    void ClickAndHold();
}
