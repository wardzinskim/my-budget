import { Button, Card, Container, Stack, Typography } from '@mui/material';
import { BudgetDTO } from '@repo/api-client';
import { ColumnDefinition, MinimalTable } from '@repo/minimal-ui';
import { Helmet } from 'react-helmet-async';
import { useLoaderData } from 'react-router-dom';

const BudgetTableColumns: Array<ColumnDefinition<BudgetDTO>> = [
  {
    id: 'name',
    label: 'Name',
    align: 'left',
  },
  {
    id: 'description',
    label: 'Description',
    align: 'left',
  },
  {
    id: 'categories',
    label: 'Defined Categories',
    align: 'left',
    render: (item: BudgetDTO) => <>{item.categories?.map((x) => x.name)}</>,
  },
];

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
          <Typography variant="h4">budgets</Typography>

          <Button
            variant="contained"
            color="inherit"
            // startIcon={<Iconify icon="eva:plus-fill" />}
          >
            New Budget
          </Button>
        </Stack>

        <Card>
          <MinimalTable<BudgetDTO>
            columns={BudgetTableColumns}
            items={budgets}
          ></MinimalTable>
        </Card>
      </Container>
    </>
  );
};

export default BudgetsPage;
