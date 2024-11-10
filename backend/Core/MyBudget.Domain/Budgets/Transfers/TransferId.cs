using MyBudget.Domain.Budgets.Rules;
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Transfers;

public record TransferId : ValueObject
{
    public Guid Value { get; }

    private TransferId(Guid value)
    {
        Value = value;
    }

    public static Result<TransferId> Of(Guid value)
    {
        var result = CheckRules(new TransferIdMustNotBeEmpty(value));

        if (result.IsFailure)
        {
            return result.Error;
        }

        return new TransferId(value);
    }

    public static implicit operator Guid(TransferId orderId) => orderId.Value;
}