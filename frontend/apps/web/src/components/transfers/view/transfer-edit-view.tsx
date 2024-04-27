import { Container, Stack, Typography } from '@mui/material';
import { useLoaderData } from 'react-router-dom';
import { TransferDTO } from '@repo/api-client';
import { TransferForm } from '../transfer-form';

interface TransferEditViewProps {}

export const TransferEditView: React.FC<TransferEditViewProps> = () => {
  const budget = useLoaderData() as TransferDTO;

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
