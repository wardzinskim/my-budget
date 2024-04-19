import Box from '@mui/material/Box';
import TableRow from '@mui/material/TableRow';
import Checkbox from '@mui/material/Checkbox';
import TableHead from '@mui/material/TableHead';
import TableCell from '@mui/material/TableCell';
import TableSortLabel from '@mui/material/TableSortLabel';

import { visuallyHidden } from './utils';

// ----------------------------------------------------------------------

export interface ColumnDefinition<TItem> {
  id?: Extract<keyof TItem, string>;
  label: string;
  align?: 'inherit' | 'left' | 'center' | 'right' | 'justify';
  render?: (item: TItem) => JSX.Element;
  sortable: boolean;
}

export interface MinimalTableHeadProps<TItem> {
  order: 'asc' | 'desc';
  orderBy: string | null;
  rowCount: number;
  headers: Array<ColumnDefinition<TItem>>;
  numSelected: number;
  withSelection: boolean;
  onRequestSort: (id: Extract<keyof TItem, string> | null) => void;
  onSelectAllClick: (checked: boolean) => void;
}

export function MinimalTableHead<TItem>(props: MinimalTableHeadProps<TItem>) {
  const onSort = (property: Extract<keyof TItem, string> | null) => () => {
    props.onRequestSort(property);
  };

  return (
    <TableHead>
      <TableRow>
        {props.withSelection && (
          <TableCell padding="checkbox">
            <Checkbox
              indeterminate={
                props.numSelected > 0 && props.numSelected < props.rowCount
              }
              checked={
                props.rowCount > 0 && props.numSelected === props.rowCount
              }
              onChange={(e) => props.onSelectAllClick(e.target.checked)}
            />
          </TableCell>
        )}

        {props.headers.map((headCell) => (
          <TableCell
            key={headCell.id}
            align={headCell.align || 'left'}
            sortDirection={props.orderBy === headCell.id ? props.order : false}
            // sx={{ width: headCell.width, minWidth: headCell.minWidth }}
          >
            {headCell.sortable ? (
              <TableSortLabel
                hideSortIcon
                active={props.orderBy === headCell.id}
                direction={props.orderBy === headCell.id ? props.order : 'asc'}
                onClick={onSort(headCell.id!)}
              >
                {headCell.label}
                {props.orderBy === headCell.id ? (
                  <Box sx={{ ...visuallyHidden }}>
                    {props.order === 'desc'
                      ? 'sorted descending'
                      : 'sorted ascending'}
                  </Box>
                ) : null}
              </TableSortLabel>
            ) : (
              headCell.label
            )}
          </TableCell>
        ))}
      </TableRow>
    </TableHead>
  );
}
