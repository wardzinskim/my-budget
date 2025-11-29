import {
  Card,
  CardHeader,
  Divider,
  List,
  ListItem,
  ListItemText,
} from '@mui/material';
import { CategoryValue, TransferDTOType } from '@repo/api-client';
import { fCurrency } from '@repo/minimal-ui';

interface TransfersByCategoryListProps {
  categories: CategoryValue[];
  transferType?: TransferDTOType;
  title?: string;
}

export const TransfersByCategoryList: React.FC<
  TransfersByCategoryListProps
> = ({ categories, transferType, title }) => {
  return (
    <Card>
      <CardHeader
        title={!title ? <>Yearly {transferType}s grouped by category</> : title}
      />

      <List>
        {categories.map((category) => (
          <>
            <ListItem
              key={category.category}
              secondaryAction={
                <div style={{ fontWeight: 'bold' }}>
                  {fCurrency(category.value)} PLN
                </div>
              }
            >
              <ListItemText primary={category.category} />
            </ListItem>
            <Divider component="li" variant="middle" />
          </>
        ))}
      </List>
    </Card>
  );
};
