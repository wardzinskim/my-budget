import {
  Box,
  Button,
  Card,
  CardActions,
  CardContent,
  FormControl,
  FormHelperText,
  Grid,
  InputLabel,
  Select,
  TextField,
  MenuItem,
} from '@mui/material';
import { DateTimePicker } from '@mui/x-date-pickers';
import { RouterLink } from '@repo/minimal-ui';
import { FormikErrors, useFormik } from 'formik';
import { useEffect } from 'react';
import { Form, useActionData, useSubmit } from 'react-router-dom';
import { useUserContext } from '../../hooks/user-context';
import {
  CategoryDTOStatus,
  CreateTransferRequest,
  TransferDTO,
  TransferDTOType,
  UpdateTransferRequest,
} from '@repo/api-client';
import * as yup from 'yup';

interface TransferFormProps {
  type: TransferDTOType;
  initialData?: TransferDTO;
  submitButtonName: string;
}

type TransferRequest = CreateTransferRequest | UpdateTransferRequest;

const validationSchema = yup.object({
  name: yup.string().required().max(128),
  category: yup.string().max(32).nullable(),
  currency: yup.string().required().max(8),
  value: yup.number().min(0).required(),
  date: yup.date().nullable(),
});

export const TransferForm: React.FC<TransferFormProps> = ({
  type,
  initialData,
  submitButtonName,
}) => {
  const submit = useSubmit();
  const action = useActionData() as FormikErrors<TransferRequest>;
  const [userContext] = useUserContext();

  const formik = useFormik<TransferRequest>({
    initialValues: initialData
      ? {
          name: initialData.name,
          category: initialData.category,
          type: type,
          value: initialData.value,
          currency: initialData.currency,
          date: new Date(initialData.transferDate!),
        }
      : {
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
                  error={formik.touched.value && Boolean(formik.errors.value)}
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
                    formik.touched.currency && Boolean(formik.errors.currency)
                  }
                  helperText={formik.touched.currency && formik.errors.currency}
                />
              </FormControl>
            </Grid>

            <Grid md={6} xs={12} item>
              <FormControl fullWidth={true} variant="outlined">
                <InputLabel>Category</InputLabel>
                <Select
                  name="category"
                  value={formik.values.category}
                  onChange={formik.handleChange}
                  onBlur={formik.handleBlur}
                  error={
                    formik.touched.category && Boolean(formik.errors.category)
                  }
                >
                  <MenuItem value={null!}>
                    <em>None</em>
                  </MenuItem>
                  {userContext.budget?.categories
                    ?.filter(
                      (category) => category.status === CategoryDTOStatus.Active
                    )
                    ?.map((category) => (
                      <MenuItem key={category.name} value={category.name!}>
                        {category.name}
                      </MenuItem>
                    ))}
                </Select>
                {formik.touched.category && Boolean(formik.errors.category) && (
                  <FormHelperText error={true}>
                    {formik.touched.category && formik.errors.category}
                  </FormHelperText>
                )}
              </FormControl>
            </Grid>

            <Grid md={6} xs={12} item>
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
                      error: formik.touched.date && Boolean(formik.errors.date),
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
          {submitButtonName}
        </Button>
      </CardActions>
    </Card>
  );
};
