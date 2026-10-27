---
description: 'Integration test conventions for MyBudget.Api.Tests (xUnit, Testcontainers, Bogus)'
applyTo: 'backend/MyBudget.Api.Tests/**/*.cs'
---

# Integration tests

- Class `<Verb><Feature>Tests(IntegrationTestWebAppFactory application) : BudgetsIntegrationTest(application)` in `Core/Budget/` (transfers in `Core/Budget/Transfers/`). Docker required (Testcontainers PostgreSQL).
- Method names: `<VERB>_<resource>_<scenario>[_returns_<status>]`, e.g. `POST_budget_category_no_budget_returns_404`. Use `//arrange //act //assert` comments.
- Seed with `FakeBudgetBuilder.Build(budgetId, _application.UserId, name)` + `_dbContext.Budgets.AddAsync` + `SaveChangesAsync`. Another user's budget: pass `Guid.NewGuid()` as owner. Data via `Bogus.Faker`.
- Call the API through `_httpClient` using the API request records.
- Verify persistence with `_dbContext.ChangeTracker.Clear()` then re-query.
- Standard cases per endpoint: 404 (`AssertBudgetNotExistsAsync`), 403 (`AssertBudgetForbiddenAsync`), 400 validation (`ValidationProblemDetails.Errors` key, lower-case), 400 business rule (`Extensions["code"]` snake_case), happy path.
- External services are mocked in `Mocks/`; `TestAuthHandler` supplies the user (`_application.UserId`).
- Run: `dotnet test --filter "FullyQualifiedName~<ClassName>"` from `backend/`.
