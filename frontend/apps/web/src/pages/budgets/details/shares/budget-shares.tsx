import { Helmet } from 'react-helmet-async';
import { BudgetSharesView } from '../../../../components/budgets/view/budget-shares-view';

const BudgetSharesPage = () => {
  return (
    <>
      <Helmet>
        <title> Budget shares | myBudget </title>
      </Helmet>
      <BudgetSharesView />
    </>
  );
};

export default BudgetSharesPage;
