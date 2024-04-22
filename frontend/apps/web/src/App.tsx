import './App.css';
import { AlertDialogProvider, ThemeProvider } from '@repo/minimal-ui';
import Router from './routes/router';
import { IUserContextState, UserContext } from './hooks/user-context';
import { useState } from 'react';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFnsV3';
import { enGB } from 'date-fns/locale';

function App() {
  const [userContextState, setUserContextState] = useState<IUserContextState>(
    {}
  );

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
            <Router></Router>
          </UserContext.Provider>
        </AlertDialogProvider>
      </LocalizationProvider>
    </ThemeProvider>
  );
}

export default App;
