import { Helmet } from 'react-helmet-async';
import { BudgetDetailsView } from '../../../components/budgets/view/budget-details-view';

const BudgetDetailsPage = () => {
  return (
    <>
      <Helmet>
        <title> Budget details | MyBudget </title>
      </Helmet>

      <BudgetDetailsView />
    </>
  );
};

export default BudgetDetailsPage;
