import React, { useState } from 'react';

import Box from '@mui/material/Box';

import { Nav, NavItem } from './nav';
import { Main } from './main';
import { Header } from './header';

// ----------------------------------------------------------------------

interface LayoutProps extends React.PropsWithChildren {
  navigationItems: Array<NavItem>;
  navItemChildren?: React.ReactNode;
}

export const Layout: React.FC<LayoutProps> = (props) => {
  const [openNav, setOpenNav] = useState<boolean>(false);

  return (
    <>
      <Header onOpenNav={() => setOpenNav(true)} />

      <Box
        sx={{
          minHeight: 1,
          display: 'flex',
          flexDirection: { xs: 'column', lg: 'row' },
        }}
      >
        <Nav
          openNav={openNav}
          onCloseNav={() => setOpenNav(false)}
          items={props.navigationItems}
        >
          {props.navItemChildren}
        </Nav>

        <Main>{props.children}</Main>
      </Box>
    </>
  );
};
