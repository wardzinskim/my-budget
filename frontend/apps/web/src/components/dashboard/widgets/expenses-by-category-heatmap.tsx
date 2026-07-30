import { Box, Card, CardHeader, useMediaQuery, useTheme } from '@mui/material';
import { CategoryMonthValue, TransferDTOType } from '@repo/api-client';
import { Chart, fCurrency, useChart } from '@repo/minimal-ui';
import { useCallback, useRef } from 'react';
import { useNavigate } from 'react-router-dom';

interface ExpensesByCategoryHeatmapProps {
  data: CategoryMonthValue[];
  year: number;
  transferType?: TransferDTOType;
  title?: string;
}

// A second click on the same cell within this window counts as a double-click.
const DOUBLE_CLICK_THRESHOLD_MS = 400;

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
> = ({
  data,
  year,
  transferType = TransferDTOType.Expense,
  title = 'Expenses by category and month',
}) => {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('sm'));
  const navigate = useNavigate();

  const categories = Array.from(
    new Set(data.map((x) => x.category ?? 'uncategorized'))
  );

  // Tracks the previous click so a second click on the same cell shortly
  // after can be treated as a double-click (ApexCharts doesn't expose a
  // native dblclick event for heatmap cells).
  const lastClickRef = useRef<{
    seriesIndex: number;
    dataPointIndex: number;
    time: number;
  } | null>(null);

  const navigateToTransfers = useCallback(
    async (categoryIndex: number, monthIndex: number) => {
      const category = categories[categoryIndex];
      if (!category) return;

      const month = monthIndex + 1;
      const dateFrom = new Date(Date.UTC(year, month - 1, 1));
      const dateTo = new Date(Date.UTC(year, month, 1));

      const params = new URLSearchParams({
        type: transferType,
        category,
        dateFrom: dateFrom.toISOString(),
        dateTo: dateTo.toISOString(),
      });

      await navigate(`/transfers?${params.toString()}`);
    },
    [categories, navigate, transferType, year]
  );

  const handleChartClick = useCallback(
    async (
      _event: MouseEvent,
      _chartContext?: ApexCharts,
      opts?: { seriesIndex?: number; dataPointIndex?: number }
    ) => {
      // ApexCharts reads these from DOM attributes internally and does not
      // parse them to numbers, so they can arrive as numeric strings (e.g.
      // "1"). Coerce explicitly, otherwise `monthIndex + 1` further down
      // would do string concatenation ("1" + 1 = "11") instead of addition.
      const seriesIndex =
        opts?.seriesIndex != null ? Number(opts.seriesIndex) : NaN;
      const dataPointIndex =
        opts?.dataPointIndex != null ? Number(opts.dataPointIndex) : NaN;
      if (
        Number.isNaN(seriesIndex) ||
        seriesIndex < 0 ||
        Number.isNaN(dataPointIndex) ||
        dataPointIndex < 0
      ) {
        return;
      }

      const now = Date.now();
      const last = lastClickRef.current;
      if (
        last &&
        last.seriesIndex === seriesIndex &&
        last.dataPointIndex === dataPointIndex &&
        now - last.time < DOUBLE_CLICK_THRESHOLD_MS
      ) {
        lastClickRef.current = null;
        await navigateToTransfers(seriesIndex, dataPointIndex);
      } else {
        lastClickRef.current = { seriesIndex, dataPointIndex, time: now };
      }
    },
    [navigateToTransfers]
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

  // Color intensity is normalized per row (category) against a fixed lower
  // bound of 0 (not the category's own minimum), so a month with a small
  // expense relative to that category's max always looks proportionally
  // "cooler" instead of being stretched up to full intensity.
  const series = categories.map((category, categoryIndex) => {
    const rowValues = rawValues[categoryIndex];
    const rowMax = Math.max(0, ...rowValues);

    return {
      name: category,
      data: MONTH_LABELS.map((label, monthIndex) => {
        const raw = rowValues[monthIndex];
        const normalized =
          raw > 0 && rowMax > 0
            ? NORMALIZED_MIN +
              (raw / rowMax) * (NORMALIZED_MAX - NORMALIZED_MIN)
            : 0;
        return { x: label, y: normalized };
      }),
    };
  });

  const colorScale = HEATMAP_COLOR_SCALE;

  const chartOptions = useChart({
    chart: {
      toolbar: { show: false },
      events: {
        click: handleChartClick,
      },
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

      <Box sx={{ p: 0, pb: 0 }}>
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
