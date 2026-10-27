---
description: 'EF Core, repositories, query handlers and installers in MyBudget.Infrastructure'
applyTo: 'backend/Core/MyBudget.Infrastructure/**/*.cs'
---

# Infrastructure layer

- `BudgetContext` (schema `SchemaName.Budget`) uses `UseSnakeCaseNamingConvention()`; configure entities only in `IEntityTypeConfiguration<T>` classes under `Domain/Budgets/...` (applied via `ApplyConfigurationsFromAssembly`). Value objects → owned types. New strong-typed IDs → converter in `Database/Conversions/IdConverters.cs` + `ConfigureConventions`.
- Repositories implement Domain interfaces; register in `Installers/RepositoryInstaller.cs`. Other services → `ServicesInstaller`. Add registrations through an `IInstaller`, never in `Program.cs`.
- Query handlers (`Application/Budgets/*QueryHandler.cs`): `AsNoTracking()`, project in SQL, `.ConfigureAwait(false)`, check access with `IBudgetAccessValidator`.
- Schema change → new EF migration (skill `add-ef-migration`). Never edit generated migration/snapshot files by hand.
- `AuditableInterceptor` fills `IAuditable` fields; do not set them manually.
