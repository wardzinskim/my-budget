import {
  ActionFunctionArgs,
  LoaderFunction,
  ParamParseKey,
  Params,
} from 'react-router-dom';
import { IUserContextState } from '../../../hooks/user-context';
import { budgetApi } from '../../../configuration/api';
import { Paths } from '../../../routes/router';

interface TransferEditLoaderArgs extends ActionFunctionArgs {
  params: Params<ParamParseKey<typeof Paths.transferEdit>>;
}

export const loader: (context: IUserContextState) => LoaderFunction =
  (context: IUserContextState) =>
  async ({ params }: TransferEditLoaderArgs) => {
    if (!context.budget) return null;

    const result = await budgetApi.getTransfer(context.budget.id!, params.id!);
    return result.data;
  };
