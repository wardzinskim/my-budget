using MyBudget.Domain.Shared.Rules;
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Shared;

public record UserId : ValueObject
{
    public Guid Value { get; }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static Result<UserId> Of(Guid value)
    {
        var result = CheckRules(new UserIdMustNotBeEmpty(value));

        if (result.IsFailure)
        {
            return result.Error;
        }

        return new UserId(value);
    }

    public static implicit operator Guid(UserId orderId) => orderId.Value;
}