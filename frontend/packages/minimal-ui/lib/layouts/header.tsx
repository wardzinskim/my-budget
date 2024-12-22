import Box from '@mui/material/Box';
import Stack from '@mui/material/Stack';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import { useTheme } from '@mui/material/styles';
import IconButton from '@mui/material/IconButton';

import { NAV, HEADER } from './config-layout';
import { Query, useResponsive } from '../hooks/use-responsive';
import { bgBlur } from '../theme/css';
import { Iconify } from '../components';

// ----------------------------------------------------------------------

interface HeaderProps {
  onOpenNav: () => void;
  rightContent?: React.ReactNode;
  centerContent?: React.ReactNode;
}

export function Header({
  onOpenNav,
  rightContent,
  centerContent,
}: HeaderProps) {
  const theme = useTheme();

  const lgUp = useResponsive(Query.UP, 'lg');

  const renderContent = (
    <>
      {!lgUp && (
        <IconButton onClick={onOpenNav} sx={{ mr: 1 }}>
          <Iconify icon="eva:menu-2-fill" />
        </IconButton>
      )}

      {/* <Searchbar /> */}

      <Box sx={{ flexGrow: 1 }}>{centerContent ?? <></>}</Box>

      <Stack direction="row" alignItems="center" spacing={1}>
        {/* <LanguagePopover /> */}
        {/* <NotificationsPopover /> */}
        {/* <AccountPopover /> */}
        {rightContent ?? <></>}
      </Stack>
    </>
  );

  return (
    <AppBar
      sx={{
        boxShadow: 'none',
        height: HEADER.H_MOBILE,
        zIndex: theme.zIndex.appBar + 1,
        ...bgBlur({
          color: theme.palette.background.default,
        }),
        transition: theme.transitions.create(['height'], {
          duration: theme.transitions.duration.shorter,
        }),
        ...(lgUp && {
          width: `calc(100% - ${NAV.WIDTH + 1}px)`,
          height: HEADER.H_DESKTOP,
        }),
      }}
    >
      <Toolbar
        sx={{
          height: 1,
          px: { lg: 5 },
        }}
      >
        {renderContent}
      </Toolbar>
    </AppBar>
  );
}
