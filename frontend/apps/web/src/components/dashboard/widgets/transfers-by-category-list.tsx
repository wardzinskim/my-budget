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
  transferType: TransferDTOType;
}

export const TransfersByCategoryList: React.FC<
  TransfersByCategoryListProps
> = ({ categories, transferType }) => {
  return (
    <Card>
      <CardHeader title={<>Yearly {transferType}s grouped by category</>} />

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
