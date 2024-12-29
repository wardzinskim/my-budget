import {
  ActionFunction,
  ActionFunctionArgs,
  ParamParseKey,
  Params,
} from 'react-router-dom';
import { budgetApi } from '../../../../configuration/api';
import { enqueueSnackbar } from 'notistack';
import { Paths } from '../../../../routes/router';
import {
  HttpValidationProblemDetails,
  ShareBudgetRequest,
} from '@repo/api-client';
import { AxiosError } from 'axios';

interface BudgetSharesLoaderArgs extends ActionFunctionArgs {
  params: Params<ParamParseKey<typeof Paths.budgetShares>>;
}

interface ActionParams extends ShareBudgetRequest {
  intent: 'share' | 'unshare';
}

export const action: ActionFunction = async ({
  request,
  params,
}: BudgetSharesLoaderArgs) => {
  const form = (await request.json()) as ActionParams;

  if (form.intent == 'share') {
    return await shareBudget(params.id!, form);
  }

  return null;
};

const shareBudget = async (budgetId: string, form: ShareBudgetRequest) => {
  return await budgetApi
    .shareBudget(budgetId, form)
    .then(() => {
      enqueueSnackbar({
        message: 'Budget shared',
        variant: 'success',
      });
      return null;
    })
    .catch((error: AxiosError<HttpValidationProblemDetails>) => {
      if (error.response?.data.type == 'ValidationException') {
        enqueueSnackbar('Validation Errors Occurred', {
          variant: 'error',
        });
        return error.response.data.errors;
      } else {
        enqueueSnackbar(error.response?.data.detail, {
          variant: 'error',
        });
      }
      return {
        ['name']: [error.response?.data.detail],
      };
    });
};
