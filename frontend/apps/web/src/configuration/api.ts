import {
  BudgetApi,
  BudgetStatisticsApi,
  CategoryApi,
  Configuration,
  TransferApi,
} from '@repo/api-client';
import { User } from 'oidc-client-ts';
import { oidcConfig } from '../config';

function getUser(): User | null {
  const oidcStorage = localStorage.getItem(
    `oidc.user:${oidcConfig.authority}:${oidcConfig.client_id}`
  );
  if (!oidcStorage) {
    return null;
  }

  return User.fromStorageString(oidcStorage);
}

const configuration = new Configuration({
  basePath: 'https://localhost:48081',
  accessToken: () => {
    return getUser()?.access_token ?? '';
  },
});

export const budgetApi = new BudgetApi(configuration);
export const transferApi = new TransferApi(configuration);
export const categoryApi = new CategoryApi(configuration);
export const statisticsApi = new BudgetStatisticsApi(configuration);
