import { Box, Stack, Typography, alpha } from '@mui/material';
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
  return (
    <Stack
      sx={{
        background: (theme) =>
          `linear-gradient(135deg, ${alpha(theme.palette.success.light, 0.2)}, ${alpha(
            theme.palette.success.main,
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
          icon={'tdesign:arrow-left-down'}
          sx={{
            bgcolor: (theme) => alpha(theme.palette.success.main, 0.16),
            color: 'success.dark',
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
