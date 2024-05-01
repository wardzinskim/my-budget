import { Container, Grid, Typography } from '@mui/material';
import {
  DashboardContext,
  IDashboardContextState,
} from '../hooks/dashboard-context';
import { useState } from 'react';
import { DashboardContextPicker } from '../dashboard-context-picker';

export const DashboardView: React.FC = () => {
  const [dashboardContextState, setDashboardContextState] =
    useState<IDashboardContextState>({
      year: new Date().getFullYear(),
      month: new Date().getMonth(),
    });

  return (
    <Container maxWidth="xl">
      <Typography variant="h4" sx={{ mb: 5 }}>
        Hi, Welcome back 👋
      </Typography>
      <DashboardContext.Provider
        value={{
          dashboardContext: dashboardContextState,
          setDashboardContext: setDashboardContextState,
        }}
      >
        <DashboardContextPicker />

        <Grid container spacing={3}></Grid>
      </DashboardContext.Provider>
    </Container>
  );
};
