import { forwardRef } from 'react';
import { Icon, IconifyIcon } from '@iconify/react';

import Box from '@mui/material/Box';
import { SxProps, Theme } from '@mui/material';

// ----------------------------------------------------------------------
interface IconifyProps {
  icon: string | IconifyIcon;
  sx?: SxProps<Theme>;
  width?: number;
}

const Iconify = forwardRef(
  ({ icon, width = 20, sx, ...other }: IconifyProps, ref) => (
    <Box
      ref={ref}
      component={Icon}
      className="component-iconify"
      icon={icon}
      sx={{ width, height: width, ...sx }}
      {...other}
    />
  )
);

export default Iconify;
