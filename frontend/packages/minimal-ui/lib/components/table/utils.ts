/* eslint-disable  @typescript-eslint/no-explicit-any */
/* eslint-disable  @typescript-eslint/no-unsafe-member-access */
import { SxProps, Theme } from '@mui/material';

export const visuallyHidden: SxProps<Theme> = {
  border: 0,
  margin: -1,
  padding: 0,
  width: '1px',
  height: '1px',
  overflow: 'hidden',
  position: 'absolute',
  whiteSpace: 'nowrap',
  clip: 'rect(0 0 0 0)',
};

// export function emptyRows(page, rowsPerPage, arrayLength) {
//   return page ? Math.max(0, (1 + page) * rowsPerPage - arrayLength) : 0;
// }

function descendingComparator(a: any, b: any, orderBy: string): number {
  if (a[orderBy] === null) {
    return 1;
  }
  if (b[orderBy] === null) {
    return -1;
  }
  if (b[orderBy] < a[orderBy]) {
    return -1;
  }
  if (b[orderBy] > a[orderBy]) {
    return 1;
  }
  return 0;
}

export function getComparator(order: 'asc' | 'desc', orderBy: string) {
  return order === 'desc'
    ? (a: any, b: any) => descendingComparator(a, b, orderBy)
    : (a: any, b: any) => -descendingComparator(a, b, orderBy);
}

export function applyFilter<TItem>(
  inputData: Array<TItem>,
  comparator: (a: any, b: any) => number
  // filterName?: string
) {
  const stabilizedThis = inputData.map((el, index) => [el, index]);

  stabilizedThis.sort((a, b) => {
    const order = comparator(a[0], b[0]);
    if (order !== 0) return order;
    return (a[1] as number) - (b[1] as number);
  });

  inputData = stabilizedThis.map((el) => el[0] as TItem);

  // if (filterName) {
  //   inputData = inputData.filter(
  //     (user) => user.name.toLowerCase().indexOf(filterName.toLowerCase()) !== -1
  //   );
  // }

  return inputData;
}
