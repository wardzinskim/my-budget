import { Button, Card, Container, Stack, Typography } from '@mui/material';
import { Helmet } from 'react-helmet-async';
import { MinimalTable } from '../../lib/components/table';
import { ColumnDefinition } from '../../lib/components/table/table-head';
import { Iconify } from '../../lib/components';
import { NavigationBar } from '../../lib/components/navigation-bar/navigation-bar';

// ----------------------------------------------------------------------

const columns: ColumnDefinition<User>[] = [
  { id: 'name', label: 'Name' },
  { id: 'company', label: 'Company' },
  { id: 'role', label: 'Role' },
  {
    id: 'isVerified',
    label: 'Verified',
    align: 'center',
    render: (item: User) => <>{item.isVerified ? 'TAK' : 'NIE'}</>,
  },
  { id: 'status', label: 'Status' },
  { label: '' },
];

interface User {
  id: string;
  name: string;
  company: string;
  role: string;
  isVerified: boolean;
  status: string;
}

const users: User[] = [
  {
    id: '1',
    company: '1aaa',
    isVerified: true,
    name: '1aaa',
    role: '1asd',
    status: '1asd',
  },
  {
    id: '2',
    company: '2aaa',
    isVerified: true,
    name: '2aaa',
    role: '2asd',
    status: '2asd',
  },
  {
    id: '3',
    company: '3aaa',
    isVerified: true,
    name: '3aaa',
    role: '3asd',
    status: '3asd',
  },
  {
    id: '4',
    company: '1aaa',
    isVerified: true,
    name: '1aaa',
    role: '1asd',
    status: '1asd',
  },
  {
    id: '5',
    company: '2aaa',
    isVerified: true,
    name: '2aaa',
    role: '2asd',
    status: '2asd',
  },
  {
    id: '6',
    company: '3aaa',
    isVerified: true,
    name: '3aaa',
    role: '3asd',
    status: '3asd',
  },
  {
    id: '7',
    company: '1aaa',
    isVerified: true,
    name: '1aaa',
    role: '1asd',
    status: '1asd',
  },
  {
    id: '8',
    company: '2aaa',
    isVerified: true,
    name: '2aaa',
    role: '2asd',
    status: '2asd',
  },
  {
    id: '9',
    company: '3aaa',
    isVerified: true,
    name: '3aaa',
    role: '3asd',
    status: '3asd',
  },
];

export default function AppPage() {
  return (
    <>
      <Helmet>
        <title> Dashboard | Minimal UI </title>
      </Helmet>

      <Container>
        <Stack
          direction="row"
          alignItems="center"
          justifyContent="space-between"
          mb={5}
        >
          <Typography variant="h4">Users</Typography>

          <Button
            variant="contained"
            color="inherit"
            startIcon={<Iconify icon="eva:plus-fill" />}
          >
            New User
          </Button>
        </Stack>

        <NavigationBar
          type="horizontal"
          items={[
            {
              path: 'asd',
              title: 'asd',
            },
          ]}
        ></NavigationBar>

        <Card>
          <MinimalTable<User>
            columns={columns}
            items={users}
            withSelection={false}
          ></MinimalTable>
        </Card>
      </Container>
    </>
  );
}
