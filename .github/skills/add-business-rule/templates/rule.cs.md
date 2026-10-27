# Rule templates

Sync:

```csharp
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Rules;

internal class <RuleName>(/* inputs */) : IBusinessRule
{
    private static readonly Error Error =
        new BusinessRuleValidationError(nameof(<RuleName>), "<human readable message>");

    public Result Validate()
        => /* condition holds */ true ? Result.Success() : Result.Failure(Error);
}
```

Async:

```csharp
internal class <RuleName>(/* inputs */, I<Dependency> dependency) : IAsyncBusinessRule
{
    private static readonly Error Error =
        new BusinessRuleValidationError(nameof(<RuleName>), "<human readable message>");

    public async Task<Result> ValidateAsync(CancellationToken cancellationToken = default)
        => await dependency.CheckAsync(/* ... */, cancellationToken).ConfigureAwait(false)
            ? Result.Success()
            : (Result)Error;
}
```
