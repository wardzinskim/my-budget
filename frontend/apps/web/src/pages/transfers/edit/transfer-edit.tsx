import { Helmet } from 'react-helmet-async';
import { TransferEditView } from '../../../components/transfers/view/transfer-edit-view';

interface TransfersEditPageParams {}

const TransfersEditPage: React.FC<TransfersEditPageParams> = () => {
  return (
    <>
      <Helmet>
        <title> Edit Transfer | MyBudget </title>
      </Helmet>

      <TransferEditView />
    </>
  );
};

export default TransfersEditPage;
