# myBudget – Copilot Instructions

## Repository layout

```
backend/    .NET solution (API + Identity + Domain/Application/Infrastructure)
frontend/   Turborepo monorepo (React web app + shared packages)
devops/     Helm / Kubernetes manifests and helmfile
.github/    GitHub Actions workflows, instructions/ and skills/
```

## Instructions and skills

Layer-specific rules apply automatically by path:
- backend: `.github/instructions/backend-*.instructions.md` (domain, application, infrastructure, api, tests, error-handling, identity)
- frontend: `.github/instructions/frontend-*.instructions.md` (web-conventions, routing-data, forms, minimal-ui)
- deployment/CI: `.github/instructions/devops.instructions.md`

Skills in `.github/skills/`:
- backend: `add-api-feature`, `add-business-rule`, `add-integration-test`, `add-ef-migration`, `add-mcp-tool`
- frontend: `add-frontend-page`, `add-frontend-form`, `add-dashboard-widget`, `add-minimal-ui-component`
- cross-cutting: `update-api-client`

## Build, test, and lint

### Backend (run from `backend/`)
```bash
dotnet build
dotnet test                                        # all tests
dotnet test --filter "FullyQualifiedName~PostBudgetTests"  # single test class
dotnet test --filter "DisplayName~POST_budget_create_budget"  # single test method
```
Tests require Docker (Testcontainers spins up a PostgreSQL container automatically).

### Frontend (run from `frontend/`)
```bash
npm install
npm run build    # turbo build across all workspaces
npm run dev      # turbo dev (starts web app + watches packages)
npm run lint     # turbo lint
```

To work on a single workspace:
```bash
cd frontend/apps/web && npm run dev
cd frontend/apps/web && npm run lint
```

### Regenerate the API client
```bash
cd frontend/packages/api-client
npm run generate   # reads swagger.json, runs openapi-generator-cli
npm run build      # generate + tsc + vite build
```
The generated client in `packages/api-client/lib/` is checked in; update `swagger.json` and rebuild when the backend API changes.

---

## Architecture

### Three deployed components

| Component | Directory | Tech |
|---|---|---|
| `my-budget.api` | `backend/` (solution root) | ASP.NET Core 8 Minimal API |
| `my-budget.identity` | `backend/Identity/` | ASP.NET Core + OpenIddict (OIDC provider) |
| `my-budget.frontend` | `frontend/apps/web/` | React 18 + Vite |

The frontend authenticates users via OIDC against the identity server, then calls the API using Bearer tokens.

### Backend layer structure

```
MyBudget.SharedKernel          Base types: Result<T>, Entity, ValueObject, Error, domain event interfaces
MyBudget.Domain                Aggregate roots (Budget), domain events, business rules, repository interfaces
MyBudget.Application           CQRS command/query handlers (MassTransit mediator), FluentValidation validators
MyBudget.Infrastructure        EF Core + PostgreSQL (BudgetContext), repository implementations
MyBudget.Api                   Carter modules (route definitions), installers, Program.cs
MyBudget.Api.Tests             xUnit integration tests (WebApplicationFactory + Testcontainers)
MyBudget.Application.Abstraction  Shared application abstractions (ICommand marker interface)
MyBudget.Infrastructure.Abstractions  Infrastructure abstractions (IInstaller, etc.)
```

### Frontend workspace structure

```
apps/web           React app (pages, routes, components, hooks)
packages/api-client   Auto-generated Axios client from OpenAPI spec
packages/minimal-ui   Shared MUI-based UI component library
packages/config-eslint / config-typescript  Shared tooling configs
```

---

## Key conventions

### Result pattern (backend)
All business operations return `Result` or `Result<T>` — never throw exceptions for domain errors. In API endpoints, call `.Match(...)` to convert results to HTTP responses:

```csharp
var result = await mediator.SendRequest(new CreateBudgetCommand(...), ct);
return result.Match(x => Results.CreatedAtRoute(nameof(GetBudget), new { id = x }));
```

Error types determine HTTP status: `NotFoundError` → 404, `BadRequestError` / `BusinessRuleValidationError` → 400, `ForbiddenError` → 403. Error codes are serialized as `snake_case` (via Humanizer `.Underscore()`).

### Defining API endpoints (Carter modules)
Each feature group gets an `ICarterModule` in `MyBudget.Api/Features/Core/`. Route handlers are `private static async Task<IResult>` methods, not controller actions. Validation is performed by FluentValidation via a MassTransit mediator filter — validators are auto-discovered by convention.

### CQRS with MassTransit mediator
Commands implement `ICommand` and inherit `Request<Result<T>>`. Handlers inherit `MediatorRequestHandler<TRequest, TResponse>`. Send via `IMediator.SendRequest(...)` (not `ISender`). There are no MediatR dependencies — MassTransit's mediator is used exclusively.

The mediator pipeline (configured in `MediatorInstaller`) applies two filters automatically:
- **`ValidationFilter<>`** — runs all `IValidator<TMessage>` implementations before every handler; throws `ValidationException` (caught by the exception handler and returned as `ValidationProblemDetails`) if validation fails.
- **`UnitOfWorkFilter<>` + `UnitOfWorkResultFilter<>`** — wraps every `ICommand` handler in a unit of work; calls `IUnitOfWork.CommitAsync()` only if the handler returns a successful `Result`. Queries are not wrapped.

### Installer pattern
Service registration uses convention-based auto-discovery. Each class implementing `IInstaller` (from `MyBudget.Infrastructure.Abstractions`) is discovered and invoked automatically when `builder.Install(assembly)` is called in `Program.cs`. Add new services by implementing `IInstaller` in the appropriate assembly — do not edit `Program.cs` directly.

### DDD aggregates
`Budget` is the primary aggregate root. Business rules are encapsulated as `IAsyncBusinessRule` / `IBusinessRule` objects and checked via `CheckRulesAsync(...)` / `CheckRules(...)` on `Entity`. Domain events are raised inside the aggregate and dispatched during persistence.

### EF Core conventions
`BudgetContext` uses `.UseSnakeCaseNamingConvention()` — all column/table names are snake_case in PostgreSQL. Owned entity types are used for value objects. Strong-typed ID wrappers (`BudgetId`, `TransferId`, `UserId`) have custom EF value converters registered globally in `BudgetContext.ConfigureConventions`. Entity configurations are applied via `ApplyConfigurationsFromAssembly` — add new `IEntityTypeConfiguration<T>` classes in `MyBudget.Infrastructure/` rather than configuring inline.

### Current user in application layer
Use `IRequestContext.UserId` (injected into handlers) to access the authenticated user's ID. Do not pass `UserId` as a command/query parameter — it is resolved from the HTTP context by the infrastructure implementation.

### Integration tests
Tests extend `BudgetsIntegrationTest` which uses `IntegrationTestWebAppFactory` (a `WebApplicationFactory<Program>`). Each test suite gets an isolated PostgreSQL database via Testcontainers. Authentication is replaced by `TestAuthHandler` — set `_application.UserId` to control the authenticated user. Use `Bogus` (`Faker`) for test data generation.

### Domain business rules
Business rules are `internal` classes implementing `IAsyncBusinessRule` or `IBusinessRule`, living in `MyBudget.Domain/Budgets/Rules/`. Each rule holds a `private static readonly Error` field named after the rule class (e.g. `BudgetNameMustBeUniqueForUser`). The class name becomes the error code, which is then serialized as `snake_case` in HTTP error responses (e.g. `budget_name_must_be_unique_for_user`). Rules that need infrastructure (like a uniqueness check) receive their dependencies via constructor — the interface for that dependency is defined in the Domain layer and implemented in Infrastructure.

### Identity server — OpenIddict
`MyBudget.Identity` is a standalone ASP.NET Core app using OpenIddict as an OIDC provider backed by ASP.NET Core Identity + a separate `ApplicationDbContext`. The `Worker` hosted service seeds OpenIddict clients and scopes on startup (runs every boot — idempotent because it checks for existence first). To change a client registration, uncomment the `DeleteAsync` block in `Worker.cs`, restart once, then recomment it.

Two scopes are registered:
- `my_budget.api` — used by the frontend; resource is `MyBudget.Backend`
- `my_budget.identity` — used by the API to call the identity server via gRPC

### API ↔ Identity gRPC channel
The API calls the Identity server over gRPC-Web (HTTP/1.1) via `Users.UsersClient` to look up users by email (e.g. for budget sharing). The API authenticates itself using client credentials (`MyBudget.Backend` client, scope `my_budget.identity`) — the token is fetched automatically per-call by `GrpcClientInstaller`. The identity server's URL is configured via `MyBudget.Identity:ApiUrl` in `appsettings.json`.

### Frontend: OIDC auth
Authentication uses `react-oidc-context` wrapping `oidc-client-ts`. The `AuthProvider` is configured in `configuration/` and wraps the app. API calls include the Bearer token via an Axios interceptor in the generated `api-client` package.

### Frontend: forms
Forms use Formik with Yup schemas for validation. MUI components are used for all UI elements. The `@repo/minimal-ui` package provides layout primitives and themed components.

---

## Devops / deployment

All three components share a single reusable Helm chart at `devops/chart/`. Per-component values are in `devops/my-budget-{api,frontend,identity}-chart-overrides.yaml`. The Kubernetes cluster runs on k3s (Hetzner); infrastructure is managed via `helmfile.yaml` for cluster-level services (ingress-nginx, cert-manager, Vault, Zalando postgres-operator, Secrets Store CSI driver).

**Secrets** are stored in HashiCorp Vault and mounted as files via the Secrets Store CSI driver (`spc.enabled: true` in chart overrides). For the API, `appsettings.json` is mounted directly from the Vault path `kv/data/app/mybudget.api/appsettings.json` into `/app/appsettings.json` at runtime.

**Images** are pulled from GitHub Container Registry (`ghcr.io/wardzinskim/my-budget.*`) with tag `main`, built and pushed by the `main-deploy.yml` GitHub Actions workflow.
