using MyBudget.Domain.Shared.Rules;
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Shared;

public record Money : ValueObject
{
    public decimal Value { get; }
    public string Currency { get; }

    private Money(decimal value, string currency)
    {
        Value = value;
        Currency = currency;
    }

    public static Result<Money> Of(decimal value, string currency)
    {
        var result = CheckRulesAsync(default,
                new MoneyMustHavePositiveValue(value),
                new MoneyMustHaveCurrency(currency))
            .ConfigureAwait(false).GetAwaiter().GetResult();

        if (result.IsFailure)
        {
            return result.Error;
        }

        return new Money(value, currency);
    }
}