import {
  Button,
  Card,
  CardActions,
  CardContent,
  Container,
  Stack,
  Typography,
} from '@mui/material';
import { RouterLink } from '@repo/minimal-ui';

interface BudgetNewViewProps {}

export const BudgetNewView: React.FC<BudgetNewViewProps> = () => {
  return (
    <>
      <Container>
        <Stack
          direction="row"
          alignItems="center"
          justifyContent="space-between"
          mb={5}
        >
          <Typography variant="h4">Create new Budget</Typography>
        </Stack>

        <Card>
          <CardContent></CardContent>
          <CardActions>
            <Button
              component={RouterLink}
              href="/budgets"
              variant="outlined"
              color="inherit"
            >
              Cancel
            </Button>
            <Button variant="contained" color="inherit">
              Create
            </Button>
          </CardActions>
        </Card>
      </Container>
    </>
  );
};
