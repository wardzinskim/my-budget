import './App.css';
import { ThemeProvider } from '@repo/minimal-ui';
import Router from './routes/router';
import { IUserContextState, UserContext } from './hooks/user-context';
import { useState } from 'react';

function App() {
  const [userContextState, setUserContextState] = useState<IUserContextState>(
    {}
  );

  return (
    <ThemeProvider>
      <UserContext.Provider
        value={{
          userContext: userContextState,
          setUserContext: setUserContextState,
        }}
      >
        <Router></Router>
      </UserContext.Provider>
    </ThemeProvider>
  );
}

export default App;
