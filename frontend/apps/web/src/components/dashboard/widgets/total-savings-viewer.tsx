import { Box, Stack, Typography, alpha } from '@mui/material';
import { TransferDTOType } from '@repo/api-client';
import { Iconify, fCurrency } from '@repo/minimal-ui';

interface TotalSavingsViewerProps {
  value: number;
  yearlyValue: number;
}

export const TotalSavingsViewer: React.FC<TotalSavingsViewerProps> = ({
  value,
  yearlyValue,
}) => {
  return (
    <Stack
      sx={{
        background: (theme) =>
          `linear-gradient(135deg, ${alpha(theme.palette.info.light, 0.2)}, ${alpha(
            theme.palette.info.main,
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
          icon={'vaadin:piggy-bank-coin'}
          sx={{
            bgcolor: (theme) => alpha(theme.palette.info.main, 0.16),
            color: 'info.dark',
            width: 48,
            height: 48,
            top: 24,
            right: 24,
            position: 'absolute',
          }}
          borderRadius={3}
          padding={1}
        ></Box>

        <Typography variant="subtitle2">{TransferDTOType.Savings}</Typography>
        <Typography variant="h3">
          {value == 0 ? 0 : fCurrency(value)} PLN{' '}
          <Typography variant="subtitle1" component="span">
            month
          </Typography>
        </Typography>

        <Typography variant="h4">
          {yearlyValue == 0 ? 0 : fCurrency(yearlyValue)} PLN{' '}
          <Typography variant="subtitle2" component="span">
            year
          </Typography>
        </Typography>
      </Stack>
    </Stack>
  );
};
