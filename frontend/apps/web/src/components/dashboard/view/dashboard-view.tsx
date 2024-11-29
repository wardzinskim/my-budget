import { Container, Grid, Typography } from '@mui/material';
import { DashboardContextPicker } from '../dashboard-context-picker';
import { TotalsViewer } from '../widgets/totals-viewer';
import { TransferDTOType } from '@repo/api-client';
import { useLoaderData } from 'react-router-dom';
import { ExpensesByCategoryBarChart } from '../widgets/expenses-by-category-bar-chart';
import { ExpensesByCategoryPieChart } from '../widgets/expenses-by-category-pie-chart';
import { BalanceViewer } from '../widgets/balance-viewer';
import { ExpensesToIncomesRatio } from '../widgets/expenses-to-incomes-ratio';
import { DashboardLoaderResult } from '../../../pages/dashboard/dashboard.loader';

export const DashboardView: React.FC = () => {
  const loaderData = useLoaderData<DashboardLoaderResult>();
  if (loaderData == null) return <></>;

  return (
    <Container maxWidth="xl">
      <Typography variant="h4" sx={{ mb: 5 }}>
        Hi, Welcome back 👋
      </Typography>

      <DashboardContextPicker />

      <Grid container paddingTop={2} spacing={3}>
        <Grid
          container
          item
          xs={12}
          sm={6}
          md={4}
          alignSelf="flex-start"
          spacing={3}
        >
          <Grid item xs={12}>
            <TotalsViewer
              type={TransferDTOType.Income}
              value={loaderData.totals.incomes ?? 0}
            />
          </Grid>
          <Grid item xs={12}>
            <TotalsViewer
              type={TransferDTOType.Expense}
              value={loaderData.totals.expenses ?? 0}
            />
          </Grid>
        </Grid>

        <Grid item xs={12} sm={6} md={4}>
          <BalanceViewer
            value={
              (loaderData.totals.incomes ?? 0) -
              (loaderData.totals.expenses ?? 0)
            }
          />
        </Grid>

        <Grid item xs={12} sm={6} md={4}>
          <ExpensesToIncomesRatio
            expenses={loaderData.totals.expenses ?? 0}
            incomes={loaderData.totals.incomes ?? 0}
          />
        </Grid>

        <Grid item xs={12}>
          <ExpensesByCategoryBarChart
            categories={loaderData.incomesGroupedByCategory}
          />
        </Grid>

        <Grid item xs={12}>
          <ExpensesByCategoryPieChart
            categories={loaderData.incomesGroupedByCategory}
          />
        </Grid>
      </Grid>
    </Container>
  );
};
