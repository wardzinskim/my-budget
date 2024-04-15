import { Button, Card, Container, Stack, Typography } from '@mui/material';
import { Iconify, RouterLink } from '@repo/minimal-ui';
import { BudgetsTable } from '../budgets-table';
import { BudgetListItemDTO } from '@repo/api-client';
import { useLoaderData } from 'react-router-dom';

export const BudgetsView: React.FC = () => {
  const budgets: BudgetListItemDTO[] = useLoaderData() as BudgetListItemDTO[];

  return (
    <>
      <Container>
        <Stack
          direction="row"
          alignItems="center"
          justifyContent="space-between"
          mb={5}
        >
          <Typography variant="h4">Budgets</Typography>

          <Button
            component={RouterLink}
            href="/budgets/new"
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
