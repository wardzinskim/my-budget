import { BudgetListItemDTO } from '@repo/api-client';
import { useFetcher } from 'react-router-dom';
import { useUserContext } from '../../hooks/user-context';
import { FormControl, InputLabel, MenuItem, Select } from '@mui/material';
import { useEffect } from 'react';

export const BudgetContextPicker: React.FC = () => {
  const fetcher = useFetcher();
  const [userContext, setUserContext] = useUserContext();

  useEffect(() => {
    if (fetcher.state === 'idle' && !fetcher.data) {
      fetcher.load('/budgets');
    }
  }, [fetcher]);

  return (
    <FormControl fullWidth={true}>
      <InputLabel>Budget</InputLabel>

      <Select
        value={userContext.budgetId}
        label="Budget"
        onChange={(x) =>
          setUserContext({ ...userContext, budgetId: x.target.value })
        }
      >
        {(fetcher.data as Array<BudgetListItemDTO>)?.map((budget) => (
          <MenuItem key={budget.id} value={budget.id}>
            {budget.name}
          </MenuItem>
        ))}
      </Select>
    </FormControl>
  );
};
