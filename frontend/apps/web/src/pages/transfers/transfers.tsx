import { Helmet } from 'react-helmet-async';
import { TransfersView } from '../../components/transfers/view/transfers-view';

const TransfersPage = () => {
  return (
    <>
      <Helmet>
        <title> Transfers | MyBudget </title>
      </Helmet>

      <TransfersView />
    </>
  );
};

export default TransfersPage;
