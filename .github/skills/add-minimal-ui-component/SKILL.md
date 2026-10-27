---
name: add-minimal-ui-component
description: 'Add or change a reusable component in the shared @repo/minimal-ui package (MUI-based library) and export it for apps/web. Use when asked to "add shared component", "dodaj komponent do minimal-ui", "reusable UI", "update theme".'
allowed-tools: shell
---

# add-minimal-ui-component

Create an app-agnostic component in `frontend/packages/minimal-ui`.

## When to Use This Skill

- User says "add shared/reusable component", "dodaj komponent do minimal-ui"
- UI primitive needed in more than one place, or theme/layout change
- Keywords: minimal-ui, theme, component library, Iconify, Label

## Steps

### 1. Check for an existing component

Browse `packages/minimal-ui/lib/components`; prefer composing/extending over duplicating. Business-specific UI belongs in `apps/web/src/components`, not here.

### 2. Create the component

`lib/components/<name>/<name>.tsx` (+ `styles.ts` if needed) and `lib/components/<name>/index.ts` (`export * from './<name>'`). Add `export * from './<name>';` to `lib/components/index.ts`.

### 3. Rules

- Props interface exported; MUI `sx`/theme tokens; no API client types or router data hooks.
- No new runtime dependency without adding it as a `peerDependency` (see `package.json`).

### 4. Build

```bash
cd frontend/packages/minimal-ui
npm run lint
npm run build
```

### 5. Consume

`import { <Name> } from '@repo/minimal-ui';` in `apps/web`, then `cd ../../apps/web && npm run build`.

## Requirements

- Follow `.github/instructions/frontend-minimal-ui.instructions.md`.
- Do NOT import from `apps/web`.

## Good Example

`lib/components/label/` (`label.tsx`, `styles.ts`, `index.ts`).

## What to Avoid

- Forgetting the barrel exports → component missing in `dist/index.d.ts`.
- Testing in the app without rebuilding the package.

## Error handling

- "Module has no exported member" in web → rebuild minimal-ui and check `components/index.ts`.
