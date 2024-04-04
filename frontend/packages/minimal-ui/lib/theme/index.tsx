import { useMemo } from 'react';

import CssBaseline from '@mui/material/CssBaseline';
import {
  createTheme,
  ThemeProvider as MUIThemeProvider,
  ThemeOptions,
} from '@mui/material/styles';

import { palette } from './palette';
import { shadows } from './shadows';
import { overrides } from './overrides';
import { typography } from './typography';
import { customShadows } from './custom-shadows';

// ----------------------------------------------------------------------

interface ThemeProviderProps extends React.PropsWithChildren {}

export const ThemeProvider: React.FC<ThemeProviderProps> = ({ children }) => {
  const memoizedValue: ThemeOptions = useMemo(() => {
    const paletteOptions = palette();

    return {
      palette: paletteOptions,
      typography: typography,
      shadows: shadows(),
      customShadows: customShadows(),
      shape: { borderRadius: 8 },
      components: overrides(paletteOptions),
    };
  }, []);

  const theme = createTheme(memoizedValue);
  return (
    <MUIThemeProvider theme={theme}>
      <CssBaseline />
      {children}
    </MUIThemeProvider>
  );
};
