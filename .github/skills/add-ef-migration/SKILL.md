---
name: add-ef-migration
description: 'Create and verify an EF Core migration for BudgetContext (PostgreSQL, snake_case, schema budget) in myBudget. Use when asked to "add migration", "dodaj migrację", "change database schema", "update entity configuration", "ef migrations".'
allowed-tools: shell
---

# add-ef-migration

Generate an EF Core migration for the Budget database after model or configuration changes.

## When to Use This Skill

- User says "add migration", "dodaj migrację", "zmień schemat bazy"
- An entity, owned type or `IEntityTypeConfiguration<T>` changed in `MyBudget.Infrastructure`
- Keywords: EF Core, migration, snapshot, BudgetContext, dotnet ef

## Steps

### 1. Finish model changes

Edit entities/configurations first (`Core/MyBudget.Infrastructure/Domain/Budgets/*EntityTypeConfiguration.cs`). Keep snake_case via convention; do not hardcode column names unless required.

### 2. Generate the migration

```bash
cd backend
dotnet ef migrations add <PascalCaseName> --project Core/MyBudget.Infrastructure --startup-project MyBudget.Api --context BudgetContext --output-dir Migrations
```

`dotnet ef` is installed globally; if missing: `dotnet tool install --global dotnet-ef`.

### 3. Review

- Inspect the generated `Up`/`Down` and `BudgetContextModelSnapshot.cs` diff: only intended changes, schema `budget`, snake_case names.
- Never hand-edit the Designer or snapshot files.
- For data backfills add `migrationBuilder.Sql(...)` in `Up` and a reversible `Down`.

### 4. Verify

```bash
dotnet build
dotnet test
```

Integration tests apply migrations on a fresh Testcontainers database, so a green run proves the migration applies (Docker required).

### 5. Undo a bad migration (not yet applied/committed)

```bash
dotnet ef migrations remove --project Core/MyBudget.Infrastructure --startup-project MyBudget.Api --context BudgetContext
```

## Requirements

- Run commands from `backend/`.
- One migration per logical change; name describes it (`AddBudgetShares`).
- The Identity project has its own `ApplicationDbContext` and migrations — do NOT mix them.
- Do NOT run `dotnet ef database update` against shared/production databases.

## Good Example

`Migrations/20241229202301_AddBudgetShares.cs`.

## What to Avoid

- Editing an already-applied migration; create a new one instead.
- Generating migrations with a pending, unbuildable model.

## Error handling

- "Unable to create a DbContext" → ensure `ConnectionStrings:Default` is available (appsettings.Development.json) and startup project builds.
- Migration contains unrelated changes → snapshot was out of date; review and trim only by regenerating, not manual edits.
