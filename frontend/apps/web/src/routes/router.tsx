import { Layout } from '@repo/minimal-ui';
import { lazy, Suspense } from 'react';
import { createBrowserRouter, Outlet, RouterProvider } from 'react-router-dom';
import { navigation } from './navigation-config';
import { loader as budgetPageLoader } from '../pages/budgets/budgets.loader';
import { action as budgetNewPageAction } from '../pages/budgets/new/budget-new.action';
import { loader as budgetDetailsPageLoader } from '../pages/budgets/details/budget-details.loader';
import { action as budgetDetailsPageAction } from '../pages/budgets/details/categories/budget-categories.action';
import { BudgetContextPicker } from '../components/budgets/budget-context-picker';
import { loader as transfersPageLoader } from '../pages/transfers/transfers.loader';
import { Stack } from '@mui/material';
import { useUserContext } from '../hooks/user-context';
import { TransferErrorPage } from '../pages/transfers/transfers-error';
import { TransferDTOType } from '@repo/api-client';

export const DashboardPage = lazy(() => import('../pages/dashboard'));
export const BudgetsPage = lazy(() => import('../pages/budgets/budgets'));
export const BudgetNewPage = lazy(
  () => import('../pages/budgets/new/budget-new')
);
export const BudgetDetailsPage = lazy(
  () => import('../pages/budgets/details/budget-details')
);
export const BudgetDetailsCategoriesPage = lazy(
  () => import('../pages/budgets/details/categories/budget-categories')
);
export const TransfersPage = lazy(() => import('../pages/transfers/transfers'));
export const TransfersNewPage = lazy(
  () => import('../pages/transfers/new/transfer-new')
);
// ----------------------------------------------------------------------

export const Paths = {
  budgetDetails: '/budgets/:id',
} as const;

export default function Router() {
  const [userContext] = useUserContext();

  const router = createBrowserRouter([
    {
      element: (
        <Layout
          navigationItems={navigation}
          navItemChildren={
            <Stack spacing={0.5} sx={{ px: 2, paddingBottom: 2 }}>
              <BudgetContextPicker />
            </Stack>
          }
        >
          <Suspense>
            <Outlet />
          </Suspense>
        </Layout>
      ),
      children: [
        { element: <DashboardPage />, index: true },
        {
          id: 'budget-list',
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
          id: 'budget-details',
          path: Paths.budgetDetails,
          element: <BudgetDetailsPage />,
          loader: budgetDetailsPageLoader,
          children: [
            {
              index: true,
              element: <BudgetDetailsCategoriesPage />,
              action: budgetDetailsPageAction,
            },
          ],
        },
        {
          path: '/transfers',
          element: <TransfersPage />,
          loader: transfersPageLoader(userContext),
          errorElement: <TransferErrorPage />,
        },
        {
          path: '/transfers/new/income',
          element: <TransfersNewPage type={TransferDTOType.Income} />,
          errorElement: <TransferErrorPage />,
        },
        {
          path: '/transfers/new/expense',
          element: <TransfersNewPage type={TransferDTOType.Expense} />,
          errorElement: <TransferErrorPage />,
        },
      ],
    },
  ]);

  return <RouterProvider router={router} />;
}
