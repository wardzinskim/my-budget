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
        var result = CheckRules(new MoneyMustHavePositiveValue(value),
            new MoneyMustHaveCurrency(currency));

        if (result.IsFailure)
        {
            return result.Error;
        }

        return new Money(value, currency);
    }
}