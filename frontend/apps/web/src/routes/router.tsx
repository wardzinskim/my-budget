import { Layout } from '@repo/minimal-ui';
import { lazy, Suspense } from 'react';
import { createBrowserRouter, Outlet, RouterProvider } from 'react-router-dom';
import { navigation } from './navigation-config';
import { loader as budgetPageLoader } from '../pages/budgets.loader';

export const DashboardPage = lazy(() => import('../pages/dashboard'));
export const BudgetsPage = lazy(() => import('../pages/budgets'));

// ----------------------------------------------------------------------

export default function Router() {
  const router = createBrowserRouter([
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
        {
          path: '/budgets',
          element: <BudgetsPage />,
          loader: budgetPageLoader,
        },
      ],
    },
  ]);

  return <RouterProvider router={router} />;
}
