import { forwardRef } from 'react';

import Box from '@mui/material/Box';
import { SxProps, Theme, useTheme } from '@mui/material/styles';

import { ColorType, StyledLabel, VariantType } from './styles';

// ----------------------------------------------------------------------

interface LabelProps extends React.PropsWithChildren {
  color: ColorType;
  variant: VariantType;
  startIcon?: JSX.Element;
  endIcon?: JSX.Element;
  sx?: SxProps<Theme>;
}

export const Label = forwardRef<unknown, LabelProps>(
  (
    {
      children,
      color = 'default',
      variant = 'soft',
      startIcon,
      endIcon,
      sx,
      ...other
    },
    ref
  ) => {
    const theme = useTheme();

    const iconStyles = {
      width: 16,
      height: 16,
      '& svg, img': { width: 1, height: 1, objectFit: 'cover' },
    };

    return (
      <StyledLabel
        ref={ref}
        component="span"
        ownerState={{ color, variant }}
        sx={{
          ...(startIcon && { pl: 0.75 }),
          ...(endIcon && { pr: 0.75 }),
          ...sx,
        }}
        theme={theme}
        {...other}
      >
        {startIcon && <Box sx={{ mr: 0.75, ...iconStyles }}> {startIcon} </Box>}

        {children}

        {endIcon && <Box sx={{ ml: 0.75, ...iconStyles }}> {endIcon} </Box>}
      </StyledLabel>
    );
  }
);
