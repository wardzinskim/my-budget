import { BudgetApi, Configuration } from '@repo/api-client';

const configuration = new Configuration({
  basePath: 'https://localhost:51451',
});

export const budgetApi = new BudgetApi(configuration);
