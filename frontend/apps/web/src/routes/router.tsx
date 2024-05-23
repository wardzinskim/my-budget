import { lazy } from 'react';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import { loader as budgetPageLoader } from '../pages/budgets/budgets.loader';
import { action as budgetNewPageAction } from '../pages/budgets/new/budget-new.action';
import { action as transferNewPageAction } from '../pages/transfers/new/transfer-new.action';
import { loader as budgetDetailsPageLoader } from '../pages/budgets/details/budget-details.loader';
import { action as budgetDetailsPageAction } from '../pages/budgets/details/categories/budget-categories.action';
import { action as transfersPageAction } from '../pages/transfers/transfers.action';
import { loader as transfersPageLoader } from '../pages/transfers/transfers.loader';
import { loader as transferEditPageLoader } from '../pages/transfers/edit/transfer-edit.loader';
import { action as transferEditPageAction } from '../pages/transfers/edit/transfer-edit.action';
import { loader as dashboardPageLoader } from '../pages/dashboard/dashboard.loader';
import { useUserContext } from '../hooks/user-context';
import { TransferErrorPage } from '../pages/transfers/transfers-error';
import { TransferDTOType } from '@repo/api-client';
import { useDashboardContext } from '../components/dashboard/hooks/dashboard-context';
import { AppLayout } from '../layout/app-layout';

export const DashboardPage = lazy(() => import('../pages/dashboard/dashboard'));
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
export const TransfersEditPage = lazy(
  () => import('../pages/transfers/edit/transfer-edit')
);
// ----------------------------------------------------------------------

export const Paths = {
  budgetDetails: '/budgets/:id',
  transferEdit: '/transfers/:id/edit',
} as const;

export default function Router() {
  const [userContext] = useUserContext();
  const [dashboardContext] = useDashboardContext();

  const router = createBrowserRouter([
    {
      element: <AppLayout />,
      children: [
        {
          element: <DashboardPage />,
          index: true,
          loader: dashboardPageLoader(userContext, dashboardContext),
        },
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
          action: transfersPageAction(userContext),
          errorElement: <TransferErrorPage />,
        },
        {
          path: '/transfers/new/income',
          element: <TransfersNewPage type={TransferDTOType.Income} />,
          // errorElement: <TransferErrorPage />,
          action: transferNewPageAction(userContext),
        },
        {
          path: '/transfers/new/expense',
          element: <TransfersNewPage type={TransferDTOType.Expense} />,
          // errorElement: <TransferErrorPage />,
          action: transferNewPageAction(userContext),
        },
        {
          path: Paths.transferEdit,
          element: <TransfersEditPage />,
          loader: transferEditPageLoader(userContext),
          action: transferEditPageAction(userContext),
          // errorElement: <TransferErrorPage />,
        },
      ],
    },
  ]);

  return <RouterProvider router={router} />;
}
