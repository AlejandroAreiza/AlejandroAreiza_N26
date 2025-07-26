namespace Monefy.Tests.E2E;

[TestFixture]
public class MonefyE2ETests : BaseTest
{
    [Test]
    public void WhenUser_SkipsAuthentication_ThenHomeScreenShouldBeDisplayed()
    {
        scenario.LoginScreen.SkipAuthenticationFlow();
        scenario.RingWidgetScreen.LeftMenuButton.Click();
        scenario.RingWidgetScreen.BalanceAmount.Text.Should().Be("1.00");
    }
    [Test]
    public void WhenUser_SkipsAuthentication_ThenHomeScreenShouldBeDisplayed2()
    {
        scenario.LoginScreen.SkipAuthenticationFlow();
        scenario.RingWidgetScreen.LeftMenuButton.Click();
        scenario.RingWidgetScreen.BalanceAmount.Text.Should().Be("1.00");
    }
}