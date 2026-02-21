import { memo } from 'react';
import { alpha, styled } from '@mui/material/styles';
import ApexChart from 'react-apexcharts';
import { CustomShadows, customShadows } from '../../theme/custom-shadows';

const customShadowsValue: CustomShadows = customShadows();

export const Chart = memo(
  styled(ApexChart)(({ theme }) => ({
    '& .apexcharts-canvas': {
      // Tooltip
      '& .apexcharts-tooltip': {
        backgroundColor: theme.palette.background.paper,
        color: theme.palette.text.primary,
        boxShadow: customShadowsValue.dropdown,
        borderRadius: theme.shape.borderRadius * 1.5,
        border: `1px solid ${alpha(theme.palette.grey[500], 0.16)}`,
        '&.apexcharts-theme-light': {
          borderColor: alpha(theme.palette.grey[500], 0.16),
          backgroundColor: theme.palette.background.paper,
        },
      },
      '& .apexcharts-xaxistooltip': {
        backgroundColor: theme.palette.background.paper,
        borderColor: alpha(theme.palette.grey[500], 0.16),
        color: theme.palette.text.primary,
        boxShadow: customShadowsValue.dropdown,
        borderRadius: theme.shape.borderRadius * 1.5,
        '&:before': {
          borderBottomColor: alpha(theme.palette.grey[500], 0.24),
        },
        '&:after': {
          borderBottomColor: theme.palette.background.paper,
        },
      },
      '& .apexcharts-tooltip-title': {
        textAlign: 'center',
        fontWeight: theme.typography.fontWeightBold,
        backgroundColor: alpha(theme.palette.grey[500], 0.08),
        color:
          theme.palette.text[
            theme.palette.mode === 'light' ? 'secondary' : 'primary'
          ],
      },

      // LEGEND
      '& .apexcharts-legend': {
        padding: 0,
      },
      '& .apexcharts-legend-series': {
        display: 'inline-flex !important',
        alignItems: 'center',
      },
      '& .apexcharts-legend-marker': {
        marginRight: 8,
      },
      '& .apexcharts-legend-text': {
        lineHeight: '18px',
        textTransform: 'capitalize',
      },
    },
  }))
);
