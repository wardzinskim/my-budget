import { Card } from '@mui/material';
import { BudgetDTO } from '@repo/api-client';
import { useRouteLoaderData } from 'react-router-dom';
import { BudgetCategoryList } from '../budget-category-list';

export const BudgetCategoriesView: React.FC = () => {
  const budget: BudgetDTO = useRouteLoaderData('budget-details') as BudgetDTO;

  return (
    <Card>
      <BudgetCategoryList items={budget.categories ?? []} />
    </Card>
  );
};
