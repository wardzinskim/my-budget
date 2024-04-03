import { useMemo } from 'react';
import { palette } from './palette';
import {
  ThemeOptions,
  createTheme,
  ThemeProvider as MUIThemeProvider,
  CssBaseline,
} from '@mui/material';
import { shadows } from './shadows';
import { typography } from './typography';

interface IProps extends React.PropsWithChildren {}

export const ThemeProvider: React.FC<IProps> = (props: IProps) => {
  const memoizedValue: ThemeOptions = useMemo(
    () => ({
      palette: palette(),
      typography: typography,
      shadows: shadows(),
      //   customShadows: customShadows(),
      shape: { borderRadius: 8 },
    }),
    []
  );

  const theme = createTheme(memoizedValue);

  // theme.components = overrides(theme);

  return (
    <MUIThemeProvider theme={theme}>
      <CssBaseline />
      {props.children}
    </MUIThemeProvider>
  );
};
