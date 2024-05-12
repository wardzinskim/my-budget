import { Box, Card, CardHeader } from '@mui/material';
import { Chart, useChart } from '@repo/minimal-ui';

interface ExpensesToIncomesRatioProps {
  incomes: number;
  expenses: number;
}

export const ExpensesToIncomesRatio: React.FC<ExpensesToIncomesRatioProps> = ({
  expenses,
  incomes,
}) => {
  const chartOptions = useChart({
    chart: {
      type: 'radialBar',
    },
    plotOptions: {
      radialBar: {
        startAngle: -135,
        endAngle: 225,
        hollow: {
          margin: 0,
          size: '70%',
          background: '#fff',
          image: undefined,
          imageOffsetX: 0,
          imageOffsetY: 0,
          position: 'front',
          dropShadow: {
            enabled: true,
            top: 3,
            left: 0,
            blur: 4,
            opacity: 0.24,
          },
        },
        track: {
          background: '#fff',
          strokeWidth: '67%',
          margin: 0, // margin is in pixels
          dropShadow: {
            enabled: true,
            top: -3,
            left: 0,
            blur: 4,
            opacity: 0.35,
          },
        },
        dataLabels: {
          show: true,
          name: {
            show: false,
          },
          value: {
            color: '#111',
            fontSize: '36px',
            show: true,
          },
        },
      },
    },
    fill: {
      type: 'gradient',
      gradient: {
        shade: 'dark',
        type: 'horizontal',
        shadeIntensity: 0.5,
        gradientToColors: ['#ABE5A1'],
        inverseColors: true,
        opacityFrom: 1,
        opacityTo: 1,
        stops: [0, 100],
      },
    },
    legend: {
      show: false,
    },
    stroke: {
      lineCap: 'round',
    },
    labels: ['ratio'],
  });

  return (
    <Card>
      <CardHeader title="Expenses to incomes ratio"></CardHeader>

      <Box sx={{ p: 1, pb: 1 }}>
        <Chart
          dir="ltr"
          type="radialBar"
          series={[Math.round((expenses / incomes) * 100)]}
          options={chartOptions}
          width="100%"
          height={300}
        />
      </Box>
    </Card>
  );
};
