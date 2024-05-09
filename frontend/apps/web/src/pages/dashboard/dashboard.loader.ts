import { LoaderFunction } from 'react-router-dom';
import { IUserContextState } from '../../hooks/user-context';
import { statisticsApi } from '../../configuration/api';
import { BudgetTotals, CategoryValue, TransferDTOType } from '@repo/api-client';
import { IDashboardContextState } from '../../components/dashboard/hooks/dashboard-context';

export interface DashboardLoaderResult {
  totals: BudgetTotals;
  incomesGroupedByCategory: CategoryValue[];
}

export const loader: (
  userContext: IUserContextState,
  dashboardContext: IDashboardContextState
) => LoaderFunction = (userContext, dashboardContext) => async () => {
  if (userContext.budget == undefined) return null;

  const [totals, incomesGroupedByCategory] = await Promise.all([
    fetchTotals(
      userContext.budget.id!,
      dashboardContext.year,
      dashboardContext.month
    ),
    fetchIncomesTotalsGroupedByCategory(
      userContext.budget.id!,
      dashboardContext.year,
      dashboardContext.month
    ),
  ]);

  return { totals, incomesGroupedByCategory } as DashboardLoaderResult;
};

const fetchTotals = async (budgetId: string, year?: number, month?: number) => {
  const response = await statisticsApi.getBudgetTotals(budgetId, year, month);
  return response.data;
};

const fetchIncomesTotalsGroupedByCategory = async (
  budgetId: string,
  year?: number,
  month?: number
) => {
  const response = await statisticsApi.getBudgetTransfersTotalsGropedByCategory(
    budgetId,
    TransferDTOType.Income,
    year,
    month
  );
  return response.data;
};
