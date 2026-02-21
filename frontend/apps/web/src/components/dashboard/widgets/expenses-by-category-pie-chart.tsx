import { Box, Card, CardHeader } from '@mui/material';
import { CategoryValue } from '@repo/api-client';
import { Chart, fCurrency, fPercent, useChart } from '@repo/minimal-ui';

interface ExpensesByCategoryPieChartProps {
  categories: CategoryValue[];
}

export const ExpensesByCategoryPieChart: React.FC<
  ExpensesByCategoryPieChartProps
> = ({ categories }) => {
  const chartOptions = useChart({
    stroke: {
      width: 2,
      colors: ['#fff'],
    },
    plotOptions: {
      pie: {
        expandOnClick: true,
        donut: {
          labels: {
            show: false,
          },
        },
      },
    },
    dataLabels: {
      enabled: true,
      formatter: (val) => fPercent(val),
      style: {
        fontSize: '14px',
      },
      dropShadow: {
        enabled: false,
      },
    },
    labels: categories.map((x) => x.category ?? 'uncategorized'),
    legend: {
      show: true,
      position: 'right',
    },
    tooltip: {
      fillSeriesColor: false,
      y: {
        formatter: (val) => `${fCurrency(val)} PLN`,
      },
    },
    responsive: [
      {
        breakpoint: 480,
        options: {
          legend: {
            show: false,
            position: 'bottom',
          },
        },
      },
    ],
  });

  return (
    <Card>
      <CardHeader title="Expenses distribution by category"></CardHeader>

      <Box sx={{ p: 0, pb: 0 }}>
        <Chart
          dir="ltr"
          type="pie"
          series={categories.map((x) => x.value!)}
          options={chartOptions}
          width="100%"
          height={400}
        />
      </Box>
    </Card>
  );
};
