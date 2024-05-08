import { LoaderFunction } from 'react-router-dom';
import { IUserContextState } from '../../hooks/user-context';
import { statisticsApi } from '../../configuration/api';
import { BudgetTotals } from '@repo/api-client';
import { IDashboardContextState } from '../../components/dashboard/hooks/dashboard-context';

export interface DashboardLoaderResult {
  totals: BudgetTotals;
}

export const loader: (
  userContext: IUserContextState,
  dashboardContext: IDashboardContextState
) => LoaderFunction = (userContext, dashboardContext) => async () => {
  if (userContext.budget == undefined) return null;

  const [totals] = await Promise.all([
    fetchTotals(
      userContext.budget.id!,
      dashboardContext.year,
      dashboardContext.month
    ),
  ]);

  return { totals } as DashboardLoaderResult;
};

const fetchTotals = async (budgetId: string, year?: number, month?: number) => {
  const response = await statisticsApi.getBudgetTotals(budgetId, year, month);
  return response.data;
};
