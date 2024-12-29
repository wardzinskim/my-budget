import { Card } from '@mui/material';
import { BudgetDTO } from '@repo/api-client';
import { useRouteLoaderData } from 'react-router-dom';
import { BudgetShareList } from '../budget-share-list';

export const BudgetSharesView: React.FC = () => {
  const budget: BudgetDTO = useRouteLoaderData('budget-details') as BudgetDTO;

  return (
    <Card>
      <BudgetShareList items={budget.shares ?? []} />
    </Card>
  );
};
