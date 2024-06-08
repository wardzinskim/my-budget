import { IconButton, Tooltip } from '@mui/material';
import { BudgetDTO, BudgetDTOStatus } from '@repo/api-client';
import {
  ColumnDefinition,
  Iconify,
  Label,
  MinimalTable,
  RouterLink,
  fToNow,
} from '@repo/minimal-ui';

interface BudgetsTableProps {
  budgets: Array<BudgetDTO>;
}

const BudgetTableColumns: Array<ColumnDefinition<BudgetDTO>> = [
  {
    id: 'name',
    label: 'Name',
    align: 'left',
    sortable: true,
  },
  {
    id: 'description',
    label: 'Description',
    align: 'left',
    sortable: false,
  },
  {
    id: 'creationDate',
    label: 'Creation Date',
    align: 'left',
    sortable: true,
    render: (item: BudgetDTO) => <>{fToNow(item.creationDate!)}</>,
  },
  {
    id: 'status',
    label: 'Status',
    align: 'center',
    sortable: true,
    render: (item: BudgetDTO) => (
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
    sortable: false,
    render: (item: BudgetDTO) => (
      <Tooltip title="Details" placement="left">
        <IconButton component={RouterLink} href={`/budgets/${item.id}`}>
          <Iconify icon="carbon:view-filled" />
        </IconButton>
      </Tooltip>
    ),
  },
];

export const BudgetsTable: React.FC<BudgetsTableProps> = ({ budgets }) => {
  return (
    <MinimalTable<BudgetDTO>
      columns={BudgetTableColumns}
      items={budgets}
      withSelection={false}
    ></MinimalTable>
  );
};
