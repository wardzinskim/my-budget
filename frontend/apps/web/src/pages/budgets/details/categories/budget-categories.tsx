import { Helmet } from 'react-helmet-async';
import { BudgetCategoriesView } from '../../../../components/budgets/view/budget-categories-view';

const BudgetCategoriesPage = () => {
  return (
    <>
      <Helmet>
        <title> Budget categories | myBudget </title>
      </Helmet>
      <BudgetCategoriesView />
    </>
  );
};

export default BudgetCategoriesPage;
