---
description: 'Conventions for the shared @repo/minimal-ui package (MUI theme and components, Vite lib build)'
applyTo: 'frontend/packages/minimal-ui/**/*.{ts,tsx}'
---

# @repo/minimal-ui

- Source in `lib/`; public API is only what `lib/index.ts` (via `components/index.ts`, `layouts`, `theme`, `hooks`, `routes`, `utils`) exports. Each component dir: `<name>.tsx`, optional `styles.ts`, `index.ts` re-export; also add to `components/index.ts`.
- MUI/emotion/iconify/react are `peerDependencies` — do not bundle or add them as dependencies.
- Components are generic and app-agnostic: no `@repo/api-client` types, no budget logic, no router data hooks; use the `RouterLink` and `useRouter`/`usePathname` wrappers in `lib/routes`.
- Styling through theme tokens (`palette`, `customShadows`, `typography`); extend theme types in `theme/theme-augmentation.d.ts`.
- After changes: `cd frontend/packages/minimal-ui && npm run lint && npm run build`. The web app consumes `dist/`, so rebuild (or run `npm run dev` at `frontend/` via turbo) before testing in `apps/web`.
