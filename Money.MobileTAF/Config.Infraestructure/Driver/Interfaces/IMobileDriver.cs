using Config.Infraestructure.Driver;

public interface IMobileDriver : IDisposable
{
    AppiumDriver Driver { get; }
    public IMobileElement MobileElement(string name, By by);
    IMobileElements MobileElements(string name, By by);
    void InstallApp();
    void Uninstall();
    void TerminateApp();
    void GoToScreen(string screenName);
}