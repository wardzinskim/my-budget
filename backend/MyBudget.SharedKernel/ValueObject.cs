namespace MyBudget.SharedKernel;

public record ValueObject
{
    protected static async ValueTask<Result> CheckRulesAsync(
        CancellationToken cancellationToken = default,
        params IAsyncBusinessRule[] rules
    )
    {
        foreach (var rule in rules)
        {
            var result = await rule.ValidateAsync(cancellationToken).ConfigureAwait(false);
            if (result.IsFailure) return result;
        }

        return Result.Success();
    }

    protected static Result CheckRules(
        params IBusinessRule[] rules
    )
    {
        foreach (var rule in rules)
        {
            var result = rule.Validate();
            if (result.IsFailure) return result;
        }

        return Result.Success();
    }
}