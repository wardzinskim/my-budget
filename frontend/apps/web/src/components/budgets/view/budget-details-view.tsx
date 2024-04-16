import { Container, Stack, Typography } from '@mui/material';
import { BudgetDTO } from '@repo/api-client';
import { Iconify, NavItem } from '@repo/minimal-ui';
import { NavigationBar } from '@repo/minimal-ui';
import { Suspense, useMemo } from 'react';
import { Outlet, useLoaderData } from 'react-router-dom';

const buildBudgetNavigation: (id: string) => Array<NavItem> = (id: string) => [
  {
    path: `/budgets/${id}`,
    title: 'Categories',
    icon: <Iconify icon="material-symbols-light:category-outline" />,
  },
];

export const BudgetDetailsView: React.FC = () => {
  const budget: BudgetDTO = useLoaderData() as BudgetDTO;

  const navigation = useMemo<Array<NavItem>>(
    () => buildBudgetNavigation(budget.id!),
    [budget]
  );

  return (
    <>
      <Container>
        <Stack
          direction="column"
          alignItems="left"
          justifyContent="space-between"
          mb={5}
        >
          <Typography variant="h4">Budget: {budget.name}</Typography>
          <Typography variant="body2">{budget.description}</Typography>
        </Stack>

        <NavigationBar type={'horizontal'} items={navigation} />

        <Suspense>
          <Outlet />
        </Suspense>
      </Container>
    </>
  );
};
