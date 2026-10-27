---
description: 'Data flow in apps/web: react-router loaders/actions, API calls, error and validation handling'
applyTo: 'frontend/apps/web/src/{pages,routes,configuration}/**/*.{ts,tsx}'
---

# Routing, loaders and actions

- Data fetching happens in **loaders**, mutations in **actions** (react-router data router). Components read data with `useLoaderData<T>()`; do not fetch in `useEffect`.
- Loaders/actions that need the selected budget are factories: `(context: IUserContextState) => LoaderFunction` registered in `router.tsx` as `loader: x(userContext)`. Loader returns `null` if `context.budget` is unset; action throws `Error('budget context is not selected')`.
- Call the API only through instances from `configuration/api.ts` (`budgetApi`, `transferApi`, `categoryApi`, `statisticsApi`); add a new instance there when a new generated `*Api` class appears. Bearer token is injected from the OIDC storage automatically.
- Parallel independent calls: `Promise.all` (see `dashboard.loader.ts`).
- Route params typing: `Params<ParamParseKey<typeof Paths.<route>>>`; add path constants to `Paths` in `router.tsx`.
- Actions read JSON: `await request.json()` typed as the generated request model; multi-intent actions switch on `intent`.

## Action error handling (backend ProblemDetails)
```ts
.catch((error: AxiosError<HttpValidationProblemDetails>) => {
  if (error.response?.data.type == 'ValidationException') {
    enqueueSnackbar('Validation Errors Occurred', { variant: 'error' });
    return error.response.data.errors;          // forms show them via formik.setErrors
  }
  enqueueSnackbar(error.response?.data.detail, { variant: 'error' });
  return {};
});
```
Success: `enqueueSnackbar({ message, variant: 'success' })` then `redirect(...)` or return data. Business errors have `data.code` (snake_case) and `detail`.

## Router
- Add routes to `createBrowserRouter` inside `AppLayout` children; lazy page export at the top of `router.tsx`; nav entry in `navigation-config.ts` (icon from `public/assets/icons/navbar`).
- Use `errorElement` for pages that need custom error UI (e.g. `TransferErrorPage`).
