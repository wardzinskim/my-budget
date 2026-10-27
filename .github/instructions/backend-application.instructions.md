---
description: 'CQRS conventions for MyBudget.Application: commands, queries, validators, access checks'
applyTo: 'backend/Core/MyBudget.Application/**/*.cs'
---

# Application layer

## Structure
`Budgets/<FeatureName>/` (transfers: `Budgets/Transfers/<FeatureName>/`) with `<Feature>CommandHandler.cs` (record + handler in one file) and `<Feature>CommandValidator.cs`. DTOs in `Budgets/Model/`. Namespace mirrors folder.

## Commands
- `public record XCommand(Guid BudgetId, ...) : Request<Result>, ICommand;` (or `Request<Result<T>>`).
- Handler in the same file: `public sealed class XCommandHandler(...) : MediatorRequestHandler<XCommand, Result>`.
- Standard flow: load budget via `IBudgetRepository` → `BudgetsErrors.BudgetNotFound` → `IBudgetAccessValidator.HasUserAccess(budget)` → call aggregate method → return `Result.Success()`.
- Do NOT commit manually: `UnitOfWorkFilter` commits when the result is success.
- Never put `UserId` in a command; use `IRequestContext.UserId`.

## Queries
- Query record (`Request<Result<T>>`, no `ICommand`) lives here; its **handler lives in `MyBudget.Infrastructure/Application/...`** and reads via `BudgetContext` with `AsNoTracking()`. Always apply budget existence + access checks.

## Validators
- `AbstractValidator<TCommand>`; validate shape only (`NotEmpty`, `MaximumLength`, ranges). Domain invariants belong in business rules. No registration needed (convention discovery).

See `backend-error-handling.instructions.md` for returning errors.
