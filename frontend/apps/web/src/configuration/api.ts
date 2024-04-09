import { BudgetApi, Configuration } from '@repo/api-client';

const configuration = new Configuration({
  basePath: 'https://localhost:58231',
});

export const budgetApi = new BudgetApi(configuration);
