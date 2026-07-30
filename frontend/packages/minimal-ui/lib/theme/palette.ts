import { PaletteOptions, alpha } from '@mui/material/styles';
import type {
  Color,
  CommonColors,
  SimplePaletteColorOptions,
  TypeAction,
} from '@mui/material/styles';
import type { ColorMode } from './color-mode-context';

// ----------------------------------------------------------------------

// SETUP COLORS

export const grey: Partial<Color> = {
  // 0: '#FFFFFF',
  100: '#F9FAFB',
  200: '#F4F6F8',
  300: '#DFE3E8',
  400: '#C4CDD5',
  500: '#919EAB',
  600: '#637381',
  700: '#454F5B',
  800: '#212B36',
  900: '#161C24',
};

export const primary: SimplePaletteColorOptions = {
  lighter: '#E0E7FF',
  light: '#818CF8',
  main: '#6366F1',
  dark: '#4338CA',
  darker: '#312E81',
  contrastText: '#FFFFFF',
};

export const secondary: SimplePaletteColorOptions = {
  lighter: '#EFD6FF',
  light: '#C684FF',
  main: '#8E33FF',
  dark: '#5119B7',
  darker: '#27097A',
  contrastText: '#FFFFFF',
};

export const info: SimplePaletteColorOptions = {
  lighter: '#CAFDF5',
  light: '#61F3F3',
  main: '#00B8D9',
  dark: '#006C9C',
  darker: '#003768',
  contrastText: '#FFFFFF',
};

export const success: SimplePaletteColorOptions = {
  lighter: '#C8FAD6',
  light: '#5BE49B',
  main: '#00A76F',
  dark: '#007867',
  darker: '#004B50',
  contrastText: '#FFFFFF',
};

export const warning: SimplePaletteColorOptions = {
  lighter: '#FFF5CC',
  light: '#FFD666',
  main: '#FFAB00',
  dark: '#B76E00',
  darker: '#7A4100',
  contrastText: grey[800],
};

export const error: SimplePaletteColorOptions = {
  lighter: '#FFE9D5',
  light: '#FFAC82',
  main: '#FF5630',
  dark: '#B71D18',
  darker: '#7A0916',
  contrastText: '#FFFFFF',
};

export const common: Partial<CommonColors> = {
  black: '#000000',
  white: '#FFFFFF',
};

function getAction(mode: ColorMode): Partial<TypeAction> {
  return {
    hover: alpha(grey[500]!, 0.08),
    selected: alpha(grey[500]!, mode === 'dark' ? 0.24 : 0.16),
    disabled: alpha(grey[500]!, 0.8),
    disabledBackground: alpha(grey[500]!, 0.24),
    focus: alpha(grey[500]!, 0.24),
    hoverOpacity: 0.08,
    disabledOpacity: 0.48,
    active: mode === 'dark' ? grey[400] : grey[600],
  };
}

const base = {
  primary,
  secondary,
  info,
  success,
  warning,
  error,
  grey,
  common,
};

// ----------------------------------------------------------------------

export function palette(mode: ColorMode = 'light'): PaletteOptions {
  if (mode === 'dark') {
    return {
      ...base,
      mode: 'dark',
      divider: alpha(grey[500]!, 0.24),
      action: getAction('dark'),
      text: {
        primary: '#FFFFFF',
        secondary: grey[400],
        disabled: grey[600],
      },
      background: {
        paper: '#1C232F',
        default: '#141A21',
      },
    };
  }

  return {
    ...base,
    mode: 'light',
    divider: alpha(grey[500]!, 0.2),
    action: getAction('light'),
    text: {
      primary: grey[800],
      secondary: grey[600],
      disabled: grey[500],
    },
    background: {
      paper: '#FFFFFF',
      default: grey[100],
    },
  };
}
