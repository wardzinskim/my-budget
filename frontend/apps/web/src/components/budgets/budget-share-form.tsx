import { FormControl, ListItem, TextField } from '@mui/material';
import { ShareBudgetRequest } from '@repo/api-client';
import { FormikErrors, useFormik } from 'formik';
import { useEffect } from 'react';
import { useFetcher } from 'react-router-dom';
import * as yup from 'yup';

const validationSchema = yup.object({
  login: yup.string().email().required().max(32),
});

export const BudgetShareForm = () => {
  const fetcher = useFetcher();
  const formik = useFormik<ShareBudgetRequest>({
    initialValues: {
      login: '',
    },
    validationSchema: validationSchema,
    onSubmit: async (values) => {
      await fetcher.submit(
        {
          intent: 'share',
          ...values,
        },
        {
          method: 'POST',
          encType: 'application/json',
        }
      );
    },
  });

  useEffect(() => {
    if (fetcher.data) {
      formik?.setErrors(fetcher.data as FormikErrors<ShareBudgetRequest>);
    }
  }, [fetcher.data, formik]);

  return (
    <ListItem onSubmit={formik.handleSubmit} component={fetcher.Form}>
      <FormControl fullWidth={true} variant="outlined">
        <TextField
          disabled={fetcher.state === 'submitting'}
          name="login"
          label="Login"
          value={formik.values.login}
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          error={formik.touched.login && Boolean(formik.errors.login)}
          helperText={formik.touched.login && formik.errors.login}
        />
      </FormControl>
    </ListItem>
  );
};
