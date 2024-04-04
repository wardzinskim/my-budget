import { useState } from 'react';

import Box from '@mui/material/Box';

import { Nav } from './nav';
import { Main } from './main';
import { Header } from './header';

// ----------------------------------------------------------------------

interface DashboardLayoutProps extends React.PropsWithChildren {}

export const DashboardLayout: React.FC<DashboardLayoutProps> = ({
  children,
}) => {
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
        <Nav openNav={openNav} onCloseNav={() => setOpenNav(false)} />

        <Main>{children}</Main>
      </Box>
    </>
  );
};
