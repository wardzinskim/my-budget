---
name: add-business-rule
description: 'Add a domain business rule (IBusinessRule / IAsyncBusinessRule) to the myBudget Budget aggregate with correct error code, aggregate wiring and integration test. Use when asked to "add business rule", "dodaj regułę biznesową", "validate invariant", "reguła domenowa".'
---

# add-business-rule

Create a domain rule whose class name becomes the snake_case HTTP error code.

## When to Use This Skill

- User says "add a rule that…", "dodaj regułę biznesową…", "budget must/must not…"
- An invariant must hold on the `Budget` aggregate (or its children)
- Keywords: IBusinessRule, IAsyncBusinessRule, CheckRules, BusinessRuleValidationError

## Steps

### 1. Name the rule

`<Subject>Must[Not]<Condition>` (e.g. `BudgetMustNotBeSharedToOwner`). Name = error code → `budget_must_not_be_shared_to_owner`.

### 2. Create the class

File `backend/Core/MyBudget.Domain/Budgets/Rules/<RuleName>.cs` (cross-cutting value rules: `Domain/Shared/Rules/`). Use [templates/rule.cs.md](templates/rule.cs.md).

- Sync rule → `IBusinessRule` (`Result Validate()`).
- Needs I/O → `IAsyncBusinessRule` (`Task<Result> ValidateAsync(CancellationToken)`); define the dependency interface in Domain (like `IBudgetNameUniquenessChecker`), implement it in Infrastructure, register in `Installers/RepositoryInstaller.cs` or `ServicesInstaller.cs`, and add a mock in `MyBudget.Api.Tests/Mocks/` if the test factory needs it.

### 3. Wire into the aggregate

```csharp
var result = CheckRules(new <RuleName>(...));          // async: await CheckRulesAsync(cancellationToken, new <RuleName>(...))
if (result.IsFailure) return result.Error;
```

### 4. Test

Use skill `add-integration-test`; assert HTTP 400 and `problemDetail.Extensions["code"]` equals the snake_case name.

```bash
cd backend
dotnet test --filter "FullyQualifiedName~<TestClass>"
```

## Requirements

- Rule class is `internal`, one invariant per class.
- Error is `private static readonly Error Error = new BusinessRuleValidationError(nameof(<RuleName>), "<human message>")`.
- Domain must not reference EF Core or Infrastructure.
- Do NOT throw exceptions from rules.

## Good Example

`Budgets/Rules/TransferCategoryMustBeDefined.cs` (sync) and `BudgetNameMustBeUniqueForUser.cs` (async).

## What to Avoid

- Putting input-shape checks (length, required) in rules → use FluentValidation.
- Using `BadRequestError` for invariant violations → use `BusinessRuleValidationError`.

## Error handling

- Code in response is not snake_case → the class name or `nameof` differs; keep them identical.
- Rule never fires → verify it is passed to `CheckRules` in the aggregate method actually called by the handler.
