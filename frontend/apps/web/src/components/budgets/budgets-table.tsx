import { IconButton } from '@mui/material';
import { BudgetDTOStatus, BudgetListItemDTO } from '@repo/api-client';
import {
  ColumnDefinition,
  Iconify,
  Label,
  MinimalTable,
  RouterLink,
  fToNow,
} from '@repo/minimal-ui';

interface BudgetsTableProps {
  budgets: Array<BudgetListItemDTO>;
}

const BudgetTableColumns: Array<ColumnDefinition<BudgetListItemDTO>> = [
  {
    id: 'name',
    label: 'Name',
    align: 'left',
  },
  {
    id: 'description',
    label: 'Description',
    align: 'left',
  },
  {
    id: 'creationDate',
    label: 'Creation Date',
    align: 'left',
    render: (item: BudgetListItemDTO) => <>{fToNow(item.creationDate!)}</>,
  },
  {
    id: 'status',
    label: 'Status',
    align: 'center',
    render: (item: BudgetListItemDTO) => (
      <Label
        color={item.status === BudgetDTOStatus.Open ? 'success' : 'default'}
        variant="filled"
      >
        {item.status}
      </Label>
    ),
  },
  {
    label: '',
    align: 'right',
    render: (item: BudgetListItemDTO) => (
      <IconButton component={RouterLink} href={`/budgets/${item.id}`}>
        <Iconify icon="carbon:view-filled" />
      </IconButton>
    ),
  },
];

export const BudgetsTable: React.FC<BudgetsTableProps> = ({ budgets }) => {
  return (
    <MinimalTable<BudgetListItemDTO>
      columns={BudgetTableColumns}
      items={budgets}
      withSelection={false}
    ></MinimalTable>
  );
};
