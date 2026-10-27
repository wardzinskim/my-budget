---
description: 'Form conventions in apps/web: Formik + Yup + react-router fetcher/Form + MUI'
applyTo: 'frontend/apps/web/src/components/**/*{form,filters}*.tsx'
---

# Forms

- `useFormik<GeneratedRequestType>` with a Yup `validationSchema` defined at module scope; keep Yup limits in sync with backend FluentValidation validators (e.g. category name max 32).
- Submit via react-router: `const fetcher = useFetcher()`; `onSubmit` → `fetcher.submit({ intent, ...values }, { method: 'POST', encType: 'application/json' })`. Use `component={fetcher.Form}` (or `<Form>` for full-page navigation forms).
- Server validation: action returns `error.response.data.errors`; in the form `useEffect(() => { if (fetcher.data) formik.setErrors(fetcher.data as FormikErrors<T>) }, [fetcher.data])`.
- MUI fields: `name`, `value`, `onChange`, `onBlur`, `error={touched && Boolean(errors)}`, `helperText`, `disabled={fetcher.state === 'submitting'}`.
- Edit forms receive `initialData` from loader data (`useLoaderData`) and a `submitButtonName`.
- Dates: `@mui/x-date-pickers` + `date-fns`; numbers: `numeral`/`fCurrency` from minimal-ui.
- Reference: `components/budgets/budget-category-form.tsx`, `components/transfers/transfer-form.tsx`.
