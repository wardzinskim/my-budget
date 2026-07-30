import IconButton from '@mui/material/IconButton';
import Tooltip from '@mui/material/Tooltip';

import { useColorMode } from '../../theme/color-mode-context';
import { Iconify } from '../iconify';

// ----------------------------------------------------------------------

export function ColorModeToggle() {
  const { mode, toggleColorMode } = useColorMode();

  const isDark = mode === 'dark';

  return (
    <Tooltip title={isDark ? 'Switch to light mode' : 'Switch to dark mode'}>
      <IconButton onClick={toggleColorMode} color="default">
        <Iconify icon={isDark ? 'solar:sun-bold' : 'solar:moon-bold'} />
      </IconButton>
    </Tooltip>
  );
}
