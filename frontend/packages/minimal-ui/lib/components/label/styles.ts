import Box from '@mui/material/Box';
import { alpha, styled, PaletteColor } from '@mui/material/styles';

// ----------------------------------------------------------------------

export type ColorType =
  | 'default'
  | 'primary'
  | 'secondary'
  | 'info'
  | 'success'
  | 'warning'
  | 'error';
export type VariantType = 'filled' | 'outlined' | 'ghost' | 'soft';

interface StyledLabelProps {
  color: ColorType;
  variant: VariantType;
}

export const StyledLabel = styled(Box, {
  shouldForwardProp: (prop) => prop !== 'color' && prop !== 'variant',
})<StyledLabelProps>(({ theme, color, variant }) => {
  const lightMode = theme.palette.mode === 'light';

  const filledVariant = variant === 'filled';

  const outlinedVariant = variant === 'outlined';

  const softVariant = variant === 'soft';

  const defaultStyle = {
    ...(color === 'default' && {
      // FILLED
      ...(filledVariant && {
        color: lightMode ? theme.palette.common.white : theme.palette.grey[800],
        backgroundColor: theme.palette.text.primary,
      }),
      // OUTLINED
      ...(outlinedVariant && {
        backgroundColor: 'transparent',
        color: theme.palette.text.primary,
        border: `2px solid ${theme.palette.text.primary}`,
      }),
      // SOFT
      ...(softVariant && {
        color: theme.palette.text.secondary,
        backgroundColor: alpha(theme.palette.grey[500], 0.16),
      }),
    }),
  };

  // eslint-disable-next-line @typescript-eslint/no-unsafe-member-access, @typescript-eslint/no-explicit-any
  const paletteColor = (theme.palette as any)[color] as PaletteColor;

  const colorStyle = {
    ...(color !== 'default' && {
      // FILLED
      ...(filledVariant && {
        color: paletteColor.contrastText,
        backgroundColor: paletteColor.main,
      }),
      // OUTLINED
      ...(outlinedVariant && {
        backgroundColor: 'transparent',
        color: paletteColor.main,
        border: `2px solid ${paletteColor.main}`,
      }),
      // SOFT
      ...(softVariant && {
        color: paletteColor[lightMode ? 'dark' : 'light'],
        backgroundColor: alpha(paletteColor.main, 0.16),
      }),
    }),
  };

  return {
    height: 24,
    minWidth: 24,
    lineHeight: 0,
    borderRadius: 6,
    cursor: 'default',
    alignItems: 'center',
    whiteSpace: 'nowrap',
    display: 'inline-flex',
    justifyContent: 'center',
    padding: theme.spacing(0, 0.75),
    fontSize: theme.typography.pxToRem(12),
    fontWeight: theme.typography.fontWeightBold,
    transition: theme.transitions.create('all', {
      duration: theme.transitions.duration.shorter,
    }),
    ...defaultStyle,
    ...colorStyle,
  };
});
