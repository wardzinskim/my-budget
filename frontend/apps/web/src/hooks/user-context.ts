import { BudgetDTO } from '@repo/api-client';
import { Dispatch, SetStateAction, createContext, useContext } from 'react';

export interface IUserContextState {
  budget?: BudgetDTO;
}

export interface IUserContext {
  userContext: IUserContextState;
  setUserContext: Dispatch<SetStateAction<IUserContextState>>;
}

export const UserContext = createContext<IUserContext>({
  userContext: {},
  setUserContext: () => {},
});

export const useUserContext: () => [
  IUserContextState,
  Dispatch<SetStateAction<IUserContextState>>,
] = () => {
  const context = useContext<IUserContext>(UserContext);

  return [context.userContext, context.setUserContext];
};
