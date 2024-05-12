import { Box, Stack, Typography } from '@mui/material';
import { TransferDTOType } from '@repo/api-client';
import { Iconify, fCurrency } from '@repo/minimal-ui';

interface TotalsViewerProps {
  type: TransferDTOType;
  value: number;
}

export const TotalsViewer: React.FC<TotalsViewerProps> = ({ type, value }) => {
  const iconColors =
    type == TransferDTOType.Income
      ? { backgroundColor: 'rgb(0, 120, 103)', color: 'rgb(200, 250, 214)' }
      : { backgroundColor: 'rgb(183, 110, 0)', color: 'rgb(255, 245, 204)' };

  return (
    <Stack
      sx={{
        background:
          type == TransferDTOType.Income
            ? 'linear-gradient(135deg, rgba(91, 228, 155, 0.2), rgba(0, 167, 111, 0.2)) rgb(255, 255, 255)'
            : 'linear-gradient(135deg, rgba(255, 214, 102, 0.2), rgba(255, 171, 0, 0.2)) rgb(255, 255, 255)',
        color:
          type == TransferDTOType.Income
            ? 'rgb(33, 43, 54)'
            : 'rgb(122, 65, 0)',
        position: 'relative',
      }}
      borderRadius={2}
    >
      <Stack padding={3}>
        <Box
          component={Iconify}
          icon={
            type == TransferDTOType.Income
              ? 'tdesign:arrow-left-down'
              : 'tdesign:arrow-right-up'
          }
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

        <Typography variant="subtitle2">{type}</Typography>
        <Typography variant="h3">
          {value == 0 ? 0 : fCurrency(value)} PLN
        </Typography>
      </Stack>
    </Stack>
  );
};
