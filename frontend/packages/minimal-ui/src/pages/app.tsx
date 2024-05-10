import { Box, Button, Card, Container, Stack, Typography } from '@mui/material';
import { Helmet } from 'react-helmet-async';
import { MinimalTable } from '../../lib/components/table';
import { ColumnDefinition } from '../../lib/components/table/table-head';
import { Iconify } from '../../lib/components';
import { NavigationBar } from '../../lib/components/navigation-bar/navigation-bar';
import { Chart, useChart } from '../../lib/components/chart';

// ----------------------------------------------------------------------

const columns: ColumnDefinition<User>[] = [
  { id: 'name', label: 'Name', sortable: true },
  { id: 'company', label: 'Company', sortable: true },
  { id: 'role', label: 'Role', sortable: true },
  {
    id: 'isVerified',
    label: 'Verified',
    align: 'center',
    render: (item: User) => <>{item.isVerified ? 'TAK' : 'NIE'}</>,
    sortable: true,
  },
  { id: 'status', label: 'Status', sortable: true },
  { label: '', sortable: true },
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
  const series = [
    {
      name: 'Team A',
      type: 'column',
      fill: 'solid',
      data: [23, 11, 22, 27, 13, 22, 37, 21, 44, 22, 30],
    },
    {
      name: 'Team B',
      type: 'area',
      fill: 'gradient',
      data: [44, 55, 41, 67, 22, 43, 21, 41, 56, 27, 43],
    },
  ];

  const chartOptions = useChart({
    // colors,
    plotOptions: {
      bar: {
        columnWidth: '16%',
      },
    },
    fill: {
      type: series.map((i) => i.fill),
    },
    labels: [
      '01/01/2003',
      '02/01/2003',
      '03/01/2003',
      '04/01/2003',
      '05/01/2003',
      '06/01/2003',
      '07/01/2003',
      '08/01/2003',
      '09/01/2003',
      '10/01/2003',
      '11/01/2003',
    ],
    xaxis: {
      type: 'datetime',
    },
    tooltip: {
      shared: true,
      intersect: false,
      y: {
        formatter: (value) => {
          if (typeof value !== 'undefined') {
            return `${value.toFixed(0)} visits`;
          }
          return value;
        },
      },
    },
    // ...options,
  });

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

          {/* <AlertDialog
            open={true}
            handleClose={function (result: boolean): void {}}
            title="Delete"
            content={'Are you sure want to delete?'}
            buttonProps={{
              acceptButton: {
                color: 'error',
              },
              acceptButtonLabel: 'Delete',
            }}
          ></AlertDialog> */}

          <Box sx={{ p: 3, pb: 1 }}>
            <Chart
              dir="ltr"
              type="line"
              series={series}
              options={chartOptions}
              width="100%"
              height={364}
            />
          </Box>
        </Card>
      </Container>
    </>
  );
}
