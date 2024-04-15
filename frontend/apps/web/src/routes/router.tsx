import { Layout } from '@repo/minimal-ui';
import { lazy, Suspense } from 'react';
import { createBrowserRouter, Outlet, RouterProvider } from 'react-router-dom';
import { navigation } from './navigation-config';
import { loader as budgetPageLoader } from '../pages/budgets/budgets.loader';
import { action as budgetNewPageAction } from '../pages/budgets/new/budget-new.action';
import { loader as budgetDetailsPageLoader } from '../pages/budgets/details/budget-details.loader';

export const DashboardPage = lazy(() => import('../pages/dashboard'));
export const BudgetsPage = lazy(() => import('../pages/budgets/budgets'));
export const BudgetNewPage = lazy(
  () => import('../pages/budgets/new/budget-new')
);
export const BudgetDetailsPage = lazy(
  () => import('../pages/budgets/details/budget-details')
);

// ----------------------------------------------------------------------

export const Paths = {
  budgetDetails: '/budgets/:id',
} as const;

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
        {
          path: '/budgets/new',
          element: <BudgetNewPage />,
          action: budgetNewPageAction,
        },
        {
          path: Paths.budgetDetails,
          element: <BudgetDetailsPage />,
          loader: budgetDetailsPageLoader,
        },
      ],
    },
  ]);

  return <RouterProvider router={router} />;
}
