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
  const upMediaQuery = useMediaQuery(theme.breakpoints.up(start));
  const downMediaQuery = useMediaQuery(theme.breakpoints.down(start));
  const betweenMediaQuery = useMediaQuery(
    theme.breakpoints.between(start, end!)
  );
  const defaultMediaQuery = useMediaQuery(theme.breakpoints.only(start));

  if (query === Query.UP) {
    return upMediaQuery;
  }

  if (query === Query.DOWN) {
    return downMediaQuery;
  }

  if (query === Query.BETWEEN) {
    return betweenMediaQuery;
  }

  return defaultMediaQuery;
}

// ----------------------------------------------------------------------

// export function useWidth() {
//   const theme = useTheme();

//   const keys = [...theme.breakpoints.keys].reverse();

//   return (
//     keys.reduce((output: Breakpoint, key) => {
//       const matches = useMediaQuery(theme.breakpoints.up(key));

//       return !output && matches ? key : output;
//     }, 'xs') || 'xs'
//   );
// }
