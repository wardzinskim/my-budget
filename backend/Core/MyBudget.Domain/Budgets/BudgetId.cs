using MyBudget.Domain.Shared.Rules;
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets;

public record BudgetId : ValueObject
{
    public Guid Value { get; }

    private BudgetId(Guid value)
    {
        Value = value;
    }

    public static Result<BudgetId> Of(Guid value)
    {
        var result = CheckRules(new UserIdMustNotBeEmpty(value));

        if (result.IsFailure)
        {
            return result.Error;
        }

        return new BudgetId(value);
    }
    
    public static implicit operator Guid(BudgetId orderId) => orderId.Value;
}