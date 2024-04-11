import { Helmet } from 'react-helmet-async';
import { BudgetNewView } from '../components/budgets/view/budget-new-view';

const BudgetNewPage = () => {
  return (
    <>
      <Helmet>
        <title> New Budget | MyBudget </title>
      </Helmet>

      <BudgetNewView />
    </>
  );
};

export default BudgetNewPage;
