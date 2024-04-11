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
  const memoizedValue = useMemo(() => {
    const paletteOptions = palette();
    const customShadowsOptions = customShadows();

    return {
      palette: paletteOptions,
      typography: typography,
      shadows: shadows(),
      customShadows: customShadowsOptions,
      shape: { borderRadius: 8 },
    };
  }, []);

  const theme = createTheme(memoizedValue as ThemeOptions);
  // @ts-expect-error ignore
  theme.components = overrides(theme, memoizedValue.customShadows);

  return (
    <MUIThemeProvider theme={theme}>
      <CssBaseline />
      {children}
    </MUIThemeProvider>
  );
};
