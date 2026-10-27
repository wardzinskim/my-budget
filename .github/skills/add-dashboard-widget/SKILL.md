---
name: add-dashboard-widget
description: 'Add a dashboard widget (stat card or apexcharts chart) to the myBudget dashboard, extending the dashboard loader and view. Use when asked to "add widget", "dodaj widget", "new chart on dashboard", "dashboard statistic".'
allowed-tools: shell
---

# add-dashboard-widget

Add a presentational widget fed by the dashboard loader.

## When to Use This Skill

- User says "add a chart/card to the dashboard", "dodaj widget"
- Keywords: dashboard, widget, apexcharts, statistics, BudgetTotals

## Steps

### 1. Ensure data is available

Statistics come from `statisticsApi` (`BudgetStatisticsApi`). If a new endpoint is required, use backend skill `add-api-feature` then `update-api-client`.

### 2. Extend the loader

In `src/pages/dashboard/dashboard.loader.ts`: add a `fetch...` helper, include it in the `Promise.all`, add the field to `DashboardLoaderResult` and to the returned object. Respect `dashboardContext.year/month`.

### 3. Create the widget

`src/components/dashboard/widgets/<name>.tsx`: props-only component (data in via props, no fetching).
- Stat cards: copy `total-expenses-viewer.tsx` (gradient `Stack`, `Iconify`, `fCurrency`).
- Charts: use `Chart`/`useChart` from `@repo/minimal-ui` with `react-apexcharts` (see `expenses-by-category-pie-chart.tsx`).

### 4. Add to the view

Render it in `components/dashboard/view/dashboard-view.tsx` inside the MUI `Grid`/`Stack` layout using data from `useLoaderData<DashboardLoaderResult>()`.

### 5. Verify

```bash
cd frontend/apps/web
npm run lint
npm run build
```

## Requirements

- Widgets are pure and typed with generated models (`CategoryValue`, `BudgetTotals`, ...).
- Colours from theme palette; amounts formatted with `fCurrency`.
- Handle empty arrays (no data) gracefully.
- Do NOT fetch inside widgets.

## Good Example

`widgets/total-expenses-viewer.tsx`, `widgets/expenses-by-category-bar-chart.tsx`.

## What to Avoid

- Sequential awaits in the loader (use `Promise.all`).
- Hardcoded currency/colours.

## Error handling

- Widget empty after filter change → verify loader reads `dashboardContext` and router is recreated with the new context (`Router` in `router.tsx`).
