import { Helmet } from 'react-helmet-async';

const BudgetsPage = () => {
  // const budgets: BudgetDTO[] = useLoaderData() as BudgetDTO[];

  return (
    <>
      <Helmet>
        <title> Budget | MyBudget </title>
      </Helmet>
      BudgetList
      {/* <AppView /> */}
    </>
  );
};

export default BudgetsPage;
