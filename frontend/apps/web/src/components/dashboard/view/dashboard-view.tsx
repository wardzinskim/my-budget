import { Container, Stack, Typography } from '@mui/material';
import { DashboardContextPicker } from '../dashboard-context-picker';
import { TotalExpensesViewer } from '../widgets/total-expenses-viewer';
import { useLoaderData } from 'react-router-dom';
import { ExpensesByCategoryBarChart } from '../widgets/expenses-by-category-bar-chart';
import { ExpensesByCategoryPieChart } from '../widgets/expenses-by-category-pie-chart';
import { BalanceViewer } from '../widgets/balance-viewer';
import { ExpensesToIncomesRatio } from '../widgets/expenses-to-incomes-ratio';
import { DashboardLoaderResult } from '../../../pages/dashboard/dashboard.loader';
import { TotalIncomesViewer } from '../widgets/total-incomes-viewer';
import Grid from '@mui/material/Grid2';

export const DashboardView: React.FC = () => {
  const loaderData = useLoaderData<DashboardLoaderResult>();
  if (loaderData == null) return <></>;

  return (
    <Container maxWidth="xl" sx={{ px: 1 }}>
      <Stack
        direction="row"
        spacing={2}
        useFlexGap
        sx={{ flexWrap: 'wrap' }}
        justifyContent="space-between"
      >
        <Typography variant="h4">Hi, Welcome back 👋</Typography>

        <DashboardContextPicker />
      </Stack>

      <Grid container paddingTop={2} spacing={3}>
        <Grid
          container
          size={{ xs: 12, sm: 6, md: 4 }}
          alignSelf="flex-start"
          spacing={3}
        >
          <Grid size={{ xs: 12 }}>
            <TotalIncomesViewer
              value={loaderData.totals.incomes ?? 0}
              tax={loaderData.totals.taxes ?? 0}
            />
          </Grid>
          <Grid size={{ xs: 12 }}>
            <TotalExpensesViewer value={loaderData.totals.expenses ?? 0} />
          </Grid>
        </Grid>

        <Grid size={{ xs: 12, sm: 6, md: 4 }}>
          <BalanceViewer
            value={
              (loaderData.totals.incomes ?? 0) -
              (loaderData.totals.taxes ?? 0) -
              (loaderData.totals.expenses ?? 0)
            }
          />
        </Grid>

        <Grid size={{ xs: 12, sm: 6, md: 4 }}>
          <ExpensesToIncomesRatio
            expenses={loaderData.totals.expenses ?? 0}
            incomes={
              (loaderData.totals.incomes ?? 0) - (loaderData.totals.taxes ?? 0)
            }
          />
        </Grid>

        <Grid size={{ xs: 12 }}>
          <ExpensesByCategoryBarChart
            categories={loaderData.incomesGroupedByCategory}
          />
        </Grid>

        <Grid size={{ xs: 12 }}>
          <ExpensesByCategoryPieChart
            categories={loaderData.incomesGroupedByCategory}
          />
        </Grid>
      </Grid>
    </Container>
  );
};
