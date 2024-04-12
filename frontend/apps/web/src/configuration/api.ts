import { BudgetApi, Configuration } from '@repo/api-client';

const configuration = new Configuration({
  basePath: 'https://localhost:58547',
});

export const budgetApi = new BudgetApi(configuration);
