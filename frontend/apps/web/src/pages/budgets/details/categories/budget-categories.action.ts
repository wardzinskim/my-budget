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
  CreateBudgetCategoryRequest,
  HttpValidationProblemDetails,
} from '@repo/api-client';
import { AxiosError } from 'axios';

interface BudgetCategoriesLoaderArgs extends ActionFunctionArgs {
  params: Params<ParamParseKey<typeof Paths.budgetDetails>>;
}

interface ActionParams extends CreateBudgetCategoryRequest {
  intent: 'archive' | 'create';
}

export const action: ActionFunction = async ({
  request,
  params,
}: BudgetCategoriesLoaderArgs) => {
  const form = (await request.json()) as ActionParams;

  if (form.intent == 'create') {
    return await createCategory(params.id!, form);
  }
  if (form.intent == 'archive') {
    return await archiveCategory(params.id!, form.name!);
  }
  return null;
};

const createCategory = async (
  budgetId: string,
  form: CreateBudgetCategoryRequest
) => {
  return await budgetApi
    .createBudgetCategory(budgetId, form)
    .then(() => {
      enqueueSnackbar({
        message: 'Budget category created',
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

const archiveCategory = async (budgetId: string, name: string) => {
  return await budgetApi
    .archiveBudgetCategory(budgetId, name)
    .then(() => {
      enqueueSnackbar({
        message: 'Budget category archived',
        variant: 'success',
      });
      return null;
    })
    .catch((error: AxiosError<HttpValidationProblemDetails>) => {
      enqueueSnackbar(error.response?.data.detail, {
        variant: 'error',
      });
      return {
        ['name']: [error.response?.data.detail],
      };
    });
};
