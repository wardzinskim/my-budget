import { BudgetApi, Configuration } from '@repo/api-client';

const configuration = new Configuration({
  basePath: 'https://localhost:57105',
});

export const budgetApi = new BudgetApi(configuration);
