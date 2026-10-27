# Form template

```tsx
import { Button, FormControl, Stack, TextField } from '@mui/material';
import { <Request> } from '@repo/api-client';
import { FormikErrors, useFormik } from 'formik';
import { useEffect } from 'react';
import { useFetcher } from 'react-router-dom';
import * as yup from 'yup';

const validationSchema = yup.object({
  name: yup.string().required().max(32),
});

export const <Name>Form = () => {
  const fetcher = useFetcher();
  const formik = useFormik<<Request>>({
    initialValues: { name: '' },
    validationSchema,
    onSubmit: async (values) => {
      await fetcher.submit(
        { intent: 'create', ...values },
        { method: 'POST', encType: 'application/json' }
      );
    },
  });

  useEffect(() => {
    if (fetcher.data) {
      formik.setErrors(fetcher.data as FormikErrors<<Request>>);
    }
  }, [fetcher.data]);

  return (
    <fetcher.Form onSubmit={formik.handleSubmit}>
      <Stack spacing={2}>
        <FormControl fullWidth variant="outlined">
          <TextField
            disabled={fetcher.state === 'submitting'}
            name="name"
            label="Name"
            value={formik.values.name}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={formik.touched.name && Boolean(formik.errors.name)}
            helperText={formik.touched.name && formik.errors.name}
          />
        </FormControl>
        <Button type="submit" variant="contained" disabled={fetcher.state === 'submitting'}>
          Save
        </Button>
      </Stack>
    </fetcher.Form>
  );
};
```
