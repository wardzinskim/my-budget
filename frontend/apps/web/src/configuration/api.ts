import { BudgetApi, Configuration } from '@repo/api-client';

const configuration = new Configuration({
  basePath: 'https://localhost:51730',
});

export const budgetApi = new BudgetApi(configuration);
