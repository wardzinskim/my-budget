import { PaletteOptions, alpha } from '@mui/material/styles';
import {
  ColorPartial,
  CommonColors,
  SimplePaletteColorOptions,
  TypeAction,
} from '@mui/material/styles/createPalette';

// ----------------------------------------------------------------------

// SETUP COLORS

export const grey: ColorPartial = {
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
  // lighter: '#D0ECFE',
  light: '#73BAFB',
  main: '#1877F2',
  dark: '#0C44AE',
  // darker: '#042174',
  contrastText: '#FFFFFF',
};

export const secondary: SimplePaletteColorOptions = {
  // lighter: '#EFD6FF',
  light: '#C684FF',
  main: '#8E33FF',
  dark: '#5119B7',
  // darker: '#27097A',
  contrastText: '#FFFFFF',
};

export const info: SimplePaletteColorOptions = {
  // lighter: '#CAFDF5',
  light: '#61F3F3',
  main: '#00B8D9',
  dark: '#006C9C',
  // darker: '#003768',
  contrastText: '#FFFFFF',
};

export const success: SimplePaletteColorOptions = {
  // lighter: '#C8FAD6',
  light: '#5BE49B',
  main: '#00A76F',
  dark: '#007867',
  // darker: '#004B50',
  contrastText: '#FFFFFF',
};

export const warning: SimplePaletteColorOptions = {
  // lighter: '#FFF5CC',
  light: '#FFD666',
  main: '#FFAB00',
  dark: '#B76E00',
  // darker: '#7A4100',
  contrastText: grey[800],
};

export const error: SimplePaletteColorOptions = {
  // lighter: '#FFE9D5',
  light: '#FFAC82',
  main: '#FF5630',
  dark: '#B71D18',
  // darker: '#7A0916',
  contrastText: '#FFFFFF',
};

export const common: Partial<CommonColors> = {
  black: '#000000',
  white: '#FFFFFF',
};

export const action: Partial<TypeAction> = {
  hover: alpha(grey[500]!, 0.08),
  selected: alpha(grey[500]!, 0.16),
  disabled: alpha(grey[500]!, 0.8),
  disabledBackground: alpha(grey[500]!, 0.24),
  focus: alpha(grey[500]!, 0.24),
  hoverOpacity: 0.08,
  disabledOpacity: 0.48,
};

const base = {
  primary,
  secondary,
  info,
  success,
  warning,
  error,
  grey,
  common,
  divider: alpha(grey[500]!, 0.2),
  action,
};

// ----------------------------------------------------------------------

export function palette(): PaletteOptions {
  return {
    ...base,
    mode: 'light',
    text: {
      primary: grey[800],
      secondary: grey[600],
      disabled: grey[500],
    },
    background: {
      paper: '#FFFFFF',
      default: grey[100],
      // neutral: grey[200],
    },
    action: {
      ...base.action,
      active: grey[600],
    },
  };
}
