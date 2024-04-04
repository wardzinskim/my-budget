import { Breakpoint, useTheme } from '@mui/material/styles';
import useMediaQuery from '@mui/material/useMediaQuery';

// ----------------------------------------------------------------------

export enum Query {
  UP = 'up',
  DOWN = 'down',
  BETWEEN = 'between',
  NONE = 'none',
}

export function useResponsive(
  query: Query,
  start: Breakpoint,
  end?: Breakpoint
) {
  const theme = useTheme();

  if (query === Query.UP) {
    return useMediaQuery(theme.breakpoints.up(start));
  }

  if (query === Query.DOWN) {
    return useMediaQuery(theme.breakpoints.down(start));
  }

  if (query === Query.BETWEEN) {
    return useMediaQuery(theme.breakpoints.between(start, end!));
  }

  return useMediaQuery(theme.breakpoints.only(start));
}

// ----------------------------------------------------------------------

export function useWidth() {
  const theme = useTheme();

  const keys = [...theme.breakpoints.keys].reverse();

  return (
    keys.reduce((output: Breakpoint, key) => {
      const matches = useMediaQuery(theme.breakpoints.up(key));

      return !output && matches ? key : output;
    }, 'xs') || 'xs'
  );
}
