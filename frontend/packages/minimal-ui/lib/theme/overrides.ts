import { Components, Theme, alpha } from '@mui/material/styles';
import { outlinedInputClasses } from '@mui/material/OutlinedInput';
import { typography } from './typography';
import { CustomShadows } from './custom-shadows';
import type {} from '@mui/x-date-pickers/themeAugmentation';
import { Iconify } from '../components';
import { renderTimeViewClock } from '@mui/x-date-pickers';
// ----------------------------------------------------------------------

export function overrides(
  theme: Theme,
  customShadows: CustomShadows
): Components<Omit<Theme, 'components'>> {
  return {
    MuiCssBaseline: {
      styleOverrides: {
        '*': {
          boxSizing: 'border-box',
        },
        html: {
          margin: 0,
          padding: 0,
          width: '100%',
          height: '100%',
          WebkitOverflowScrolling: 'touch',
        },
        body: {
          margin: 0,
          padding: 0,
          width: '100%',
          height: '100%',
        },
        '#root': {
          width: '100%',
          height: '100%',
        },
        input: {
          '&[type=number]': {
            MozAppearance: 'textfield',
            '&::-webkit-outer-spin-button': {
              margin: 0,
              WebkitAppearance: 'none',
            },
            '&::-webkit-inner-spin-button': {
              margin: 0,
              WebkitAppearance: 'none',
            },
          },
        },
        img: {
          maxWidth: '100%',
          display: 'inline-block',
          verticalAlign: 'bottom',
        },
      },
    },
    MuiBackdrop: {
      styleOverrides: {
        root: {
          backgroundColor: alpha(theme.palette.grey[900], 0.8),
        },
        invisible: {
          background: 'transparent',
        },
      },
    },
    MuiButton: {
      styleOverrides: {
        containedInherit: {
          color: theme.palette.common.white,
          backgroundColor: theme.palette.grey[800],
          '&:hover': {
            color: theme.palette.common.white,
            backgroundColor: theme.palette.grey[800],
          },
        },
        sizeLarge: {
          minHeight: 48,
        },
      },
    },
    MuiCard: {
      styleOverrides: {
        root: {
          boxShadow: customShadows.card,
          borderRadius: theme.shape.borderRadius * 2,
          position: 'relative',
          zIndex: 0, // Fix Safari overflow: hidden with border radius
        },
      },
    },
    MuiCardHeader: {
      defaultProps: {
        titleTypographyProps: { variant: 'h6' },
        subheaderTypographyProps: { variant: 'body2' },
      },
      styleOverrides: {
        root: {
          padding: theme.spacing(3, 3, 0),
        },
      },
    },
    MuiCardContent: {
      styleOverrides: {
        root: {
          padding: theme.spacing(3, 3, 3, 3),
        },
      },
    },
    MuiCardActions: {
      styleOverrides: {
        root: {
          padding: theme.spacing(3, 3, 3, 3),
          justifyContent: 'flex-end',
        },
      },
    },
    MuiOutlinedInput: {
      styleOverrides: {
        root: {
          [`& .${outlinedInputClasses.notchedOutline}`]: {
            borderColor: alpha(theme.palette.grey[500], 0.24),
          },
        },
      },
    },
    MuiPaper: {
      defaultProps: {
        elevation: 0,
      },
    },
    MuiTableCell: {
      styleOverrides: {
        head: {
          color: theme.palette.text.secondary,
          backgroundColor: theme.palette.grey[200],
        },
      },
    },
    MuiTooltip: {
      styleOverrides: {
        tooltip: {
          backgroundColor: theme.palette.grey[800],
        },
        arrow: {
          color: theme.palette.grey[800],
        },
      },
    },
    MuiTypography: {
      styleOverrides: {
        paragraph: {
          marginBottom: theme.spacing(2),
        },
        gutterBottom: {
          marginBottom: theme.spacing(1),
        },
      },
    },
    MuiMenuItem: {
      styleOverrides: {
        root: {
          ...typography.body2,
        },
      },
    },
    MuiDialog: {
      styleOverrides: {
        paper: {
          borderRadius: theme.shape.borderRadius * 2,
        },
      },
    },
    MuiDialogActions: {
      styleOverrides: {
        root: {
          padding: theme.spacing(3, 3, 3, 3),
        },
      },
    },
    MuiDateTimePicker: {
      defaultProps: {
        slots: {
          openPickerIcon: Iconify,
        },
        slotProps: {
          openPickerIcon: {
            icon: 'solar:calendar-mark-bold-duotone',
          },
        },
        viewRenderers: {
          hours: renderTimeViewClock,
          minutes: renderTimeViewClock,
          seconds: renderTimeViewClock,
        },
      },
    },
  };
}
