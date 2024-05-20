import {
  BudgetApi,
  BudgetStatisticsApi,
  CategoryApi,
  Configuration,
  TransferApi,
} from '@repo/api-client';

const configuration = new Configuration({
  basePath: 'https://localhost:48081',
});

export const budgetApi = new BudgetApi(configuration);
export const transferApi = new TransferApi(configuration);
export const categoryApi = new CategoryApi(configuration);
export const statisticsApi = new BudgetStatisticsApi(configuration);
