import Box from '@mui/material/Box';

import { NAV, HEADER } from './config-layout';
import React from 'react';
import { SxProps, Theme } from '@mui/material';
import { Query, useResponsive } from '../hooks/use-responsive';

// ----------------------------------------------------------------------

const SPACING = 8;

interface MainProps extends React.PropsWithChildren {
  sx?: SxProps<Theme>;
}

export function Main({ children, sx, ...other }: MainProps) {
  const lgUp = useResponsive(Query.UP, 'lg');

  return (
    <Box
      component="main"
      sx={{
        flexGrow: 1,
        minHeight: 1,
        display: 'flex',
        flexDirection: 'column',
        py: `${HEADER.H_MOBILE + SPACING}px`,
        ...(lgUp && {
          px: 2,
          py: `${HEADER.H_DESKTOP + SPACING}px`,
          width: `calc(100% - ${NAV.WIDTH}px)`,
        }),
        ...sx,
      }}
      {...other}
    >
      {children}
    </Box>
  );
}
