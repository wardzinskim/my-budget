import { Button, Card, Container, Stack, Typography } from '@mui/material';
import {
  BudgetDTO,
  BudgetDTOStatus,
  CategoryDTOStatus,
} from '@repo/api-client';
import {
  ColumnDefinition,
  Iconify,
  Label,
  MinimalTable,
} from '@repo/minimal-ui';
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
    render: (item: BudgetDTO) => (
      <>
        {item.categories?.map((x) => (
          <Label
            color={
              x.status == CategoryDTOStatus.Archived ? 'default' : 'success'
            }
            variant="outlined"
            key={x.name}
          >
            {x.name}
          </Label>
        ))}
      </>
    ),
  },
  {
    id: 'status',
    label: 'Status',
    align: 'center',
    render: (item: BudgetDTO) => (
      <Label
        color={item.status === BudgetDTOStatus.Open ? 'success' : 'default'}
        variant="filled"
      >
        {item.status}
      </Label>
    ),
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
          <MinimalTable<BudgetDTO>
            columns={BudgetTableColumns}
            items={budgets}
            withSelection={false}
          ></MinimalTable>
        </Card>
      </Container>
    </>
  );
};

export default BudgetsPage;
