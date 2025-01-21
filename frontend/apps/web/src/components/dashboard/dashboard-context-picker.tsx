import {
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  Stack,
} from '@mui/material';
import { useDashboardContext } from './hooks/dashboard-context';
import { useLocalizationContext } from '@mui/x-date-pickers/internals';

export const DashboardContextPicker: React.FC = () => {
  const [dashboardContext, setDashboardContext] = useDashboardContext();
  const localizationContext = useLocalizationContext();

  return (
    dashboardContext && (
      <Stack direction="row-reverse" spacing={1}>
        <FormControl size="small">
          <InputLabel>Year</InputLabel>
          <Select
            value={dashboardContext.year}
            label="Year"
            onChange={(x) =>
              setDashboardContext({
                ...dashboardContext,
                year: x.target.value as number,
              })
            }
          >
            <MenuItem value={2024} key={2024}>
              2024
            </MenuItem>
            <MenuItem value={2025} key={2025}>
              2025
            </MenuItem>
            <MenuItem value={2026} key={2026}>
              2026
            </MenuItem>
          </Select>
        </FormControl>

        <FormControl size="small">
          <InputLabel>Month</InputLabel>
          <Select
            value={dashboardContext.month}
            label="Month"
            onChange={(x) =>
              setDashboardContext({
                ...dashboardContext,
                month: x.target.value as number,
              })
            }
          >
            {[0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11].map((x) => (
              <MenuItem value={x + 1} key={x}>
                {
                  // eslint-disable-next-line @typescript-eslint/no-unsafe-member-access, @typescript-eslint/no-unsafe-call
                  localizationContext.utils.locale.localize.month(x)
                }
              </MenuItem>
            ))}
          </Select>
        </FormControl>
      </Stack>
    )
  );
};
