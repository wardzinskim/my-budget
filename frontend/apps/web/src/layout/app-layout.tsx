import { Stack } from '@mui/material';
import { Layout } from '@repo/minimal-ui';
import { BudgetContextPicker } from '../components/budgets/budget-context-picker';
import { Suspense } from 'react';
import { Outlet } from 'react-router-dom';
import { useAuth } from 'react-oidc-context';
import { navigation } from '../routes/navigation-config';
import { LogoutButton } from './components/logout-button';
import { Header } from './components/header';

export const AppLayout = () => {
  const auth = useAuth();

  return (
    <Layout
      account={
        auth && {
          displayName: auth.user?.profile.preferred_username,
          photoUrl:
            auth.user?.profile.picture ??
            '/assets/images/avatars/avatar_25.jpg',
        }
      }
      headerRightContent={auth?.user && <LogoutButton />}
      headerCenterContent={<Header />}
      navigationItems={navigation}
      navItemChildren={
        <Stack spacing={0.5} sx={{ px: 2, paddingBottom: 2 }}>
          <BudgetContextPicker />
        </Stack>
      }
      logoSrc="/assets/logo.svg"
    >
      <Suspense>
        <Outlet />
      </Suspense>
    </Layout>
  );
};
