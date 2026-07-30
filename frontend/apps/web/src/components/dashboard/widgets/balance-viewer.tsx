import { Box, Stack, Typography, alpha } from '@mui/material';
import { Iconify, fCurrency } from '@repo/minimal-ui';

interface BalanceViewerProps {
  value: number;
}

export const BalanceViewer: React.FC<BalanceViewerProps> = ({ value }) => {
  const isPositive = value >= 0;
  const accentColor = isPositive ? 'success' : 'error';

  return (
    <Stack
      sx={{
        background: (theme) =>
          `linear-gradient(135deg, ${alpha(theme.palette[accentColor].light, 0.24)}, ${alpha(
            theme.palette[accentColor].main,
            0.24
          )}) ${theme.palette.background.paper}`,
        color: isPositive ? 'text.primary' : 'error.dark',
        position: 'relative',
      }}
      borderRadius={2}
    >
      <Stack padding={3}>
        <Box
          component={Iconify}
          icon={isPositive ? 'fa6-solid:plus' : 'fa6-solid:minus'}
          sx={{
            bgcolor: (theme) => alpha(theme.palette[accentColor].main, 0.16),
            color: `${accentColor}.dark`,
            width: 48,
            height: 48,
            top: 24,
            right: 24,
            position: 'absolute',
          }}
          borderRadius={3}
          padding={1}
        ></Box>

        <Typography variant="subtitle2">Balance</Typography>
        <Typography variant="h3">{fCurrency(value)} PLN</Typography>
      </Stack>
    </Stack>
  );
};
