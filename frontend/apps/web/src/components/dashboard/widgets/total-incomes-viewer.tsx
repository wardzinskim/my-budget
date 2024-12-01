import { Box, Stack, Typography } from '@mui/material';
import { TransferDTOType } from '@repo/api-client';
import { Iconify, fCurrency } from '@repo/minimal-ui';

interface TotalIncomesViewerProps {
  value: number;
  tax: number;
}

export const TotalIncomesViewer: React.FC<TotalIncomesViewerProps> = ({
  value,
  tax,
}) => {
  const iconColors = {
    backgroundColor: 'rgb(0, 120, 103)',
    color: 'rgb(200, 250, 214)',
  };

  return (
    <Stack
      sx={{
        background:
          'linear-gradient(135deg, rgba(91, 228, 155, 0.2), rgba(0, 167, 111, 0.2)) rgb(255, 255, 255)',
        color: 'rgb(33, 43, 54)',
        position: 'relative',
      }}
      borderRadius={2}
    >
      <Stack padding={3}>
        <Box
          component={Iconify}
          icon={'tdesign:arrow-left-down'}
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

        <Typography variant="subtitle2">{TransferDTOType.Income}</Typography>
        <Typography variant="h3">
          {value == 0 ? 0 : fCurrency(value - tax)} PLN{' '}
          <Typography variant="subtitle1" component="span">
            net
          </Typography>
        </Typography>

        <Typography variant="h4">
          {value == 0 ? 0 : fCurrency(value)} PLN{' '}
          <Typography variant="subtitle2" component="span">
            gross
          </Typography>
        </Typography>
      </Stack>
    </Stack>
  );
};
