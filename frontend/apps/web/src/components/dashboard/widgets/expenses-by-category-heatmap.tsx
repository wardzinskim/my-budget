import { Box, Card, CardHeader, useMediaQuery, useTheme } from '@mui/material';
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

const BUCKET_COUNT = 8;
// Reserve 0 exclusively for "no expense" cells; real values are normalized
// per-row into (NORMALIZED_MIN, NORMALIZED_MAX].
const NORMALIZED_MIN = 1;
const NORMALIZED_MAX = 100;

interface ColorScaleRange {
  from: number;
  to: number;
  color: string;
  foreColor?: string;
  name?: string;
}

// Parses a '#rgb' or '#rrggbb' hex color into its RGB components.
const hexToRgb = (hex: string): [number, number, number] => {
  const normalized = hex.replace('#', '');
  const expanded =
    normalized.length === 3
      ? normalized
          .split('')
          .map((c) => c + c)
          .join('')
      : normalized;
  const value = parseInt(expanded, 16);
  return [(value >> 16) & 255, (value >> 8) & 255, value & 255];
};

// Blends two hex colors into a solid rgb() color, so the result stays
// legible regardless of what sits behind it (unlike a semi-transparent fill).
const mixColors = (from: string, to: string, ratio: number): string => {
  const [r1, g1, b1] = hexToRgb(from);
  const [r2, g2, b2] = hexToRgb(to);
  const r = Math.round(r1 + (r2 - r1) * ratio);
  const g = Math.round(g1 + (g2 - g1) * ratio);
  const b = Math.round(b1 + (b2 - b1) * ratio);
  return `rgb(${r}, ${g}, ${b})`;
};

// Fixed heatmap palette, intentionally independent of light/dark theme mode
// so the color scale always looks and means the same thing.
const EMPTY_COLOR = '#DFE3E8';
const BASE_COLOR = '#F9FAFB';
const TARGET_COLOR = '#6366F1';
const LIGHT_TEXT = '#FFFFFF';
const DARK_TEXT = '#212B36';

const buildColorScaleRanges = (): { ranges: ColorScaleRange[] } => {
  const noExpenseRange = {
    from: 0,
    to: 0,
    color: EMPTY_COLOR,
    name: 'no expense',
  };

  const ranges: ColorScaleRange[] = [noExpenseRange];
  for (let i = 0; i < BUCKET_COUNT; i++) {
    const from =
      NORMALIZED_MIN + ((NORMALIZED_MAX - NORMALIZED_MIN) * i) / BUCKET_COUNT;
    const to =
      i === BUCKET_COUNT - 1
        ? NORMALIZED_MAX
        : NORMALIZED_MIN +
          ((NORMALIZED_MAX - NORMALIZED_MIN) * (i + 1)) / BUCKET_COUNT;
    const intensity = i / (BUCKET_COUNT - 1);

    ranges.push({
      from,
      to,
      color: mixColors(BASE_COLOR, TARGET_COLOR, intensity),
      foreColor: intensity < 0.55 ? DARK_TEXT : LIGHT_TEXT,
      name: `bucket-${i}`,
    });
  }

  return { ranges };
};

// The color scale is fixed (percentage buckets), so it only needs to be
// built once.
const HEATMAP_COLOR_SCALE = buildColorScaleRanges();

export const ExpensesByCategoryHeatmap: React.FC<
  ExpensesByCategoryHeatmapProps
> = ({ data, title = 'Expenses by category and month' }) => {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('sm'));

  const categories = Array.from(
    new Set(data.map((x) => x.category ?? 'uncategorized'))
  );

  // Raw expense values per category/month, kept aside for labels & tooltips
  // since the series itself carries a row-normalized value used for color.
  const rawValues: number[][] = categories.map((category) =>
    MONTH_LABELS.map((_, index) => {
      const month = index + 1;
      const match = data.find(
        (x) => (x.category ?? 'uncategorized') === category && x.month === month
      );
      return Math.round((match?.value ?? 0) * 100) / 100;
    })
  );

  // Color intensity is normalized per row (category), because each category
  // can have a very different min/max — a single global scale would make
  // small variations within a low-value category invisible.
  const series = categories.map((category, categoryIndex) => {
    const rowValues = rawValues[categoryIndex];
    const positiveValues = rowValues.filter((v) => v > 0);
    const rowMin = positiveValues.length ? Math.min(...positiveValues) : 0;
    const rowMax = positiveValues.length ? Math.max(...positiveValues) : 0;

    return {
      name: category,
      data: MONTH_LABELS.map((label, monthIndex) => {
        const raw = rowValues[monthIndex];
        let normalized = 0;
        if (raw > 0) {
          normalized =
            rowMax === rowMin
              ? NORMALIZED_MAX
              : NORMALIZED_MIN +
                ((raw - rowMin) / (rowMax - rowMin)) *
                  (NORMALIZED_MAX - NORMALIZED_MIN);
        }
        return { x: label, y: normalized };
      }),
    };
  });

  const colorScale = HEATMAP_COLOR_SCALE;

  const chartOptions = useChart({
    chart: {
      toolbar: { show: false },
    },
    legend: {
      show: false,
    },
    stroke: {
      show: true,
      width: 1,
      colors: [theme.palette.background.paper],
    },
    dataLabels: {
      enabled: !isMobile,
      style: {
        colors: [LIGHT_TEXT],
      },
      formatter: (_val, opts) => {
        const raw =
          rawValues[opts?.seriesIndex ?? -1]?.[opts?.dataPointIndex ?? -1] ?? 0;
        return raw ? fCurrency(raw) : '';
      },
    },
    plotOptions: {
      heatmap: {
        radius: 2,
        enableShades: false,
        colorScale,
      },
    },
    colors: [TARGET_COLOR],
    xaxis: {
      type: 'category',
      axisTicks: { show: false },
      labels: {
        style: {
          colors: theme.palette.text.secondary,
        },
      },
    },
    yaxis: {
      labels: {
        style: {
          colors: theme.palette.text.secondary,
        },
      },
    },
    tooltip: {
      y: {
        formatter: (_val, opts) => {
          const raw =
            rawValues[opts?.seriesIndex ?? -1]?.[opts?.dataPointIndex ?? -1] ??
            0;
          return `${fCurrency(raw)} PLN`;
        },
      },
    },
  });

  const height = Math.max(350, categories.length * 45 + 100);

  return (
    <Card>
      <CardHeader title={title} />

      <Box sx={{ p: { xs: 1, sm: 3 } }}>
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
