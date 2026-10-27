---
name: add-integration-test
description: 'Write xUnit integration tests for a myBudget API endpoint using BudgetsIntegrationTest, FakeBudgetBuilder, Bogus and Testcontainers PostgreSQL. Use when asked to "add tests", "dodaj testy", "write integration test", "test endpoint".'
allowed-tools: shell
---

# add-integration-test

Create an endpoint test class covering 404, 403, validation, business-rule and happy-path cases.

## When to Use This Skill

- User says "add tests for endpoint X", "dodaj testy integracyjne"
- A new endpoint/command/rule was added and needs coverage
- Keywords: xUnit, WebApplicationFactory, Testcontainers, integration test

## Steps

### 1. Create the class

`backend/MyBudget.Api.Tests/Core/Budget/<Verb><Feature>Tests.cs` (transfers: `Core/Budget/Transfers/`). Start from [templates/test-class.cs.md](templates/test-class.cs.md).

### 2. Cover the cases

1. Budget not found → `AssertBudgetNotExistsAsync(response)`
2. Not my budget → `AssertBudgetForbiddenAsync(response)` (owner `Guid.NewGuid()`)
3. Invalid input → 400 + `ValidationProblemDetails.Errors` contains lower-case property key
4. Business rule violated → 400 + `Extensions["code"]` snake_case
5. Happy path → status code + re-query DB after `_dbContext.ChangeTracker.Clear()`

### 3. Run

Docker must be running.

```bash
cd backend
dotnet test --filter "FullyQualifiedName~<Verb><Feature>Tests"
```

## Requirements

- Extend `BudgetsIntegrationTest`; constructor takes `IntegrationTestWebAppFactory`.
- Name tests `<VERB>_<resource>_<scenario>[_returns_<status>]`.
- Generate data with `Bogus.Faker`; seed with `FakeBudgetBuilder.Build(...)`.
- Use API request records in the body, not Application commands.
- Do NOT share state between tests; each uses fresh ids.

## Good Example

`PostBudgetCategoryTests.cs`.

## What to Avoid

- Asserting on in-memory tracked entities without `ChangeTracker.Clear()`.
- Hard-coded GUIDs/names that collide across tests in the shared database.

## Error handling

- Container start fails → start Docker Desktop.
- 401 in tests → ensure the base class sets the test auth header (default) and don't override `HttpClient`.
