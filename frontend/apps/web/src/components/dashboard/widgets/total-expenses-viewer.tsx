import { Box, Stack, Typography } from '@mui/material';
import { TransferDTOType } from '@repo/api-client';
import { Iconify, fCurrency } from '@repo/minimal-ui';

interface TotalExpensesViewerProps {
  value: number;
}

export const TotalExpensesViewer: React.FC<TotalExpensesViewerProps> = ({
  value,
}) => {
  const iconColors = {
    backgroundColor: 'rgb(183, 110, 0)',
    color: 'rgb(255, 245, 204)',
  };

  return (
    <Stack
      sx={{
        background:
          'linear-gradient(135deg, rgba(255, 214, 102, 0.2), rgba(255, 171, 0, 0.2)) rgb(255, 255, 255)',
        color: 'rgb(122, 65, 0)',
        position: 'relative',
      }}
      borderRadius={2}
    >
      <Stack padding={3}>
        <Box
          component={Iconify}
          icon={'tdesign:arrow-right-up'}
          sx={{
            ...iconColors,
            width: 48,
            height: 48,
            top: 24,
            right: 24,
            position: 'absolute',
          }}
          borderRadius={3}
          padding={1}
        ></Box>

        <Typography variant="subtitle2">{TransferDTOType.Expense}</Typography>
        <Typography variant="h3">
          {value == 0 ? 0 : fCurrency(value)} PLN
        </Typography>
      </Stack>
    </Stack>
  );
};
