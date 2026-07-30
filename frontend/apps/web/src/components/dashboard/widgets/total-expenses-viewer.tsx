import { Box, Stack, Typography, alpha } from '@mui/material';
import { TransferDTOType } from '@repo/api-client';
import { Iconify, fCurrency } from '@repo/minimal-ui';

interface TotalExpensesViewerProps {
  value: number;
}

export const TotalExpensesViewer: React.FC<TotalExpensesViewerProps> = ({
  value,
}) => {
  return (
    <Stack
      sx={{
        background: (theme) =>
          `linear-gradient(135deg, ${alpha(theme.palette.warning.light, 0.2)}, ${alpha(
            theme.palette.warning.main,
            0.2
          )}) ${theme.palette.background.paper}`,
        color: 'text.primary',
        position: 'relative',
      }}
      borderRadius={2}
    >
      <Stack padding={3}>
        <Box
          component={Iconify}
          icon={'tdesign:arrow-right-up'}
          sx={{
            bgcolor: (theme) => alpha(theme.palette.warning.main, 0.16),
            color: 'warning.dark',
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
