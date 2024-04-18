import {
  Box,
  Button,
  Card,
  CardActions,
  CardContent,
  Container,
  FormControl,
  Grid,
  Stack,
  TextField,
  Typography,
} from '@mui/material';
import { RouterLink } from '@repo/minimal-ui';
import { Form, useActionData, useSubmit } from 'react-router-dom';
import { FormikErrors, useFormik } from 'formik';
import { CreateTransferRequest, TransferDTOType } from '@repo/api-client';
import * as yup from 'yup';
import { useEffect } from 'react';
import { DateTimePicker } from '@mui/x-date-pickers';

interface TransferNewViewProps {
  type: TransferDTOType;
}

const validationSchema = yup.object({
  name: yup.string().required().max(128),
  category: yup.string().max(32).nullable(),
  currency: yup.string().required().max(8),
  value: yup.number().min(0),
  date: yup.date().nullable(),
});

export const TransferNewView: React.FC<TransferNewViewProps> = ({ type }) => {
  const submit = useSubmit();
  const action = useActionData() as FormikErrors<CreateTransferRequest>;

  const formik = useFormik<CreateTransferRequest>({
    initialValues: {
      name: '',
      category: null,
      type: type,
      value: undefined,
      currency: 'PLN',
      date: null,
    },
    validationSchema: validationSchema,
    onSubmit: (values) => {
      submit(
        {
          ...values,
          date: values.date?.toJSON() ?? null,
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
          <Typography variant="h4">Create new {type}</Typography>
        </Stack>

        <Card component={Form} method="post" onSubmit={formik.handleSubmit}>
          <CardContent>
            <Box sx={{ flexGrow: 1 }}>
              <Grid container spacing={2}>
                <Grid item xs={12}>
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
                </Grid>

                <Grid item md={6} xs={12}>
                  <FormControl fullWidth={true} variant="outlined">
                    <TextField
                      type="number"
                      name="value"
                      label="Value"
                      value={formik.values.value}
                      onChange={formik.handleChange}
                      onBlur={formik.handleBlur}
                      error={
                        formik.touched.value && Boolean(formik.errors.value)
                      }
                      helperText={formik.touched.value && formik.errors.value}
                    />
                  </FormControl>
                </Grid>
                <Grid item md={6} xs={12}>
                  <FormControl fullWidth={true} variant="outlined">
                    <TextField
                      disabled
                      name="currency"
                      label="Currency"
                      value={formik.values.currency}
                      onChange={formik.handleChange}
                      onBlur={formik.handleBlur}
                      error={
                        formik.touched.currency &&
                        Boolean(formik.errors.currency)
                      }
                      helperText={
                        formik.touched.currency && formik.errors.currency
                      }
                    />
                  </FormControl>
                </Grid>

                <Grid xs={12} item>
                  <FormControl fullWidth={true} variant="outlined">
                    <DateTimePicker
                      name="date"
                      label="Date"
                      value={formik.values.date}
                      onChange={(value) => {
                        // eslint-disable-next-line @typescript-eslint/no-floating-promises
                        formik.setFieldValue('date', value, true);
                      }}
                      slotProps={{
                        textField: {
                          helperText: formik.touched.date && formik.errors.date,
                          error:
                            formik.touched.date && Boolean(formik.errors.date),
                          onBlur: formik.handleBlur,
                        },
                      }}
                    />
                  </FormControl>
                </Grid>
              </Grid>
            </Box>
          </CardContent>
          <CardActions>
            <Button
              component={RouterLink}
              href="/transfers"
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
