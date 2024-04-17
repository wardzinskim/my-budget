import { BudgetListItemDTO } from '@repo/api-client';
import { useFetcher } from 'react-router-dom';
import { useUserContext } from '../../hooks/user-context';
import { FormControl, InputLabel, MenuItem, Select } from '@mui/material';
import { useEffect } from 'react';

export const BudgetContextPicker: React.FC = () => {
  const fetcher = useFetcher();
  const [userContext, setUserContext] = useUserContext();

  const budgets = fetcher.data as Array<BudgetListItemDTO>;

  useEffect(() => {
    if (fetcher.state === 'idle' && !fetcher.data) {
      fetcher.load('/budgets');
    }
  }, [fetcher]);

  const updateBudgetInContext = (id: string) => {
    const selectedBudget = budgets.find((x) => x.id == id);

    setUserContext({
      ...userContext,
      budget: selectedBudget,
    });
  };

  return (
    <FormControl fullWidth={true}>
      <InputLabel>Budget</InputLabel>

      <Select
        value={userContext.budget?.id}
        label="Budget"
        onChange={(x) => updateBudgetInContext(x.target.value)}
      >
        {budgets?.map((budget) => (
          <MenuItem key={budget.id} value={budget.id}>
            {budget.name}
          </MenuItem>
        ))}
      </Select>
    </FormControl>
  );
};
