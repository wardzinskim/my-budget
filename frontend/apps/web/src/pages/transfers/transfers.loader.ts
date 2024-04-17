import { LoaderFunction } from 'react-router-dom';
import { budgetApi } from '../../configuration/api';
import { IUserContextState } from '../../hooks/user-context';

export const loader: (context: IUserContextState) => LoaderFunction =
  (context: IUserContextState) => async () => {
    if (context.budget === null) return null;

    const result = await budgetApi.getTransfers(context.budget!.id!);
    return result.data;
  };
