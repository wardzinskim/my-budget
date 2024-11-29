import { Container, Stack, Typography } from '@mui/material';
import { useLoaderData } from 'react-router-dom';
import { TransferForm } from '../transfer-form';
import { TransferDTO } from '@repo/api-client';

interface TransferEditViewProps {}

export const TransferEditView: React.FC<TransferEditViewProps> = () => {
  const budget = useLoaderData<TransferDTO>();

  if (!budget) return <></>;

  return (
    <>
      <Container>
        <Stack
          direction="row"
          alignItems="center"
          justifyContent="space-between"
          mb={5}
        >
          <Typography variant="h4">Edit new {budget.type}</Typography>
        </Stack>

        <TransferForm
          type={budget.type!}
          initialData={budget}
          submitButtonName="Update"
        />
      </Container>
    </>
  );
};
