import { Container, Grid, Typography } from '@mui/material';
import { DashboardContextPicker } from '../dashboard-context-picker';
import { TotalsViewer } from '../widgets/totals-viewer';
import { TransferDTOType } from '@repo/api-client';
import { useLoaderData } from 'react-router-dom';
import { DashboardLoaderResult } from '../../../pages/dashboard/dashboard.loader';
import { ExpensesByCategoryViewer } from '../widgets/expenses-by-category-viewer';

export const DashboardView: React.FC = () => {
  const loaderData = useLoaderData() as DashboardLoaderResult;
  if (loaderData == null) return <></>;

  return (
    <Container maxWidth="xl">
      <Typography variant="h4" sx={{ mb: 5 }}>
        Hi, Welcome back 👋
      </Typography>

      <DashboardContextPicker />

      <Grid container paddingTop={2} spacing={3}>
        <Grid item xs={6}>
          <TotalsViewer
            type={TransferDTOType.Income}
            value={loaderData.totals.incomes!}
          />
        </Grid>
        <Grid item xs={6}>
          <TotalsViewer
            type={TransferDTOType.Expense}
            value={loaderData.totals.expenses!}
          />
        </Grid>

        <Grid item xs={12}>
          <ExpensesByCategoryViewer
            categories={loaderData.incomesGroupedByCategory}
          />
        </Grid>
      </Grid>
    </Container>
  );
};
