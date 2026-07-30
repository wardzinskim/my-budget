import { createContext, useContext } from 'react';

// ----------------------------------------------------------------------

export type ColorMode = 'light' | 'dark';

export const COLOR_MODE_STORAGE_KEY = 'my-budget-color-mode';

interface ColorModeContextValue {
  mode: ColorMode;
  toggleColorMode: () => void;
  setMode: (mode: ColorMode) => void;
}

export const ColorModeContext = createContext<ColorModeContextValue>({
  mode: 'light',
  toggleColorMode: () => {},
  setMode: () => {},
});

export const useColorMode = () => useContext(ColorModeContext);

export function getInitialColorMode(): ColorMode {
  if (typeof window === 'undefined') return 'light';

  const stored = window.localStorage.getItem(COLOR_MODE_STORAGE_KEY);
  if (stored === 'light' || stored === 'dark') return stored;

  if (
    window.matchMedia &&
    window.matchMedia('(prefers-color-scheme: dark)').matches
  ) {
    return 'dark';
  }

  return 'light';
}
