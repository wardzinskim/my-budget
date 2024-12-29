import { IconButton, List, ListItem, Tooltip } from '@mui/material';
import { ShareDTO } from '@repo/api-client';
import { Iconify } from '@repo/minimal-ui';
import { useFetcher } from 'react-router-dom';
import { BudgetShareForm } from './budget-share-form';

interface BudgetShareListProps {
  items: Array<ShareDTO>;
}

export const BudgetShareList: React.FC<BudgetShareListProps> = ({ items }) => {
  const fetcher = useFetcher();

  const unshare = async (share: ShareDTO) => {
    await fetcher.submit(
      {
        intent: 'share',
        login: share.userId!,
      },
      {
        method: 'DELETE',
        encType: 'application/json',
      }
    );
  };

  return (
    <List>
      {items?.map((sharing) => (
        <ListItem
          key={sharing.userId}
          secondaryAction={
            <Tooltip title="Unshare">
              <IconButton sx={{ mr: 1 }} onClick={() => unshare(sharing)}>
                <Iconify icon="material-symbols:delete" />
              </IconButton>
            </Tooltip>
          }
        >
          {sharing.login}
        </ListItem>
      ))}
      <BudgetShareForm />
    </List>
  );
};
