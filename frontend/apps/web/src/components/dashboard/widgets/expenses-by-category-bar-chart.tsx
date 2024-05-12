import { Box, Card, CardHeader } from '@mui/material';
import { CategoryValue } from '@repo/api-client';
import { fCurrency } from '@repo/minimal-ui';
import { Chart, useChart } from '@repo/minimal-ui';
import _ from 'lodash';

interface ExpensesByCategoryBarChartProps {
  categories: CategoryValue[];
}

export const ExpensesByCategoryBarChart: React.FC<
  ExpensesByCategoryBarChartProps
> = ({ categories }) => {
  const chartOptions = useChart({
    plotOptions: {
      bar: {
        columnWidth: '35%',
        distributed: true,
        dataLabels: {
          position: 'top',
        },
        borderRadius: 10,
      },
    },
    dataLabels: {
      enabled: true,
      formatter: (val) => `${fCurrency(val)} PLN`,
      offsetY: -20,
      style: {
        fontSize: '12px',
        colors: ['#304758'],
      },
    },

    legend: {
      show: false,
    },
    xaxis: {
      categories: categories.map((x) => x.category),
    },
    yaxis: {
      title: {
        text: 'Currency',
      },
      max: Math.round(
        (_.maxBy(categories, (x) => x.value!)?.value ?? 0) * 1.05
      ),
      min: 0,
    },
    tooltip: {
      y: {
        formatter: (val) => `${fCurrency(val)} PLN`,
      },
    },
  });

  return (
    <Card>
      <CardHeader title="Expenses grouped into categories"></CardHeader>

      <Box sx={{ p: 3, pb: 1 }}>
        <Chart
          dir="ltr"
          type="bar"
          series={[
            {
              data: categories.map((x) => x.value!),
              name: 'Total Value',
            },
          ]}
          options={chartOptions}
          width="100%"
          height={400}
        />
      </Box>
    </Card>
  );
};
