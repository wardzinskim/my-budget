import { IconButton, Stack, Tooltip, Typography } from '@mui/material';
import { useUserContext } from '../../hooks/user-context';
import { Iconify, RouterLink } from '@repo/minimal-ui';

export const Header = () => {
  const [userContext] = useUserContext();

  return (
    <Stack
      direction="row"
      sx={{ alignItems: 'center', justifyContent: 'center' }}
    >
      <Typography variant="h4" component="h1" color="textPrimary">
        {userContext?.budget?.name}
      </Typography>

      <Tooltip title="Add Income" placement="left">
        <IconButton
          component={RouterLink}
          href="/transfers/new/income"
          size="large"
          color="default"
        >
          <Iconify icon="game-icons:receive-money" />
        </IconButton>
      </Tooltip>

      <Tooltip title="Add Tax" placement="left">
        <IconButton
          component={RouterLink}
          href="/transfers/new/tax"
          size="large"
          color="default"
        >
          <Iconify icon="hugeicons:taxes" />
        </IconButton>
      </Tooltip>

      <Tooltip title="Add Expense" placement="left">
        <IconButton
          component={RouterLink}
          href="/transfers/new/expense"
          size="large"
          color="default"
        >
          <Iconify icon="game-icons:pay-money" />
        </IconButton>
      </Tooltip>
    </Stack>
  );
};
