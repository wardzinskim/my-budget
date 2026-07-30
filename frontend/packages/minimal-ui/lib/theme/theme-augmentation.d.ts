import type { CustomShadows } from './custom-shadows';

// ----------------------------------------------------------------------
// Extends MUI's theme typings with the custom tokens used across the app:
// - `lighter` / `darker` variants for palette colors
// - `theme.customShadows` (colored/soft shadows used by cards & buttons)

declare module '@mui/material/styles' {
  interface PaletteColor {
    lighter?: string;
    darker?: string;
  }

  interface SimplePaletteColorOptions {
    lighter?: string;
    darker?: string;
  }

  interface Theme {
    customShadows: CustomShadows;
  }

  interface ThemeOptions {
    customShadows?: CustomShadows;
  }
}

export {};
