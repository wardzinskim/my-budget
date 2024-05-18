import React, { Suspense } from 'react';
import ReactDOM from 'react-dom/client';
import App from './App.tsx';
import './index.css';
import { HelmetProvider } from 'react-helmet-async';
import { SnackbarProvider } from 'notistack';
import { AuthProvider } from 'react-oidc-context';
import { oidcConfig } from './config.ts';

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <HelmetProvider>
      <Suspense>
        <SnackbarProvider
          maxSnack={3}
          autoHideDuration={2000}
          preventDuplicate
          anchorOrigin={{
            vertical: 'top',
            horizontal: 'right',
          }}
        >
          <AuthProvider {...oidcConfig}>
            <App />
          </AuthProvider>
        </SnackbarProvider>
      </Suspense>
    </HelmetProvider>
  </React.StrictMode>
);
