import { Components, PaletteOptions, Theme, alpha } from '@mui/material/styles';
import { outlinedInputClasses } from '@mui/material/OutlinedInput';
import { typography } from './typography';
import { CustomShadows } from './custom-shadows';

// ----------------------------------------------------------------------

export function overrides(
  paletteOptions: PaletteOptions,
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
          backgroundColor: alpha(paletteOptions.grey![900]!, 0.8),
        },
        invisible: {
          background: 'transparent',
        },
      },
    },
    MuiButton: {
      styleOverrides: {
        containedInherit: {
          color: paletteOptions.common!.white,
          backgroundColor: paletteOptions.grey![800],
          '&:hover': {
            color: paletteOptions.common!.white,
            backgroundColor: paletteOptions.grey![800],
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
          borderRadius: 8 * 2,
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
          // padding: theme.spacing(3, 3, 0),
        },
      },
    },
    MuiOutlinedInput: {
      styleOverrides: {
        root: {
          [`& .${outlinedInputClasses.notchedOutline}`]: {
            borderColor: alpha(paletteOptions.grey![500]!, 0.24),
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
          color: paletteOptions.text!.secondary,
          backgroundColor: paletteOptions.grey![200],
        },
      },
    },
    MuiTooltip: {
      styleOverrides: {
        tooltip: {
          backgroundColor: paletteOptions.grey![800],
        },
        arrow: {
          color: paletteOptions.grey![800],
        },
      },
    },
    MuiTypography: {
      styleOverrides: {
        paragraph: {
          // marginBottom: theme.spacing(2),
        },
        gutterBottom: {
          // marginBottom: theme.spacing(1),
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
  };
}
