import { IconButton, Tooltip } from '@mui/material';
import { Iconify } from '@repo/minimal-ui';
import { useAuth } from 'react-oidc-context';

export const LogoutButton = () => {
  const auth = useAuth();

  return (
    <Tooltip title="Logout" placement="left">
      <IconButton
        size="large"
        onClick={() => {
          // eslint-disable-next-line @typescript-eslint/no-floating-promises
          auth?.signoutRedirect();
        }}
      >
        <Iconify icon="hugeicons:logout-04" />
      </IconButton>
    </Tooltip>
  );
};
