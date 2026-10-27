---
description: 'General conventions for the myBudget React web app (apps/web): structure, naming, TypeScript, MUI, formatting'
applyTo: 'frontend/apps/web/src/**/*.{ts,tsx}'
---

# Frontend web app conventions

- Stack: React 19, Vite, TypeScript, MUI 7, react-router-dom 7 (data router), Formik + Yup, notistack, apexcharts, `react-oidc-context`.
- Prettier (`frontend/.prettierrc`): 2 spaces, semicolons, single quotes, `trailingComma: es5`. Lint must pass with `--max-warnings 0`.
- File names kebab-case; one exported component per file; function components typed as `React.FC<Props>` with a `Props` interface.
- Layout of `src/`:
  - `pages/<area>/<page>.tsx` (thin: `Helmet` title + view), `<page>.loader.ts`, `<page>.action.ts`
  - `components/<area>/view/*-view.tsx` (page content), `components/<area>/*.tsx` (tables, forms, lists), `components/dashboard/widgets/*`
  - `routes/router.tsx` (all routes, `Paths` constants), `routes/navigation-config.ts`
  - `configuration/api.ts` (API client instances), `hooks/` (contexts), `layout/`
- Pages are lazy-loaded in `router.tsx` and use `export default`.
- Use MUI components and `sx`/theme palette (no raw CSS colours); reusable UI primitives (`Iconify`, `RouterLink`, `Label`, `Scrollbar`, `fCurrency`, tables, theme) come from `@repo/minimal-ui`.
- Types for API data come from `@repo/api-client` (never redefine DTOs). Do not edit `packages/api-client/lib` by hand.
- Runtime config comes from `window.MyBudgetConfig` (`public/assets/config.js`) via `src/config.ts`; never hardcode URLs or client ids.
- Selected budget lives in `UserContext` (`useUserContext`); dashboard filters in `DashboardContext`.
- Verify: `cd frontend/apps/web && npm run lint && npm run build`.
