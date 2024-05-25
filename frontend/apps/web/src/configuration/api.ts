import {
  BudgetApi,
  BudgetStatisticsApi,
  CategoryApi,
  Configuration,
  TransferApi,
} from '@repo/api-client';
import { OidcClientSettings, User } from 'oidc-client-ts';
import { oidcConfig } from '../config';

function getUser(): User | null {
  const clientSettings = oidcConfig as OidcClientSettings;

  const oidcStorage = localStorage.getItem(
    `oidc.user:${clientSettings.authority}:${clientSettings.client_id}`
  );
  if (!oidcStorage) {
    return null;
  }

  return User.fromStorageString(oidcStorage);
}

const configuration = new Configuration({
  basePath: window.MyBudgetConfig?.backendUrl,
  accessToken: () => {
    return getUser()?.access_token ?? '';
  },
});

export const budgetApi = new BudgetApi(configuration);
export const transferApi = new TransferApi(configuration);
export const categoryApi = new CategoryApi(configuration);
export const statisticsApi = new BudgetStatisticsApi(configuration);
