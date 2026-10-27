# Command + handler template

```csharp
using MyBudget.Application.Budgets.Services;
using MyBudget.Domain.Budgets;
using MyBudget.Infrastructure.Abstractions.Features;

namespace MyBudget.Application.Budgets.<Feature>;

public record <Feature>Command(Guid BudgetId, string Name) : Request<Result>, ICommand;

public sealed class <Feature>CommandHandler(
    IBudgetRepository budgetRepository,
    IBudgetAccessValidator budgetAccessValidator
)
    : MediatorRequestHandler<<Feature>Command, Result>
{
    protected override async Task<Result> Handle(<Feature>Command request, CancellationToken cancellationToken)
    {
        var budget = await budgetRepository.GetAsync(request.BudgetId, cancellationToken).ConfigureAwait(false);
        if (budget is null) return BudgetsErrors.BudgetNotFound;

        var access = budgetAccessValidator.HasUserAccess(budget);
        if (access.IsFailure) return access.Error;

        var result = budget.<AggregateMethod>(request.Name);
        if (result.IsFailure) return result.Error;

        return Result.Success();
    }
}
```

Use `Request<Result<T>>` when returning data (e.g. created id).
