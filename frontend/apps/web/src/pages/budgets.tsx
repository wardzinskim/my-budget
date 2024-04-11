import { Button, Card, Container, Stack, Typography } from '@mui/material';
import { BudgetDTO } from '@repo/api-client';
import { Iconify } from '@repo/minimal-ui';
import { Helmet } from 'react-helmet-async';
import { useLoaderData } from 'react-router-dom';
import { BudgetsTable } from '../components/budgets/budgets-table';

const BudgetsPage = () => {
  const budgets: BudgetDTO[] = useLoaderData() as BudgetDTO[];

  return (
    <>
      <Helmet>
        <title> Budget | MyBudget </title>
      </Helmet>

      <Container>
        <Stack
          direction="row"
          alignItems="center"
          justifyContent="space-between"
          mb={5}
        >
          <Typography variant="h4">Budgets</Typography>

          <Button
            variant="contained"
            color="inherit"
            startIcon={<Iconify icon="eva:plus-fill" />}
          >
            New Budget
          </Button>
        </Stack>

        <Card>
          <BudgetsTable budgets={budgets} />
        </Card>
      </Container>
    </>
  );
};

export default BudgetsPage;
