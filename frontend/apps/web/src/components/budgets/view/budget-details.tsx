import {
  Box,
  Card,
  Container,
  Stack,
  Tab,
  Tabs,
  Typography,
} from '@mui/material';
import { BudgetDTO } from '@repo/api-client';
import { Iconify } from '@repo/minimal-ui';
import { useLoaderData } from 'react-router-dom';

export const BudgetDetails: React.FC = () => {
  const budget: BudgetDTO = useLoaderData() as BudgetDTO;
  return (
    <>
      <Container>
        <Stack
          direction="column"
          alignItems="left"
          justifyContent="space-between"
          mb={5}
        >
          <Typography variant="h4">Budget: {budget.name}</Typography>
          <Typography variant="body2">{budget.description}</Typography>
        </Stack>

        <Tabs>
          <Tab
            icon={<Iconify icon="carbon:view-filled" />}
            iconPosition="start"
            label="Categories"
            sx={{
              animation: 'off',
              minHeight: 44,
              borderRadius: 0.75,
              typography: 'body2',
              color: 'text.secondary',
              textTransform: 'capitalize',
              fontWeight: 'fontWeightRegular',
            }}
          ></Tab>
        </Tabs>

        <Card></Card>
      </Container>
    </>
  );
};
