import { WebStorageStateStore } from 'oidc-client-ts';
import { AuthProviderProps } from 'react-oidc-context';

interface ApplicationConfig {
  backendUrl: string;
  oidcAuthority: string;
  oidcClientId: string;
  oidcScope: string;
}

declare global {
  interface Window {
    MyBudgetConfig: ApplicationConfig;
  }
}

export const oidcConfig: AuthProviderProps = {
  authority: window.MyBudgetConfig?.oidcAuthority,
  automaticSilentRenew: true,
  client_id: window.MyBudgetConfig?.oidcClientId,
  redirect_uri: window.location.origin,
  response_type: 'code',
  scope: window.MyBudgetConfig?.oidcScope,
  loadUserInfo: true,
  post_logout_redirect_uri: window.location.origin,
  userStore: new WebStorageStateStore({ store: window.localStorage }),
  onSigninCallback: () => {
    window.history.replaceState({}, document.title, window.location.pathname);
  },
};
