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
  yearlyBalancesGroupedByCategory: CategoryValue[];
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
    yearlyTaxesGroupedByCategory,
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
    fetchTransfersTotalsGroupedByCategory(
      TransferDTOType.Tax,
      userContext.budget.id!,
      dashboardContext.year
    ),
  ]);

  const computedYearlyBalancesGroupedByCategory =
    computeYearlyBalancesGroupedByCategory(
      yearlyIncomesGroupedByCategory,
      yearlyExpensesGroupedByCategory,
      yearlyTaxesGroupedByCategory
    );

  return {
    totals,
    incomesGroupedByCategory,
    yearlyIncomesGroupedByCategory,
    yearlyExpensesGroupedByCategory,
    yearlyBalancesGroupedByCategory: computedYearlyBalancesGroupedByCategory,
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

const computeYearlyBalancesGroupedByCategory = (
  yearlyIncomesGroupedByCategory: CategoryValue[],
  yearlyExpensesGroupedByCategory: CategoryValue[],
  yearlyTaxesGroupedByCategory: CategoryValue[]
): CategoryValue[] => {
  const incomesMap = new Map<string, number>();
  const expensesMap = new Map<string, number>();
  const taxesMap = new Map<string, number>();

  for (const cv of yearlyIncomesGroupedByCategory) {
    if (cv.category) incomesMap.set(cv.category, cv.value ?? 0);
  }
  for (const cv of yearlyExpensesGroupedByCategory) {
    if (cv.category) expensesMap.set(cv.category, cv.value ?? 0);
  }
  for (const cv of yearlyTaxesGroupedByCategory) {
    if (cv.category) taxesMap.set(cv.category, cv.value ?? 0);
  }

  const commonCategories = Array.from(incomesMap.keys()).filter(
    (c) => expensesMap.has(c) || taxesMap.has(c)
  );

  return commonCategories.map((category) => ({
    category,
    value:
      (incomesMap.get(category) ?? 0) -
      (expensesMap.get(category) ?? 0) -
      (taxesMap.get(category) ?? 0),
  }));
};
