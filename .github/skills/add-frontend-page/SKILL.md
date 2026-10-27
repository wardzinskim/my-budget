---
name: add-frontend-page
description: 'Add a new page to the myBudget React app: route, lazy page, view component, loader and action using react-router data APIs and the generated API client. Use when asked to "add page", "dodaj stronę", "new screen", "nowy widok", "add route".'
allowed-tools: shell
---

# add-frontend-page

Scaffold a routed page (page + view + loader/action + route + optional nav item).

## When to Use This Skill

- User says "add page for X", "dodaj stronę/widok", "add route"
- A new screen that lists, shows or edits backend data
- Keywords: page, route, loader, action, view, router.tsx

## Steps

### 1. Confirm the API exists

Needed operation must be in `packages/api-client` (skill `update-api-client`) and exposed as an instance in `apps/web/src/configuration/api.ts`.

### 2. Create files (kebab-case)

- `src/pages/<area>/<page>.tsx` — see [templates/page.tsx.md](templates/page.tsx.md)
- `src/pages/<area>/<page>.loader.ts` / `<page>.action.ts` — see [templates/loader-action.ts.md](templates/loader-action.ts.md)
- `src/components/<area>/view/<page>-view.tsx` — content using `useLoaderData<T>()`, `Container`, `Stack`, `Typography variant="h4"`, `Card`, `RouterLink`/`Iconify` from `@repo/minimal-ui`.

### 3. Register the route

In `src/routes/router.tsx`: add `export const <Name>Page = lazy(() => import('...'))`, a `Paths` entry if it has params, and the route object under `AppLayout` children with `loader`/`action` (factories receive `userContext`).

### 4. Navigation (optional)

Add an item to `src/routes/navigation-config.ts`; icons live in `public/assets/icons/navbar/*.svg`.

### 5. Verify

```bash
cd frontend/apps/web
npm run lint
npm run build
```

## Requirements

- Follow `.github/instructions/frontend-routing-data.instructions.md`.
- Pages stay thin: `Helmet` title (`<Title> | myBudget`) + view.
- Fetch in loaders, mutate in actions; no `useEffect` fetching.
- Show backend errors via `enqueueSnackbar`; return `errors` for validation.
- Do NOT hardcode backend URLs or redefine DTO types.

## Good Example

`pages/transfers/edit/*` + `components/transfers/view/transfer-edit-view.tsx`.

## What to Avoid

- Fetching with axios directly in components.
- Forgetting the null guard when `userContext.budget` is unset.

## Error handling

- `tsc` error on route params → type `params` with `ParamParseKey<typeof Paths.x>`.
- Page blank after reload → loader returned `null` because no budget selected; handle in the view.
