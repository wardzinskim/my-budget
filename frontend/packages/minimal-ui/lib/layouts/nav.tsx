import { useEffect } from 'react';

import Box from '@mui/material/Box';
import Stack from '@mui/material/Stack';
import Drawer from '@mui/material/Drawer';
import Avatar from '@mui/material/Avatar';
import { alpha } from '@mui/material/styles';
import Typography from '@mui/material/Typography';
import ListItemButton from '@mui/material/ListItemButton';
import { Query, useResponsive } from '../hooks/use-responsive';
import { usePathname } from '../routes/hooks';
import { RouterLink } from '../routes/components';
import { NAV } from './config-layout';
import { account } from '../_mock/account';
import { Logo, Scrollbar, SvgColor } from '../components';
import { isElement } from 'react-dom/test-utils';

// ----------------------------------------------------------------------

const icon = (name: string) => (
  <SvgColor
    src={`/assets/icons/navbar/${name}.svg`}
    sx={{ width: 1, height: 1 }}
  />
);

export interface NavItem {
  title: string;
  path: string;
  icon?: string | JSX.Element;
}

interface NavProps {
  items: Array<NavItem>;
  openNav: boolean;
  onCloseNav: () => void;
}

export const Nav: React.FC<NavProps> = (props) => {
  const pathname = usePathname();

  const upLg = useResponsive(Query.UP, 'lg');

  useEffect(() => {
    if (props.openNav) {
      props.onCloseNav();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [pathname]);

  const renderAccount = (
    <Box
      sx={{
        my: 3,
        mx: 2.5,
        py: 2,
        px: 2.5,
        display: 'flex',
        borderRadius: 1.5,
        alignItems: 'center',
        bgcolor: (theme) => alpha(theme.palette.grey[500], 0.12),
      }}
    >
      <Avatar src={account.photoURL} alt="photoURL" />

      <Box sx={{ ml: 2 }}>
        <Typography variant="subtitle2">{account.displayName}</Typography>

        <Typography variant="body2" sx={{ color: 'text.secondary' }}>
          {account.role}
        </Typography>
      </Box>
    </Box>
  );

  const renderMenu = (
    <Stack component="nav" spacing={0.5} sx={{ px: 2 }}>
      {props.items.map((item) => (
        <NavItem key={item.title} item={item} />
      ))}
    </Stack>
  );

  const renderContent = (
    <Scrollbar
      sx={{
        height: 1,
        '& .simplebar-content': {
          height: 1,
          display: 'flex',
          flexDirection: 'column',
        },
      }}
    >
      <Logo sx={{ mt: 3, ml: 4 }} />

      {renderAccount}

      {renderMenu}
    </Scrollbar>
  );

  return (
    <Box
      sx={{
        flexShrink: { lg: 0 },
        width: { lg: NAV.WIDTH },
      }}
    >
      {upLg ? (
        <Box
          sx={{
            height: 1,
            position: 'fixed',
            width: NAV.WIDTH,
            borderRight: (theme) => `dashed 1px ${theme.palette.divider}`,
          }}
        >
          {renderContent}
        </Box>
      ) : (
        <Drawer
          open={props.openNav}
          onClose={props.onCloseNav}
          PaperProps={{
            sx: {
              width: NAV.WIDTH,
            },
          }}
        >
          {renderContent}
        </Drawer>
      )}
    </Box>
  );
};

// ----------------------------------------------------------------------

interface NavItemProps {
  item: NavItem;
}

export function NavItem({ item }: NavItemProps) {
  const pathname = usePathname();

  const active = item.path === pathname;

  return (
    <ListItemButton
      component={RouterLink}
      href={item.path}
      sx={{
        minHeight: 44,
        borderRadius: 0.75,
        typography: 'body2',
        color: 'text.secondary',
        textTransform: 'capitalize',
        fontWeight: 'fontWeightRegular',
        ...(active && {
          color: 'primary.main',
          fontWeight: 'fontWeightMedium',
          bgcolor: (theme) => alpha(theme.palette.primary.main, 0.08),
          '&:hover': {
            bgcolor: (theme) => alpha(theme.palette.primary.main, 0.16),
          },
        }),
      }}
    >
      {typeof item.icon === 'string' && (
        <Box component="span" sx={{ width: 24, height: 24, mr: 2 }}>
          {icon(item.icon)}
        </Box>
      )}

      {isElement(item.icon) && (
        <Box component="span" sx={{ width: 24, height: 24, mr: 1 }}>
          {item.icon}
        </Box>
      )}

      <Box component="span">{item.title} </Box>
    </ListItemButton>
  );
}
