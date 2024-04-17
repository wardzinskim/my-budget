import { BudgetApi, Configuration } from '@repo/api-client';

const configuration = new Configuration({
  basePath: 'https://localhost:61898',
});

export const budgetApi = new BudgetApi(configuration);
