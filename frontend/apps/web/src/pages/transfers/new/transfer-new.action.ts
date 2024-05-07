import { ActionFunction, redirect } from 'react-router-dom';
import { transferApi } from '../../../configuration/api';
import { IUserContextState } from '../../../hooks/user-context';
import { enqueueSnackbar } from 'notistack';
import { AxiosError } from 'axios';
import {
  CreateTransferRequest,
  HttpValidationProblemDetails,
} from '@repo/api-client';

export const action: (context: IUserContextState) => ActionFunction =
  (context: IUserContextState) =>
  async ({ request }) => {
    if (!context.budget) throw new Error('budget context is not selected');

    const form = (await request.json()) as CreateTransferRequest;
    return await transferApi
      .addTransfer(context.budget.id!, form)
      .then(() => {
        enqueueSnackbar({
          message: `${form.type} added.`,
          variant: 'success',
        });

        return redirect('/transfers/');
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
        return {};
      });
  };
