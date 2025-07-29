namespace Monefy.Tests.E2E;

[TestFixture]
[AllureNUnit]
[AllureFeature("Pet Api Tests")]
public class MonefyE2ETests : BaseTest
{
    [AllureFeature("Regression")]
    [TestCase(5000, 2500, 2500, IncomeCategory.Salary, ExpenseCategory.House, ExpenseCategory.Car, "Salary Income", "House", "Car", "en-US")]
    public void WhenUser_AddsValidIncomesAndExpenses_Then_BalanceShouldBeReflecteProperly(
    int income,
    int expense1,
    int expense2,
    IncomeCategory incomeCategory,
    ExpenseCategory expenseCategory1,
    ExpenseCategory expenseCategory2,
    string incomeNote,
    string expenseNote1,
    string expenseNote2,
    string culture)
    {
        var expenses = expense1 + expense2;
        var balance = income - expenses;

        scenario.RingWidgetScreen.AddIncome(income, incomeCategory, incomeNote);
        scenario.RingWidgetScreen.AddExpense(expense1, expenseCategory1, expenseNote1);
        scenario.RingWidgetScreen.AddExpense(expense2, expenseCategory2, expenseNote2);

        scenario.RingWidgetScreen.TotalIncomeAmount.Text
            .Should().BeEquivalentTo(income.ToString().FormatCurrency(culture));

        scenario.RingWidgetScreen.TotalExpenseAmount.Text
            .Should().BeEquivalentTo(expenses.ToString().FormatCurrency(culture));

        scenario.RingWidgetScreen.BalanceAmount.Text
            .Should().Contain(balance.ToString().FormatCurrency(culture));
    }

    [AllureFeature("Regression")]
    [TestCase(99999999, 10, IncomeCategory.Salary, ExpenseCategory.Bills, "Salary", "Bills", "en-US")]
    public void UserInitiatesDeleteTransaction_ThenCancels_DeletionIsAborted(
    int expense,
    int income,
    ExpenseCategory expenseCategory,
    IncomeCategory incomeCategory,
    string incomeNote,
    string expenseNote,
    string culture)
    {
        var balance = income - expense;
        scenario.RingWidgetScreen.AddExpense(expense, expenseCategory, expenseNote);
        scenario.RingWidgetScreen.AddIncome(income, incomeCategory, incomeNote);
        scenario.RingWidgetScreen.LeftMenuButton.Click();
        scenario.RingWidgetScreen.BalanceAmount.Text
            .Should().Contain(balance.ToString().FormatCurrency(culture));
        scenario.SlideUpScreen.GroupingTransactions.Items.First().Click();
        scenario.SlideUpScreen.GroupingTransactionsAmounts.Items.ToList().ForEach(x => x.ClickAndHold());
        scenario.SlideUpScreen.DeleteIcon.IsDisplayed().Should().BeTrue();
        scenario.SlideUpScreen.SortingModeButton.Click();
        scenario.RingWidgetScreen.LeftMenuButton.Click();
        scenario.SlideUpScreen.DeleteIcon.IsDisplayed().Should().BeFalse();
    }

    [AllureFeature("Regression")]
    [TestCase(500, ExpenseCategory.Bills, "Bills", "01/01/2020", AccountCategory.Cash)]
    public void DateFromDeletedAccount_ShouldNotBeReflected_OnOtherAccounts(
    int expense,
    ExpenseCategory expenseCategory,
    string expenseNote,
    string date,
    AccountCategory accountName
    )
    {
        var formattedDateByMonth = DateTime.ParseExact(date, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMMM yyyy");
        var todayFormattedByMonth = DateTime.Today.ToString("MMMM yyyy");
        scenario.RingWidgetScreen.AddExpense(expense, expenseCategory, expenseNote, date);
        scenario.RingWidgetScreen.MonthElement.Text.Should().Contain(formattedDateByMonth);
        scenario.RingWidgetScreen.OptionsButton.Click();
        scenario.Options.Accounts.DeleteAccount(accountName);
        scenario.RingWidgetScreen.SnackBar.WaitUntilItDissapear();
        scenario.RingWidgetScreen.MonthElement.Text.Should().Contain(todayFormattedByMonth);
    }

}