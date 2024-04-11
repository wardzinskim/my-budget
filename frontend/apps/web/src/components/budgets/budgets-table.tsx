import {
  BudgetDTO,
  BudgetDTOStatus,
  CategoryDTOStatus,
} from '@repo/api-client';
import { ColumnDefinition, Label, MinimalTable } from '@repo/minimal-ui';

interface BudgetsTableProps {
  budgets: Array<BudgetDTO>;
}

const BudgetTableColumns: Array<ColumnDefinition<BudgetDTO>> = [
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
    id: 'categories',
    label: 'Defined Categories',
    align: 'left',
    render: (item: BudgetDTO) => (
      <>
        {item.categories?.map((x) => (
          <Label
            color={
              x.status == CategoryDTOStatus.Archived ? 'default' : 'success'
            }
            variant="outlined"
            key={x.name}
          >
            {x.name}
          </Label>
        ))}
      </>
    ),
  },
  {
    id: 'status',
    label: 'Status',
    align: 'center',
    render: (item: BudgetDTO) => (
      <Label
        color={item.status === BudgetDTOStatus.Open ? 'success' : 'default'}
        variant="filled"
      >
        {item.status}
      </Label>
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
