# Query template

Application (`MyBudget.Application/Budgets/<Feature>/<Feature>Query.cs`):

```csharp
namespace MyBudget.Application.Budgets.<Feature>;

public record <Feature>Query(Guid Id) : Request<Result<<Feature>Response>>;

public record <Feature>Response(/* fields */);
```

Infrastructure (`MyBudget.Infrastructure/Application/Budgets/<Feature>QueryHandler.cs`):

```csharp
public sealed class <Feature>QueryHandler(IBudgetAccessValidator budgetAccessValidator, BudgetContext context)
    : MediatorRequestHandler<<Feature>Query, Result<<Feature>Response>>
{
    protected override async Task<Result<<Feature>Response>> Handle(
        <Feature>Query request, CancellationToken cancellationToken)
    {
        var budget = await context.Budgets
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        if (budget is null) return BudgetsErrors.BudgetNotFound;

        var access = budgetAccessValidator.HasUserAccess(budget);
        if (access.IsFailure) return access.Error;

        // project with AsNoTracking() queries on context.Transfers etc.
        return new <Feature>Response(/* ... */);
    }
}
```

Reference: `GetBudgetTotalsQueryHandler`. Queries are not `ICommand` (no unit of work).
