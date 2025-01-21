import { LoaderFunction } from 'react-router-dom';
import { IUserContextState } from '../../hooks/user-context';
import { statisticsApi } from '../../configuration/api';
import { BudgetTotals, CategoryValue, TransferDTOType } from '@repo/api-client';
import { IDashboardContextState } from '../../components/dashboard/hooks/dashboard-context';

export interface DashboardLoaderResult {
  totals: BudgetTotals;
  incomesGroupedByCategory: CategoryValue[];
  yearlyIncomesGroupedByCategory: CategoryValue[];
  yearlyExpensesGroupedByCategory: CategoryValue[];
}

export const loader: (
  userContext: IUserContextState,
  dashboardContext: IDashboardContextState
) => LoaderFunction = (userContext, dashboardContext) => async () => {
  if (userContext.budget == undefined) return null;

  const [
    totals,
    incomesGroupedByCategory,
    yearlyIncomesGroupedByCategory,
    yearlyExpensesGroupedByCategory,
  ] = await Promise.all([
    fetchTotals(
      userContext.budget.id!,
      dashboardContext.year,
      dashboardContext.month
    ),
    fetchExpensesTotalsGroupedByCategory(
      userContext.budget.id!,
      dashboardContext.year,
      dashboardContext.month
    ),
    fetchTransfersTotalsGroupedByCategory(
      TransferDTOType.Income,
      userContext.budget.id!,
      dashboardContext.year
    ),
    fetchTransfersTotalsGroupedByCategory(
      TransferDTOType.Expense,
      userContext.budget.id!,
      dashboardContext.year
    ),
  ]);

  return {
    totals,
    incomesGroupedByCategory,
    yearlyIncomesGroupedByCategory,
    yearlyExpensesGroupedByCategory,
  } as DashboardLoaderResult;
};

const fetchTotals = async (budgetId: string, year?: number, month?: number) => {
  const response = await statisticsApi.getBudgetTotals(budgetId, year, month);
  return response.data;
};

const fetchExpensesTotalsGroupedByCategory = async (
  budgetId: string,
  year?: number,
  month?: number
) => {
  const response = await statisticsApi.getBudgetTransfersTotalsGropedByCategory(
    budgetId,
    TransferDTOType.Expense,
    year,
    month
  );
  return response.data;
};

const fetchTransfersTotalsGroupedByCategory = async (
  transferType: TransferDTOType,
  budgetId: string,
  year?: number
) => {
  const response = await statisticsApi.getBudgetTransfersTotalsGropedByCategory(
    budgetId,
    transferType,
    year
  );
  return response.data;
};
