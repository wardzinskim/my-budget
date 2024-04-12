import { BudgetDTO } from '@repo/api-client';
import { Helmet } from 'react-helmet-async';
import { useLoaderData } from 'react-router-dom';
import { BudgetsView } from '../../components/budgets/view/budgets-view';

const BudgetsPage = () => {
  const budgets: BudgetDTO[] = useLoaderData() as BudgetDTO[];

  return (
    <>
      <Helmet>
        <title> Budget | MyBudget </title>
      </Helmet>

      <BudgetsView budgets={budgets} />
    </>
  );
};

export default BudgetsPage;
