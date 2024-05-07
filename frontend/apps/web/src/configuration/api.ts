import {
  BudgetApi,
  CategoryApi,
  Configuration,
  TransferApi,
} from '@repo/api-client';

const configuration = new Configuration({
  basePath: 'https://localhost:60818',
});

export const budgetApi = new BudgetApi(configuration);
export const transferApi = new TransferApi(configuration);
export const categoryApi = new CategoryApi(configuration);
