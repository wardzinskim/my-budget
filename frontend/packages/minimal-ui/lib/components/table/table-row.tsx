/* eslint-disable  @typescript-eslint/no-explicit-any */
/* eslint-disable  @typescript-eslint/no-unsafe-member-access */
import { Checkbox, TableCell, TableRow } from '@mui/material';
import { ColumnDefinition } from './table-head';

interface MinimalTableRowProps<TItem extends { id?: string }> {
  columnsDefinition: Array<ColumnDefinition<TItem>>;
  item: TItem;
  selected: boolean;
  withSelection: boolean;
  handleClick: (id: string) => void;
}

export function MinimalTableRow<TItem extends { id?: string }>({
  columnsDefinition,
  item,
  selected,
  withSelection,
  handleClick,
}: MinimalTableRowProps<TItem>) {
  return (
    <TableRow hover tabIndex={-1} role="checkbox">
      {withSelection && (
        <TableCell padding="checkbox">
          <Checkbox
            disableRipple
            checked={selected}
            onChange={() => handleClick(item.id!)}
          />
        </TableCell>
      )}

      {columnsDefinition.map((column, index) => (
        <TableCell align={column.align || 'left'} key={`${item.id}_${index}`}>
          {column.render ? column.render(item) : (item as any)[column.id]}
        </TableCell>
      ))}
    </TableRow>
  );
}
