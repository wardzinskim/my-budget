import { Helmet } from 'react-helmet-async';
import { BudgetsView } from '../../components/budgets/view/budgets-view';

const BudgetsPage = () => {
  return (
    <>
      <Helmet>
        <title> Budget | myBudget </title>
      </Helmet>

      <BudgetsView />
    </>
  );
};

export default BudgetsPage;
