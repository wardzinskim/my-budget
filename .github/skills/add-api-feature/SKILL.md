---
name: add-api-feature
description: 'Add a new command or query end-to-end to the myBudget backend: Application handler + FluentValidation validator, Request record, Carter endpoint and integration test. Use when asked to "add endpoint", "dodaj endpoint", "add command", "add query", "nowa funkcjonalność w API".'
allowed-tools: shell
---

# add-api-feature

Scaffold one CQRS feature (command or query) across all backend layers.

## When to Use This Skill

- User says "add endpoint for X", "dodaj endpoint", "add command/query X"
- User wants a new operation on budgets, categories, transfers or statistics
- Keywords: command, query, handler, validator, Carter module, endpoint

## Steps

### 1. Decide kind and name

- Mutating → **command** (`ICommand`, handler in Application). Read-only → **query** (handler in Infrastructure).
- Name `<Verb><Noun>` (e.g. `RenameBudget`). Pick the Carter module (`BudgetModule`, `CategoryModule`, `TransferModule`, `BudgetStatisticsModule`) or create a new `ICarterModule`.

### 2. Create Application files

- Command: `backend/Core/MyBudget.Application/Budgets/<Feature>/<Feature>CommandHandler.cs` — see [templates/command.cs.md](templates/command.cs.md).
- Validator: `<Feature>CommandValidator.cs` — see [templates/validator.cs.md](templates/validator.cs.md).
- Query: record in `Application/Budgets/<Feature>/<Feature>Query.cs` and handler in `Infrastructure/Application/Budgets/<Feature>QueryHandler.cs` — see [templates/query.cs.md](templates/query.cs.md).
- If a new invariant is needed, use skill `add-business-rule`.

### 3. Add the endpoint

- Add request record to `backend/MyBudget.Api/Features/Core/Requests.cs`.
- Map the route and add a private static handler in the module — see [templates/endpoint.cs.md](templates/endpoint.cs.md).

### 4. Write integration tests

Use skill `add-integration-test` (404, 403, 400 validation, happy path).

### 5. Build and test

```bash
cd backend
dotnet build
dotnet test --filter "FullyQualifiedName~<Feature>"
```

### 6. Sync clients

If the HTTP contract changed → skill `update-api-client`. If useful for the assistant → skill `add-mcp-tool`.

## Requirements

- Return `Result`/`Result<T>`; never throw for domain errors (see `.github/instructions/backend-error-handling.instructions.md`).
- Always check budget existence (`BudgetsErrors.BudgetNotFound`) then `IBudgetAccessValidator.HasUserAccess`.
- Take the user from `IRequestContext`, never from a parameter.
- Commands do not commit; `UnitOfWorkFilter` does.
- Do NOT edit `Program.cs`; DI is convention-based.

## Good Example

Reference implementation: `Budgets/Transfers/UpdateTransfer/UpdateTransferCommandHandler.cs` + `TransferModule.UpdateTransfer`.

## What to Avoid

- Calling the repository's save/commit in the handler.
- Query handlers with tracking enabled.
- Request bodies that are Application command types.

## Error handling

- Build fails on missing handler → ensure the handler class derives from `MediatorRequestHandler<,>` and lives in a scanned assembly.
- Validator not triggered → it must be a public `AbstractValidator<TCommand>` for the exact command type.
