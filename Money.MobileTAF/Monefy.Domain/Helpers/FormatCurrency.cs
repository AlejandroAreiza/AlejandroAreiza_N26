namespace Monefy.Domain.Helpers;

public static class CurrencyExtensions
{
    public static string FormatCurrency(this string amountString, string cultureCode)
    {
        if (!decimal.TryParse(amountString, out decimal amount))
            throw new ArgumentException($"'{amountString}' is not a valid decimal amount.");

        var culture = CultureInfo.CreateSpecificCulture(cultureCode);
        return amount.ToString("C", culture);
    }
}