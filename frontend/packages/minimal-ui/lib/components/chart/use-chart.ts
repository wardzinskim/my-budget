import { alpha, useTheme } from '@mui/material/styles';
import { Query, useResponsive } from '../../hooks/use-responsive';
import { merge } from 'lodash';

export const useChart = (options: ApexCharts.ApexOptions) => {
  const theme = useTheme();

  const smUp = useResponsive(Query.UP, 'sm');

  const LABEL_TOTAL = {
    show: true,
    label: 'Total',
    color: theme.palette.text.secondary,
    fontSize: theme.typography.subtitle2.fontSize as string | undefined,
    fontWeight: theme.typography.subtitle2.fontWeight,
    lineHeight: theme.typography.subtitle2.lineHeight,
    fontFamily: theme.typography.fontFamily,
  };

  const LABEL_VALUE = {
    offsetY: 8,
    color: theme.palette.text.primary,
    fontSize: theme.typography.h3.fontSize as string | undefined,
    fontWeight: theme.typography.h3.fontWeight,
    lineHeight: theme.typography.h3.lineHeight,
    fontFamily: theme.typography.fontFamily,
  };

  const baseOptions: ApexCharts.ApexOptions = {
    // Colors — 12 hues spread across the color wheel, all from theme palette
    colors: [
      theme.palette.primary.main, // indigo
      theme.palette.info.main, // cyan
      theme.palette.success.main, // green
      theme.palette.warning.main, // amber
      theme.palette.secondary.main, // violet
      theme.palette.error.main, // red-orange
      theme.palette.primary.light, // light indigo
      theme.palette.info.dark, // dark cyan
      theme.palette.success.light, // light green
      theme.palette.warning.dark, // dark amber
      theme.palette.secondary.light, // light violet
      theme.palette.error.light, // peach
    ],

    // Chart
    chart: {
      toolbar: { show: false },
      zoom: { enabled: false },
      animations: {
        enabled: true,
        speed: 400,
        animateGradually: { enabled: false },
        dynamicAnimation: { speed: 350 },
      },
      foreColor: theme.palette.text.disabled,
      fontFamily: theme.typography.fontFamily,
    },

    // States
    states: {
      hover: {
        filter: {
          type: 'lighten',
          // value: 0.04,
        },
      },
      active: {
        filter: {
          type: 'darken',
          // value: 0.88,
        },
      },
    },

    // Fill
    fill: {
      opacity: 1,
      gradient: {
        type: 'vertical',
        shadeIntensity: 0,
        opacityFrom: 0.4,
        opacityTo: 0,
        stops: [0, 100],
      },
    },

    // Datalabels
    dataLabels: {
      enabled: false,
      style: {
        colors: [theme.palette.text.primary],
      },
    },

    // Stroke
    stroke: {
      width: 3,
      curve: 'smooth',
      lineCap: 'round',
    },

    // Grid
    grid: {
      strokeDashArray: 4,
      borderColor: alpha(theme.palette.divider, 0.6),
      xaxis: {
        lines: {
          show: false,
        },
      },
    },

    // Xaxis
    xaxis: {
      axisBorder: { show: false },
      axisTicks: { show: false },
    },

    // Markers
    markers: {
      size: 0,
      strokeColors: theme.palette.background.paper,
    },

    // Tooltip
    tooltip: {
      //   theme: false,
      x: {
        show: true,
      },
    },

    // Legend
    legend: {
      show: true,
      fontSize: '13',
      position: 'top',
      horizontalAlign: 'right',
      fontWeight: 500,
      itemMargin: {
        horizontal: 8,
      },
      labels: {
        colors: theme.palette.text.primary,
      },
    },

    // plotOptions
    plotOptions: {
      // Bar
      bar: {
        borderRadius: smUp ? 3 : 1,
        columnWidth: '28%',
        borderRadiusApplication: 'end',
        borderRadiusWhenStacked: 'last',
      },

      // Pie + Donut
      pie: {
        donut: {
          labels: {
            show: true,
            value: LABEL_VALUE,
            total: LABEL_TOTAL,
          },
        },
      },

      // Radialbar
      radialBar: {
        track: {
          strokeWidth: '100%',
          background: alpha(theme.palette.grey[500], 0.16),
        },
        dataLabels: {
          value: LABEL_VALUE,
          total: LABEL_TOTAL,
        },
      },

      // Radar
      radar: {
        polygons: {
          fill: { colors: ['transparent'] },
          strokeColors: theme.palette.divider,
          connectorColors: theme.palette.divider,
        },
      },

      // polarArea
      polarArea: {
        rings: {
          strokeColor: theme.palette.divider,
        },
        spokes: {
          connectorColors: theme.palette.divider,
        },
      },
    },

    // Responsive
    responsive: [
      {
        // sm
        breakpoint: theme.breakpoints.values.sm,
        options: {
          plotOptions: { bar: { columnWidth: '40%' } },
        },
      },
      {
        // md
        breakpoint: theme.breakpoints.values.md,
        options: {
          plotOptions: { bar: { columnWidth: '32%' } },
        },
      },
    ],
  };

  return merge(baseOptions, options);
};
