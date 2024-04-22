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
  useAlert,
} from '@repo/minimal-ui';
import { useMemo } from 'react';
import { useSubmit } from 'react-router-dom';

interface TransfersTableProps {
  transfers: Array<TransferDTO>;
}

const TransferTableColumnsBuilder: (
  deleteAction: (transferId: string) => void
) => Array<ColumnDefinition<TransferDTO>> = (deleteAction) => [
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
        <IconButton color="error" onClick={() => deleteAction(item.id!)}>
          <Iconify icon="solar:trash-bin-minimalistic-bold" />
        </IconButton>
      </>
    ),
  },
];

export const TransfersTable: React.FC<TransfersTableProps> = ({
  transfers,
}) => {
  const alert = useAlert();
  const submit = useSubmit();
  const transferTableColumns = useMemo(() => {
    const deleteTransfer = (transferId: string) => {
      alert.show(
        'Delete',
        'Are you sure want to delete?',
        (result: boolean) => {
          if (!result) return;
          submit(
            {
              intent: 'delete',
              transferId,
            },
            {
              method: 'POST',
              encType: 'application/json',
            }
          );
        },
        {
          acceptButtonLabel: 'Delete',
          acceptButton: {
            color: 'error',
          },
        }
      );
    };

    return TransferTableColumnsBuilder(deleteTransfer);
  }, [submit, alert]);

  return (
    <>
      <MinimalTable<TransferDTO>
        columns={transferTableColumns}
        items={transfers}
        withSelection={false}
      ></MinimalTable>
    </>
  );
};
