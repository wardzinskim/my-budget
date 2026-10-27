---
description: 'Error handling in the myBudget backend: Result pattern, Error types, HTTP mapping, validation, MCP'
applyTo: 'backend/**/*.cs'
---

# Backend error handling

## Rules
- Domain/business failures return `Result` / `Result<T>` (`MyBudget.SharedKernel`). Never throw for them.
- Exceptions are only for unexpected failures and for FluentValidation (`ValidationException` thrown by `ValidationFilter<>`, rendered by `ValidationExceptionHandler` as `ValidationProblemDetails` 400).
- Return an `Error` implicitly from a handler: `return BudgetsErrors.BudgetNotFound;` / `return access.Error;`.

## Error type → HTTP status (`MyBudget.Api/Extensions/ResultExtensions.cs`)
| Error | Status | Use for |
|---|---|---|
| `NotFoundError` | 404 | missing budget/transfer/user |
| `BadRequestError` | 400 | invalid operation on valid input |
| `BusinessRuleValidationError` | 400 | broken domain rule (`IBusinessRule`) |
| `ForbiddenError` | 403 | user has no access to the budget |
| any other `Error` | 500 | avoid — add a new subtype instead |

- ProblemDetails `code` extension = `error.Code.Underscore()` (e.g. `BudgetNotFound` → `budget_not_found`). Tests assert this code.
- New error subtype → add record in `SharedKernel/Error.cs` AND a branch in `ToProblemDetail` switch.
- Shared, reusable errors live in `Domain/Budgets/BudgetsErrors.cs`; rule-specific errors are `private static readonly` in the rule class (code = `nameof(Rule)`).

## Where each failure belongs
| Failure | Mechanism |
|---|---|
| Shape/length/required input | FluentValidation `AbstractValidator<TCommand>` (auto-discovered) |
| Entity not found | `BudgetsErrors.*NotFound` returned from handler |
| Access denied | `IBudgetAccessValidator.HasUserAccess(budget)` → `access.Error` |
| Invariant of aggregate | `IBusinessRule` / `IAsyncBusinessRule` via `CheckRules(...)` in the aggregate |

## Endpoints
`result.Match(Results.Ok)` / `result.Match(() => Results.Ok())` / `result.Match(Results.Created)`. Declare `ProducesProblem(404/403)` and `ProducesValidationProblem()` on every route that can return them.

## MCP tools
Unwrap with `result.Match(x => x, x => throw new McpException(x.Description))`.
