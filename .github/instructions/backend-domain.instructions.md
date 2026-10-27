---
description: 'DDD conventions for MyBudget.Domain: aggregates, business rules, value objects, events'
applyTo: 'backend/Core/MyBudget.Domain/**/*.cs'
---

# Domain layer

- `Budget` is the aggregate root; `Transfer`, `TransferCategory`, `BudgetShare` live under it. Change state only through aggregate methods that return `Result` / `Result<T>`.
- No references to EF Core, Application or Infrastructure. Dependencies needed by rules (e.g. uniqueness) are interfaces defined here (`IBudgetNameUniquenessChecker`) and implemented in Infrastructure.
- Strong-typed IDs (`BudgetId`, `TransferId`, `UserId`) and value objects (`Money`) derive from SharedKernel types; validate through rules in `Shared/Rules/`.

## Business rules (`Budgets/Rules/`)
- `internal class <RuleName>(...) : IBusinessRule` (or `IAsyncBusinessRule` when I/O is needed).
- Class name states the invariant (`BudgetNameMustBeUniqueForUser`) — it becomes the snake_case error code.
- Hold `private static readonly Error Error = new BusinessRuleValidationError(nameof(<RuleName>), "<message>");`.
- Use from the aggregate: `CheckRules(new RuleA(...), new RuleB(...))` / `await CheckRulesAsync(...)`; return `result.Error` on failure.
- Every new rule needs an integration test asserting the snake_case `code`.

## Events
- Raise domain events inside the aggregate; they are dispatched during persistence. Name: `<Entity><PastTense>Event` in `Events/`.
