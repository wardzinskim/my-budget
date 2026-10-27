---
name: add-frontend-form
description: 'Create a Formik + Yup form in the myBudget React app that submits through a react-router fetcher/Form to an action and shows server validation errors. Use when asked to "add form", "dodaj formularz", "create/edit dialog form", "form validation".'
allowed-tools: shell
---

# add-frontend-form

Build a validated MUI form wired to a route action.

## When to Use This Skill

- User says "add a form to create/edit X", "dodaj formularz"
- Keywords: Formik, Yup, useFetcher, TextField, validation

## Steps

### 1. Pick the request model

Use the generated type from `@repo/api-client` (e.g. `CreateBudgetCategoryRequest`). Mirror backend FluentValidation limits in Yup.

### 2. Create the component

`src/components/<area>/<name>-form.tsx` — see [templates/form.tsx.md](templates/form.tsx.md). Rules in `.github/instructions/frontend-forms.instructions.md`.

### 3. Handle submit in an action

Route action reads `await request.json()`, calls the API and returns `errors` on `ValidationException` (skill `add-frontend-page`, template `loader-action.ts.md`). For several forms on one route use an `intent` field and branch in the action.

### 4. Place the form

Render it in a view component; for edit pass `initialData` from `useLoaderData`.

### 5. Verify

```bash
cd frontend/apps/web
npm run lint
npm run build
```

## Requirements

- Every field: `value`, `onChange`, `onBlur`, `error`, `helperText`; disable while `fetcher.state === 'submitting'`.
- Server errors set via `formik.setErrors(fetcher.data)`.
- Do NOT call the API directly from `onSubmit`.

## Good Example

`components/budgets/budget-category-form.tsx`.

## What to Avoid

- Validation rules only on the client (backend remains the authority).
- Untyped `values`.

## Error handling

- Server errors not shown → backend error keys are camelCase/lower-case property names; match form field `name`.
- Infinite re-render from `useEffect` → depend on `fetcher.data`, not on a new object.
