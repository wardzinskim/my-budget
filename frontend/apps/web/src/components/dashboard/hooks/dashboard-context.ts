import { Dispatch, SetStateAction, createContext, useContext } from 'react';

export interface IDashboardContextState {
  year: number;
  month: number;
}

export interface IDashboardContext {
  dashboardContext: IDashboardContextState;
  setDashboardContext: Dispatch<SetStateAction<IDashboardContextState>>;
}

export const DashboardContext = createContext<IDashboardContext>({
  dashboardContext: {
    year: new Date().getFullYear(),
    month: new Date().getMonth(),
  },
  setDashboardContext: () => {},
});

export const useDashboardContext: () => [
  IDashboardContextState,
  Dispatch<SetStateAction<IDashboardContextState>>,
] = () => {
  const context = useContext<IDashboardContext>(DashboardContext);

  return [context.dashboardContext, context.setDashboardContext];
};
