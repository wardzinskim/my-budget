import {
  FormControl,
  FormHelperText,
  IconButton,
  InputLabel,
  MenuItem,
  Select,
  Stack,
  Tooltip,
} from '@mui/material';
import { DatePicker } from '@mui/x-date-pickers';
import { TransferDTOType } from '@repo/api-client';
import { Iconify } from '@repo/minimal-ui';
import { useFormik } from 'formik';
import { Form, useSearchParams } from 'react-router-dom';

interface TransferFiltersProps {}

interface TransfersFiltersQueryForm {
  type?: TransferDTOType;
  dateFrom?: Date;
  dateTo?: Date;
}

export const TransferFilters: React.FC<TransferFiltersProps> = () => {
  const [searchParams, setSearchParams] = useSearchParams();

  const formik = useFormik<TransfersFiltersQueryForm>({
    initialValues: {
      type: searchParams.get('type')
        ? (searchParams.get('type') as TransferDTOType)
        : undefined,
      dateFrom: searchParams.get('dateFrom')
        ? new Date(searchParams.get('dateFrom')!)
        : undefined,
      dateTo: searchParams.get('dateTo')
        ? new Date(searchParams.get('dateTo')!)
        : undefined,
    },
    onSubmit: (values) => {
      const params = {
        type: values.type?.toString(),
        dateFrom: values.dateFrom?.toISOString(),
        dateTo: values.dateTo?.toISOString(),
      };

      console.log(values, params);
      // eslint-disable-next-line @typescript-eslint/no-unsafe-argument
      setSearchParams(JSON.parse(JSON.stringify(params)));
    },
  });

  return (
    <Stack
      spacing={2}
      alignItems="center"
      justifyContent="space-between"
      margin={2}
      direction={{ xs: 'column', sm: 'row' }}
      component={Form}
      method="post"
      onSubmit={formik.handleSubmit}
    >
      <FormControl fullWidth={true} variant="outlined">
        <InputLabel>Category</InputLabel>
        <Select
          name="type"
          value={formik.values.type}
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          error={formik.touched.type && Boolean(formik.errors.type)}
        >
          <MenuItem value={undefined}>
            <em>Both</em>
          </MenuItem>
          <MenuItem value={TransferDTOType.Income}>Income</MenuItem>
          <MenuItem value={TransferDTOType.Expense}>Expense</MenuItem>
        </Select>
        {formik.touched.type && Boolean(formik.errors.type) && (
          <FormHelperText error={true}>
            {formik.touched.type && formik.errors.type}
          </FormHelperText>
        )}
      </FormControl>
      <FormControl fullWidth={true} variant="outlined">
        <DatePicker
          name="dateFrom"
          label="Date From"
          value={formik.values.dateFrom}
          onChange={(value) => {
            // eslint-disable-next-line @typescript-eslint/no-floating-promises
            formik.setFieldValue('dateFrom', value, true);
          }}
          slotProps={{
            textField: {
              helperText: formik.touched.dateFrom && formik.errors.dateFrom,
              error: formik.touched.dateFrom && Boolean(formik.errors.dateFrom),
              onBlur: formik.handleBlur,
            },
          }}
        />
      </FormControl>
      <FormControl fullWidth={true} variant="outlined">
        <DatePicker
          name="dateTo"
          label="Date To"
          value={formik.values.dateTo}
          onChange={(value) => {
            // eslint-disable-next-line @typescript-eslint/no-floating-promises
            formik.setFieldValue('dateTo', value, true);
          }}
          slotProps={{
            textField: {
              helperText: formik.touched.dateTo && formik.errors.dateTo,
              error: formik.touched.dateTo && Boolean(formik.errors.dateTo),
              onBlur: formik.handleBlur,
            },
          }}
        />
      </FormControl>
      <Tooltip title="Search">
        <IconButton size="large" type="submit" color="inherit">
          <Iconify icon="eva:search-fill" />
        </IconButton>
      </Tooltip>
    </Stack>
  );
};
