import { WebStorageStateStore } from 'oidc-client-ts';
import { AuthProviderProps } from 'react-oidc-context';

export const oidcConfig: AuthProviderProps = {
  authority: 'https://mybudget.identity:8081',
  automaticSilentRenew: true,
  client_id: 'MyBudget.Frontend',
  redirect_uri: 'http://localhost:5173',
  response_type: 'code',
  scope: 'openid profile email MyBudget',
  loadUserInfo: true,
  post_logout_redirect_uri: window.location.origin,
  userStore: new WebStorageStateStore({ store: window.localStorage }),
  onSigninCallback: () => {
    window.history.replaceState({}, document.title, window.location.pathname);
  },
};
