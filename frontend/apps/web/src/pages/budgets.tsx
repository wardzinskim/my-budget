import { Helmet } from 'react-helmet-async';

// ----------------------------------------------------------------------

export default function BudgetsPage() {
  return (
    <>
      <Helmet>
        <title> Budget | MyBudget </title>
      </Helmet>
      BudgetList
      {/* <AppView /> */}
    </>
  );
}
