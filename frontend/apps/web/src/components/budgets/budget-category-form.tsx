import { FormControl, ListItem, TextField } from '@mui/material';
import { CreateBudgetCategoryRequest } from '@repo/api-client';
import { FormikErrors, useFormik } from 'formik';
import { useEffect } from 'react';
import { useFetcher } from 'react-router-dom';
import * as yup from 'yup';

const validationSchema = yup.object({
  name: yup.string().required().max(32),
});

export const BudgetCategoryForm = () => {
  const fetcher = useFetcher();
  const formik = useFormik<CreateBudgetCategoryRequest>({
    initialValues: {
      name: '',
    },
    validationSchema: validationSchema,
    onSubmit: (values) => {
      fetcher.submit(
        {
          intent: 'create',
          ...values,
        },
        {
          method: 'POST',
        }
      );
    },
  });

  useEffect(() => {
    if (fetcher.data) {
      formik?.setErrors(
        fetcher.data as FormikErrors<CreateBudgetCategoryRequest>
      );
    }
  }, [fetcher.data, formik]);

  return (
    <ListItem onSubmit={formik.handleSubmit} component={fetcher.Form}>
      <FormControl fullWidth={true} variant="outlined">
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
    </ListItem>
  );
};
