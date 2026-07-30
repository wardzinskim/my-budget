import Table from '@mui/material/Table';
import TableContainer from '@mui/material/TableContainer';
import { ColumnDefinition, MinimalTableHead } from './table-head';
import { ChangeEvent, useState } from 'react';
import {
  Stack,
  TableBody,
  TableCell,
  TablePagination,
  TableRow,
  Typography,
} from '@mui/material';
import { MinimalTableRow } from './table-row';
import { applyFilter, getComparator } from './utils';
import { Scrollbar } from '../scrollbar';
import { Iconify } from '../iconify';

export interface MinimalTableProps<TItem> {
  columns: Array<ColumnDefinition<TItem>>;
  items: Array<TItem>;
  withSelection: boolean;
}

export function MinimalTable<TItem extends { id?: string }>({
  columns,
  items,
  withSelection,
}: MinimalTableProps<TItem>) {
  const [page, setPage] = useState(0);

  const [order, setOrder] = useState<'asc' | 'desc'>('asc');

  const [selected, setSelected] = useState<Array<string>>([]);

  const [orderBy, setOrderBy] = useState<Extract<keyof TItem, string> | null>(
    null
  );

  //   const [filterName, setFilterName] = useState('');

  const [rowsPerPage, setRowsPerPage] = useState(10);

  const handleSort = (id: Extract<keyof TItem, string> | null) => {
    const isAsc = orderBy === id && order === 'asc';
    if (id !== '') {
      setOrder(isAsc ? 'desc' : 'asc');
      setOrderBy(id);
    }
  };

  const handleSelectAllClick = (checked: boolean) => {
    if (checked) {
      const newSelecteds = items.map((n) => n.id!);
      setSelected(newSelecteds);
      return;
    }
    setSelected([]);
  };

  const handleChangePage = (
    _event: React.MouseEvent<HTMLButtonElement, MouseEvent> | null,
    newPage: number
  ) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (
    event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    setPage(0);
    setRowsPerPage(parseInt(event.target.value, 10));
  };

  const handleClick = (id: string) => {
    const selectedIndex = selected.indexOf(id);
    let newSelected: Array<string> = [];
    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selected, id);
    } else if (selectedIndex === 0) {
      newSelected = newSelected.concat(selected.slice(1));
    } else if (selectedIndex === selected.length - 1) {
      newSelected = newSelected.concat(selected.slice(0, -1));
    } else if (selectedIndex > 0) {
      newSelected = newSelected.concat(
        selected.slice(0, selectedIndex),
        selected.slice(selectedIndex + 1)
      );
    }
    setSelected(newSelected);
  };

  const dataFiltered =
    orderBy === null
      ? items
      : applyFilter(items, getComparator(order, orderBy));

  return (
    <>
      <Scrollbar>
        <TableContainer sx={{ overflow: 'unset' }}>
          <Table stickyHeader sx={{ minWidth: 800 }}>
            <MinimalTableHead<TItem>
              order={order}
              orderBy={orderBy}
              numSelected={selected.length}
              onRequestSort={handleSort}
              headers={columns}
              onSelectAllClick={handleSelectAllClick}
              rowCount={items.length}
              withSelection={withSelection}
            />
            <TableBody>
              {dataFiltered
                .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                .map((row) => (
                  <MinimalTableRow<TItem>
                    key={row.id}
                    columnsDefinition={columns}
                    item={row}
                    selected={selected.indexOf(row.id!) !== -1}
                    handleClick={handleClick}
                    withSelection={withSelection}
                  />
                ))}

              {items.length === 0 && (
                <TableRow>
                  <TableCell colSpan={columns.length + (withSelection ? 1 : 0)}>
                    <Stack
                      alignItems="center"
                      justifyContent="center"
                      spacing={1}
                      sx={{ py: 8, color: 'text.secondary' }}
                    >
                      <Iconify
                        icon="solar:inbox-line-linear"
                        width={40}
                        sx={{ opacity: 0.6 }}
                      />
                      <Typography variant="body2">No data found</Typography>
                    </Stack>
                  </TableCell>
                </TableRow>
              )}
            </TableBody>
          </Table>
        </TableContainer>
      </Scrollbar>
      <TablePagination
        page={page}
        component="div"
        count={items.length}
        rowsPerPage={rowsPerPage}
        onPageChange={handleChangePage}
        rowsPerPageOptions={[10, 20, 50]}
        onRowsPerPageChange={handleChangeRowsPerPage}
      />
    </>
  );
}
