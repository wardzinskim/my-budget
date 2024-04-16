import { Helmet } from 'react-helmet-async';
import { BudgetCategoriesView } from '../../../../components/budgets/view/budget-categories-view';

const BudgetCategoriesPage = () => {
  return (
    <>
      <Helmet>
        <title> Budget categories | MyBudget </title>
      </Helmet>
      <BudgetCategoriesView />
    </>
  );
};

export default BudgetCategoriesPage;
