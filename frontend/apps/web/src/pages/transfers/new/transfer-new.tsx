import { Helmet } from 'react-helmet-async';
import { TransferDTOType } from '@repo/api-client';
import { TransferNewView } from '../../../components/transfers/view/transfer-new-view';

interface TransfersNewPageParams {
  type: TransferDTOType;
}

const TransfersNewPage: React.FC<TransfersNewPageParams> = ({ type }) => {
  return (
    <>
      <Helmet>
        <title> New Budget | myBudget </title>
      </Helmet>

      <TransferNewView type={type} />
    </>
  );
};

export default TransfersNewPage;
