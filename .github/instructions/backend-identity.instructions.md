---
description: 'Conventions for MyBudget.Identity: OpenIddict OIDC provider, ASP.NET Identity, gRPC users service, client seeding'
applyTo: 'backend/Identity/**'
---

# Identity server (MyBudget.Identity)

- Standalone ASP.NET Core app: OpenIddict + ASP.NET Core Identity with its own `ApplicationDbContext` and migrations (`Identity/MyBudget.Identity/Migrations`, `Data/Migrations`). Never mix with `BudgetContext` migrations.
- Services are registered through `Installers/*Installer.cs` (`IInstaller`); do not edit `Program.cs` for DI.
- `Worker` seeds scopes and clients (`MyBudget.Frontend` public client with PKCE; `MyBudget.Backend` confidential client) on every start and is idempotent. To change an existing client: uncomment the `DeleteAsync` block, run once, then re-comment. Keep redirect URIs in sync with the frontend origin and `config.js`.
- Scopes: `my_budget.api` (frontend → API), `my_budget.identity` (API → Identity gRPC via client credentials). A new scope must be created in `Worker`, granted to the client permissions (`Permissions.Prefixes.Scope + "<scope>"`) and requested in frontend `oidcScope` / API `OpenIddict:Scopes`.
- gRPC (`grpc/Services/UsersService.cs`, exposed as gRPC-Web): requires a token with `my_budget.identity`. Changing the `.proto` contract requires updating the API client side (`Users.UsersClient` usage in `MyBudget.Infrastructure/Application/Services/UserService.cs`) and its test mock (`MyBudget.Api.Tests/Mocks/UserServiceMock.cs`).
- Never commit real secrets or client secrets; use appsettings placeholders / Vault in deployed environments.
