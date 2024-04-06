import { Layout } from '@repo/minimal-ui';
import { lazy, Suspense } from 'react';
import { Outlet, useRoutes } from 'react-router-dom';
import { navigation } from './navigation-config';

export const DashboardPage = lazy(() => import('../pages/dashboard'));
export const BudgetsPage = lazy(() => import('../pages/budgets'));

// ----------------------------------------------------------------------

export default function Router() {
  const routes = useRoutes([
    {
      element: (
        <Layout navigationItems={navigation}>
          <Suspense>
            <Outlet />
          </Suspense>
        </Layout>
      ),
      children: [
        { element: <DashboardPage />, index: true },
        { path: '/budgets', element: <BudgetsPage /> },
      ],
    },
  ]);

  return routes;
}
