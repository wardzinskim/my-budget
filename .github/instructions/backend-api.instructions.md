---
description: 'Carter modules, request contracts, installers and MCP tools in MyBudget.Api'
applyTo: 'backend/MyBudget.Api/**/*.cs'
---

# API layer

- Routes: `ICarterModule` in `Features/Core/*Module.cs`; `app.MapGroup(...).WithTags(...).IncludeInOpenApi().RequireAuthorization()`.
- Each route: `.WithName(nameof(Handler))` + `.Produces(...)`, `.ProducesProblem(404/403)`, `.ProducesValidationProblem()` as applicable. `IncludeInOpenApi` is required for the route to appear in Swagger.
- Handlers: `private static async Task<IResult> Name(IMediator mediator, [FromRoute] Guid id, [FromBody] XRequest request, CancellationToken cancellationToken)` → `mediator.SendRequest(new XCommand(...), cancellationToken)` → `result.Match(...)`.
- Request contracts are records in `Features/Core/Requests.cs`; do not use Application commands as request bodies. Route ids via `[FromRoute]`.
- Creation returns `Results.CreatedAtRoute(nameof(Get...), new { id })` or `Results.Created`.
- MCP tools in `MCP/BudgetTools.cs` (skill `add-mcp-tool`). After any contract change update the frontend client (skill `update-api-client`).
- Services: new `IInstaller` in `Installers/`; do not edit `Program.cs`.
- Error mapping: see `backend-error-handling.instructions.md`.
