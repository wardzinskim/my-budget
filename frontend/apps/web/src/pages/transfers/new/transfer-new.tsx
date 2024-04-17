import { Helmet } from 'react-helmet-async';
import { TransferDTOType } from '@repo/api-client';

interface TransfersNewPageParams {
  type: TransferDTOType;
}

const TransfersNewPage: React.FC<TransfersNewPageParams> = ({ type }) => {
  return (
    <>
      <Helmet>
        <title> New Budget | MyBudget </title>
      </Helmet>

      {type}
    </>
  );
};

export default TransfersNewPage;
