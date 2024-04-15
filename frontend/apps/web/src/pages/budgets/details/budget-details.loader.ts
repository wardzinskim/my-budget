import {
  ActionFunctionArgs,
  LoaderFunction,
  ParamParseKey,
  Params,
} from 'react-router-dom';
import { budgetApi } from '../../../configuration/api';
import { Paths } from '../../../routes/router';

interface BudgetDetailsLoaderArgs extends ActionFunctionArgs {
  params: Params<ParamParseKey<typeof Paths.budgetDetails>>;
}

export const loader: LoaderFunction = async ({
  params,
}: BudgetDetailsLoaderArgs) => {
  const result = await budgetApi.getBudget(params.id!);
  return result.data;
};
