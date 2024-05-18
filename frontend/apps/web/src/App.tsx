import './App.css';
import { AlertDialogProvider, ThemeProvider } from '@repo/minimal-ui';
import Router from './routes/router';
import { IUserContextState, UserContext } from './hooks/user-context';
import { useEffect, useState } from 'react';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFnsV3';
import { enGB } from 'date-fns/locale';
import {
  DashboardContext,
  IDashboardContextState,
} from './components/dashboard/hooks/dashboard-context';
import { hasAuthParams, useAuth } from 'react-oidc-context';

function App() {
  const [userContextState, setUserContextState] = useState<IUserContextState>(
    {}
  );
  const [dashboardContextState, setDashboardContextState] =
    useState<IDashboardContextState>({
      year: new Date().getFullYear(),
      month: new Date().getMonth() + 1,
    });

  const auth = useAuth();
  const [hasTriedSignin, setHasTriedSignin] = useState(false);

  // automatically sign-in
  useEffect(() => {
    if (
      !hasAuthParams() &&
      !auth.isAuthenticated &&
      !auth.activeNavigator &&
      !auth.isLoading &&
      !hasTriedSignin
    ) {
      // eslint-disable-next-line @typescript-eslint/no-floating-promises
      auth.signinRedirect();
      setHasTriedSignin(true);
    }
  }, [auth, hasTriedSignin]);

  if (auth.isLoading) {
    return <div>Signing you in/out...</div>;
  }

  if (!auth.isAuthenticated) {
    return <div>Unable to log in</div>;
  }

  return (
    <ThemeProvider>
      <LocalizationProvider dateAdapter={AdapterDateFns} adapterLocale={enGB}>
        <AlertDialogProvider>
          <UserContext.Provider
            value={{
              userContext: userContextState,
              setUserContext: setUserContextState,
            }}
          >
            <DashboardContext.Provider
              value={{
                dashboardContext: dashboardContextState,
                setDashboardContext: setDashboardContextState,
              }}
            >
              <Router></Router>
            </DashboardContext.Provider>
          </UserContext.Provider>
        </AlertDialogProvider>
      </LocalizationProvider>
    </ThemeProvider>
  );
}

export default App;
