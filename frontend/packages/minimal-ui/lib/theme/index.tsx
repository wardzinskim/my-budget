import { useEffect, useMemo, useState } from 'react';

import CssBaseline from '@mui/material/CssBaseline';
import {
  createTheme,
  ThemeProvider as MUIThemeProvider,
} from '@mui/material/styles';
import { palette } from './palette';
import { shadows } from './shadows';
import { overrides } from './overrides';
import { typography } from './typography';
import { customShadows } from './custom-shadows';
import {
  COLOR_MODE_STORAGE_KEY,
  ColorMode,
  ColorModeContext,
  getInitialColorMode,
} from './color-mode-context';

// ----------------------------------------------------------------------

interface ThemeProviderProps extends React.PropsWithChildren {}

export const ThemeProvider: React.FC<ThemeProviderProps> = ({ children }) => {
  const [mode, setModeState] = useState<ColorMode>(getInitialColorMode);

  useEffect(() => {
    window.localStorage.setItem(COLOR_MODE_STORAGE_KEY, mode);
  }, [mode]);

  const colorModeContextValue = useMemo(
    () => ({
      mode,
      setMode: (newMode: ColorMode) => setModeState(newMode),
      toggleColorMode: () =>
        setModeState((prev) => (prev === 'light' ? 'dark' : 'light')),
    }),
    [mode]
  );

  const theme = useMemo(() => {
    const paletteOptions = palette(mode);
    const customShadowsOptions = customShadows(mode);

    const nextTheme = createTheme({
      palette: paletteOptions,
      typography: typography,
      shadows: shadows(mode),
      customShadows: customShadowsOptions,
      shape: { borderRadius: 12 },
    });

    // @ts-expect-error component style overrides reference the augmented Theme type
    nextTheme.components = overrides(nextTheme, customShadowsOptions);

    return nextTheme;
  }, [mode]);

  return (
    <ColorModeContext.Provider value={colorModeContextValue}>
      <MUIThemeProvider theme={theme}>
        <CssBaseline />
        {children}
      </MUIThemeProvider>
    </ColorModeContext.Provider>
  );
};
