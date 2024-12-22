import React, { useState } from 'react';

import Box from '@mui/material/Box';

import { Account, Nav, NavItem } from './nav';
import { Main } from './main';
import { Header } from './header';

// ----------------------------------------------------------------------

interface LayoutProps extends React.PropsWithChildren {
  navigationItems: Array<NavItem>;
  navItemChildren?: React.ReactNode;
  account?: Account;
  headerRightContent?: React.ReactNode;
  headerCenterContent?: React.ReactNode;
  logoSrc?: string;
}

export const Layout: React.FC<LayoutProps> = (props) => {
  const [openNav, setOpenNav] = useState<boolean>(false);

  return (
    <>
      <Header
        onOpenNav={() => setOpenNav(true)}
        rightContent={props.headerRightContent}
        centerContent={props.headerCenterContent}
      />

      <Box
        sx={{
          minHeight: 1,
          display: 'flex',
          flexDirection: { xs: 'column', lg: 'row' },
        }}
      >
        <Nav
          account={props.account}
          openNav={openNav}
          onCloseNav={() => setOpenNav(false)}
          items={props.navigationItems}
          logoSrc={props.logoSrc}
        >
          {props.navItemChildren}
        </Nav>

        <Main>{props.children}</Main>
      </Box>
    </>
  );
};
