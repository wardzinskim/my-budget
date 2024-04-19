import { IconButton, Typography } from '@mui/material';
import {
  BudgetListItemDTO,
  TransferDTO,
  TransferDTOType,
} from '@repo/api-client';
import {
  ColumnDefinition,
  Iconify,
  MinimalTable,
  RouterLink,
  Label,
  fToNow,
} from '@repo/minimal-ui';

interface TransfersTableProps {
  transfers: Array<TransferDTO>;
}

const TransferTableColumns: Array<ColumnDefinition<TransferDTO>> = [
  {
    id: 'value',
    label: 'Amount',
    align: 'left',
    sortable: false,
    render: (item: TransferDTO) => (
      <Typography
        variant="caption"
        sx={{
          fontWeight:
            item.type == TransferDTOType.Income ? 'fontWeightMedium' : null,
          fontSize: 16,
          color: item.type == TransferDTOType.Income ? 'success.main' : null,
        }}
      >
        {item.type == TransferDTOType.Income ? '+' : '-'}
        {item.value?.toFixed(2)} {item.currency}
      </Typography>
    ),
  },
  {
    id: 'name',
    label: 'Description',
    align: 'left',
    sortable: false,
  },
  {
    id: 'transferDate',
    label: 'Date',
    align: 'left',
    sortable: false,
    render: (item: TransferDTO) => <>{fToNow(item.transferDate!)}</>,
  },
  {
    id: 'category',
    label: 'Category',
    align: 'center',
    sortable: false,
    render: (item: TransferDTO) =>
      item.category ? (
        <Label color="primary" variant="filled">
          {item.category}
        </Label>
      ) : (
        <></>
      ),
  },
  {
    label: '',
    align: 'right',
    sortable: false,
    render: (item: BudgetListItemDTO) => (
      <>
        <IconButton component={RouterLink} href={`/transfers/${item.id}/edit`}>
          <Iconify icon="solar:pen-bold" />
        </IconButton>
        <IconButton color="error">
          <Iconify icon="solar:trash-bin-minimalistic-bold" />
        </IconButton>
      </>
    ),
  },
];

export const TransfersTable: React.FC<TransfersTableProps> = ({
  transfers,
}) => {
  return (
    <MinimalTable<TransferDTO>
      columns={TransferTableColumns}
      items={transfers}
      withSelection={false}
    ></MinimalTable>
  );
};
