import { Button, Card, Container, Stack, Typography } from '@mui/material';
import { Iconify, RouterLink, fDate } from '@repo/minimal-ui';
import { useUserContext } from '../../../hooks/user-context';
import { TransfersTable } from '../transfers-table';
import { TransfersQueryResponse } from '@repo/api-client';
import { useLoaderData } from 'react-router-dom';
import { TransferFilters } from '../transfers-filters';

export const TransfersView: React.FC = () => {
  const [userContext] = useUserContext();
  const transfersResponse = useLoaderData() as TransfersQueryResponse;
  return (
    <>
      <Container>
        <Stack
          direction="row"
          alignItems="center"
          justifyContent="space-between"
          mb={5}
        >
          <Typography variant="h4">
            Transfers for budget: {userContext.budget?.name}
          </Typography>

          <Stack direction="row" spacing={2}>
            <Button
              component={RouterLink}
              href="/transfers/new/income"
              variant="contained"
              color="inherit"
              startIcon={<Iconify icon="eva:plus-fill" />}
            >
              Add Income
            </Button>

            <Button
              component={RouterLink}
              href="/transfers/new/expense"
              variant="contained"
              color="inherit"
              startIcon={<Iconify icon="eva:plus-fill" />}
            >
              Add Expense
            </Button>
          </Stack>
        </Stack>

        <Card>
          <TransferFilters />
          <Stack
            spacing={2}
            alignItems="center"
            justifyContent="space-between"
            margin={2}
            direction="row"
          >
            <Typography variant="body2">
              Transfers: {fDate(transfersResponse.dateFrom!)} -{' '}
              {fDate(transfersResponse.dateTo!)}{' '}
            </Typography>
          </Stack>
          <TransfersTable transfers={transfersResponse.transfers ?? []} />
        </Card>
      </Container>
    </>
  );
};
