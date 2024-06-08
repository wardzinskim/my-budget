import { IconButton, List, ListItem, Tooltip } from '@mui/material';
import { CategoryDTO, CategoryDTOStatus } from '@repo/api-client';
import { Iconify, Label } from '@repo/minimal-ui';
import { useFetcher } from 'react-router-dom';
import { BudgetCategoryForm } from './budget-category-form';

interface BudgetCategoryListProps {
  items: Array<CategoryDTO>;
}

export const BudgetCategoryList: React.FC<BudgetCategoryListProps> = ({
  items,
}) => {
  const fetcher = useFetcher();

  const archive = (category: CategoryDTO) => {
    fetcher.submit(
      {
        intent: 'archive',
        name: category.name!,
      },
      {
        method: 'POST',
        encType: 'application/json',
      }
    );
  };

  return (
    <List>
      {items?.map((category) => (
        <ListItem
          key={category.name}
          secondaryAction={
            category.status === CategoryDTOStatus.Active && (
              <Tooltip title="Archive">
                <IconButton sx={{ mr: 1 }} onClick={() => archive(category)}>
                  <Iconify icon="eva:archive-outline" />
                </IconButton>
              </Tooltip>
            )
          }
        >
          <Label
            color={
              category.status == CategoryDTOStatus.Active
                ? 'success'
                : 'default'
            }
            variant={'filled'}
          >
            {category.name}
          </Label>
        </ListItem>
      ))}
      <BudgetCategoryForm />
    </List>
  );
};
