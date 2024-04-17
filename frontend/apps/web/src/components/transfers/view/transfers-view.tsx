import { Button, Card, Container, Stack, Typography } from '@mui/material';
import { Iconify, RouterLink } from '@repo/minimal-ui';
import { useUserContext } from '../../../hooks/user-context';

export const TransfersView: React.FC = () => {
  const [userContext] = useUserContext();
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

        <Card></Card>
      </Container>
    </>
  );
};
