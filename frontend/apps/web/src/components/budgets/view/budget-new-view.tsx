import {
  Box,
  Button,
  Card,
  CardActions,
  CardContent,
  Container,
  FormControl,
  Stack,
  TextField,
  Typography,
} from '@mui/material';
import { RouterLink } from '@repo/minimal-ui';
import { Form, useActionData, useSubmit } from 'react-router-dom';
import { FormikErrors, useFormik } from 'formik';
import { CreateBudgetRequest } from '@repo/api-client';
import * as yup from 'yup';
import { useEffect } from 'react';

interface BudgetNewViewProps {}

const validationSchema = yup.object({
  name: yup.string().required().max(50),
  description: yup.string().max(256),
});

export const BudgetNewView: React.FC<BudgetNewViewProps> = () => {
  const submit = useSubmit();
  const action = useActionData() as FormikErrors<CreateBudgetRequest>;

  const formik = useFormik<CreateBudgetRequest>({
    initialValues: {
      name: '',
      description: undefined,
    },
    validationSchema: validationSchema,
    onSubmit: (values) => {
      submit(
        {
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
    if (action) {
      formik?.setErrors(action);
    }
  }, [action, formik]);

  return (
    <>
      <Container>
        <Stack
          direction="row"
          alignItems="center"
          justifyContent="space-between"
          mb={5}
        >
          <Typography variant="h4">Create new Budget</Typography>
        </Stack>

        <Card
          component={Form}
          method="post"
          action="/budgets/new"
          onSubmit={formik.handleSubmit}
        >
          <CardContent>
            <Box columnGap={2} rowGap={2} gridColumn={2} display="grid">
              <FormControl fullWidth={true} variant="outlined">
                <TextField
                  name="name"
                  label="Name"
                  value={formik.values.name}
                  onChange={formik.handleChange}
                  onBlur={formik.handleBlur}
                  error={formik.touched.name && Boolean(formik.errors.name)}
                  helperText={formik.touched.name && formik.errors.name}
                />
              </FormControl>

              <FormControl fullWidth={true} variant="outlined">
                <TextField
                  multiline={true}
                  name="description"
                  label="Description"
                  value={formik.values.description}
                  onChange={formik.handleChange}
                  onBlur={formik.handleBlur}
                  error={
                    formik.touched.description &&
                    Boolean(formik.errors.description)
                  }
                  helperText={
                    formik.touched.description && formik.errors.description
                  }
                />
              </FormControl>
            </Box>
          </CardContent>
          <CardActions>
            <Button
              component={RouterLink}
              href="/budgets"
              variant="outlined"
              color="inherit"
            >
              Cancel
            </Button>
            <Button variant="contained" color="inherit" type="submit">
              Create
            </Button>
          </CardActions>
        </Card>
      </Container>
    </>
  );
};
