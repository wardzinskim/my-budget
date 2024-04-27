import { Container, Stack, Typography } from '@mui/material';
import { TransferDTOType } from '@repo/api-client';
import { TransferForm } from '../transfer-form';

interface TransferNewViewProps {
  type: TransferDTOType;
}

export const TransferNewView: React.FC<TransferNewViewProps> = ({ type }) => {
  return (
    <>
      <Container>
        <Stack
          direction="row"
          alignItems="center"
          justifyContent="space-between"
          mb={5}
        >
          <Typography variant="h4">Create new {type}</Typography>
        </Stack>

        <TransferForm type={type} submitButtonName="Create" />
      </Container>
    </>
  );
};
