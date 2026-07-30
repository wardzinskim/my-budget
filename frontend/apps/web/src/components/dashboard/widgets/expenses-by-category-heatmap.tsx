import { Box, Card, CardHeader, useTheme } from '@mui/material';
import { CategoryMonthValue } from '@repo/api-client';
import { Chart, fCurrency, useChart } from '@repo/minimal-ui';

interface ExpensesByCategoryHeatmapProps {
  data: CategoryMonthValue[];
  title?: string;
}

const MONTH_LABELS = [
  'Jan',
  'Feb',
  'Mar',
  'Apr',
  'May',
  'Jun',
  'Jul',
  'Aug',
  'Sep',
  'Oct',
  'Nov',
  'Dec',
];

export const ExpensesByCategoryHeatmap: React.FC<
  ExpensesByCategoryHeatmapProps
> = ({ data, title = 'Expenses by category and month' }) => {
  const theme = useTheme();

  const categories = Array.from(
    new Set(data.map((x) => x.category ?? 'uncategorized'))
  );

  const series = categories.map((category) => ({
    name: category,
    data: MONTH_LABELS.map((label, index) => {
      const month = index + 1;
      const match = data.find(
        (x) => (x.category ?? 'uncategorized') === category && x.month === month
      );
      return { x: label, y: Math.round((match?.value ?? 0) * 100) / 100 };
    }),
  }));

  const chartOptions = useChart({
    chart: {
      toolbar: { show: false },
    },
    dataLabels: {
      enabled: true,
      style: {
        colors: [theme.palette.common.white],
      },
      formatter: (val: number) => (val ? fCurrency(val) : ''),
    },
    plotOptions: {
      heatmap: {
        radius: 2,
        colorScale: {
          ranges: [
            {
              from: 0,
              to: 0,
              color: theme.palette.action.hover,
              name: 'no expense',
            },
          ],
        },
      },
    },
    colors: [theme.palette.primary.main],
    xaxis: {
      type: 'category',
      axisTicks: { show: false },
    },
    tooltip: {
      y: {
        formatter: (val: number) => `${fCurrency(val)} PLN`,
      },
    },
  });

  const height = Math.max(160, categories.length * 16 + 60);

  return (
    <Card>
      <CardHeader title={title} />

      <Box sx={{ p: 3 }}>
        <Chart
          dir="ltr"
          type="heatmap"
          series={series}
          options={chartOptions}
          width="100%"
          height={height}
        />
      </Box>
    </Card>
  );
};
