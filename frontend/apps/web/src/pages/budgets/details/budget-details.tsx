import { Helmet } from 'react-helmet-async';
import { BudgetDetails } from '../../../components/budgets/view/budget-details';

const BudgetDetailsPage = () => {
  return (
    <>
      <Helmet>
        <title> Budget details | MyBudget </title>
      </Helmet>

      <BudgetDetails />
    </>
  );
};

export default BudgetDetailsPage;
