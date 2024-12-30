import { Container, Stack, Typography } from '@mui/material';
import { BudgetDTO } from '@repo/api-client';
import { Iconify, NavItem } from '@repo/minimal-ui';
import { NavigationBar } from '@repo/minimal-ui';
import { Suspense, useMemo } from 'react';
import { Outlet, useLoaderData } from 'react-router-dom';
import { useAuth } from 'react-oidc-context';

const buildBudgetNavigation: (
  budget: BudgetDTO,
  currentUserId: string | undefined
) => Array<NavItem> = (
  budget: BudgetDTO,
  currentUserId: string | undefined
) => {
  const routes = [
    {
      path: `/budgets/${budget.id}/categories`,
      title: 'Categories',
      icon: <Iconify icon="material-symbols-light:category-outline" />,
    },
  ];
  if (budget.ownerId === currentUserId) {
    routes.push({
      path: `/budgets/${budget.id}/shares`,
      title: 'Shares',
      icon: <Iconify icon="material-symbols-light:share" />,
    });
  }

  return routes;
};

export const BudgetDetailsView: React.FC = () => {
  const auth = useAuth();
  const budget: BudgetDTO = useLoaderData();

  const navigation = useMemo<Array<NavItem>>(
    () => buildBudgetNavigation(budget, auth.user?.profile?.sub),
    [budget, auth.user?.profile?.sub]
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
