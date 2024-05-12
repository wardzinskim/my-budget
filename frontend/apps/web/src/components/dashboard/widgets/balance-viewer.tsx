import { Box, Stack, Typography } from '@mui/material';
import { Iconify, fCurrency } from '@repo/minimal-ui';

interface BalanceViewerProps {
  value: number;
}

export const BalanceViewer: React.FC<BalanceViewerProps> = ({ value }) => {
  const isPositive = value >= 0;

  const iconColors = isPositive
    ? { backgroundColor: 'rgb(0, 96, 83)', color: 'rgb(200, 250, 214)' }
    : { backgroundColor: 'rgb(183, 0, 0)', color: 'rgb(255, 188, 188)' };

  return (
    <Stack
      sx={{
        background: isPositive
          ? 'linear-gradient(135deg, rgba(97, 197, 73, 0.4), rgba(70, 120, 25, 0.4)) rgb(255, 255, 255)'
          : 'linear-gradient(135deg, rgba(255, 102, 102, 0.4), rgba(255, 0, 0, 0.4)) rgb(255, 255, 255)',
        color: isPositive ? 'rgb(33, 43, 54)' : 'rgb(153, 1, 0)',
        position: 'relative',
      }}
      borderRadius={2}
    >
      <Stack padding={3}>
        <Box
          component={Iconify}
          icon={isPositive ? 'fa6-solid:plus' : 'fa6-solid:minus'}
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

        <Typography variant="subtitle2">Balance</Typography>
        <Typography variant="h3">{fCurrency(value)} PLN</Typography>
      </Stack>
    </Stack>
  );
};
