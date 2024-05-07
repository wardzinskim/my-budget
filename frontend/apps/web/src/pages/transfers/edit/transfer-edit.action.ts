import {
  ActionFunction,
  ActionFunctionArgs,
  ParamParseKey,
  Params,
  redirect,
} from 'react-router-dom';
import { transferApi } from '../../../configuration/api';
import { IUserContextState } from '../../../hooks/user-context';
import { enqueueSnackbar } from 'notistack';
import { AxiosError } from 'axios';
import {
  HttpValidationProblemDetails,
  UpdateTransferRequest,
} from '@repo/api-client';
import { Paths } from '../../../routes/router';

interface TransferEditActionArgs extends ActionFunctionArgs {
  params: Params<ParamParseKey<typeof Paths.transferEdit>>;
}

export const action: (context: IUserContextState) => ActionFunction =
  (context: IUserContextState) =>
  async ({ request, params }: TransferEditActionArgs) => {
    if (!context.budget) throw new Error('budget context is not selected');

    const form = (await request.json()) as UpdateTransferRequest;
    return await transferApi
      .updateTransfer(context.budget.id!, params.id!, form)
      .then(() => {
        enqueueSnackbar({
          message: `Transfer updated.`,
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
