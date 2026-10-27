---
name: update-api-client
description: 'Regenerate the frontend TypeScript API client (packages/api-client) from the backend OpenAPI spec after API contract changes in myBudget. Use when asked to "update api client", "regenerate swagger", "odśwież klienta API", or after adding/changing endpoints or DTOs.'
allowed-tools: shell
---

# update-api-client

Sync `frontend/packages/api-client` with the backend OpenAPI contract.

## When to Use This Skill

- User says "update API client", "zregeneruj klienta", "odśwież swagger.json"
- An endpoint, request record or DTO changed in `MyBudget.Api`/`MyBudget.Application`
- Keywords: swagger.json, openapi-generator, typescript-axios, api-client

## Steps

### 1. Run the API in Development

```bash
cd backend/MyBudget.Api
dotnet run --launch-profile http
```

Swagger is served at `http://localhost:5035/swagger/v1/swagger.json` (check `Properties/launchSettings.json` if the port differs; requires Postgres from `.github/docker-compose.yml` or local config).

### 2. Update the spec

```bash
curl -o frontend/packages/api-client/swagger.json http://localhost:5035/swagger/v1/swagger.json
```

Stop the API afterwards (`Stop-Process -Id <PID>`).

### 3. Regenerate and build

```bash
cd frontend/packages/api-client
npm run build
```

### 4. Fix consumers

```bash
cd frontend
npm run lint
npm run build
```

Update `apps/web` code that uses changed operations/models.

### 5. Review diff

`git diff --stat frontend/packages/api-client` should show only changes related to the backend change; `lib/` is checked in and must be committed.

## Requirements

- Only routes with `.IncludeInOpenApi()` appear in the spec.
- Do NOT edit files in `lib/` manually; they are generated.
- Keep the spec formatting produced by Swagger (avoid reformatting noise).

## Good Example

New endpoint with `.WithName(nameof(X))` → generated method `xApi.x(...)` in `lib/api`.

## What to Avoid

- Committing `swagger.json` without regenerating `lib/`.
- Regenerating from a stale API build.

## Error handling

- Generator fails (Java missing) → install a JRE; `openapi-generator-cli` needs Java.
- Spec missing a route → add `.IncludeInOpenApi()` to the group/route and rebuild.
- Type errors in `apps/web` after regeneration → adapt the calls to the new models.
