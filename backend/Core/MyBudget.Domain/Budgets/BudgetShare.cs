using MyBudget.Domain.Budgets.Rules;
using MyBudget.Domain.Shared;
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets;

public record BudgetShare : ValueObject
{
    public UserId UserId { get; }
    public string UserName { get; }

    private BudgetShare(UserId userId, string userName)
    {
        UserId = userId;
        UserName = userName;
    }

    public static Result<BudgetShare> Create(Guid userId, string userName)
    {
        var result = CheckRules(new UserNameMustNotBeEmpty(userName));
        if (result.IsFailure) return result.Error;
        
        var userIdValue = UserId.Of(userId);
        if (userIdValue.IsFailure) return userIdValue.Error;

        return new BudgetShare(userIdValue.Value, userName);
    }
}