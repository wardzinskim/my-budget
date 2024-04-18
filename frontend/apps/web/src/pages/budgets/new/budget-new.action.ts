import { ActionFunction, redirect } from 'react-router-dom';
import { budgetApi } from '../../../configuration/api';
import { AxiosError } from 'axios';
import {
  CreateBudgetRequest,
  HttpValidationProblemDetails,
} from '@repo/api-client';
import { enqueueSnackbar } from 'notistack';

export const action: ActionFunction = async ({ request }) => {
  const form = (await request.json()) as CreateBudgetRequest;
  return await budgetApi
    .createBudget(form)
    .then(() => {
      enqueueSnackbar({
        message: 'Budget created',
        variant: 'success',
      });

      return redirect('/budgets/');
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
