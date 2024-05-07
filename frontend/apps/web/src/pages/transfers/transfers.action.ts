import { ActionFunction } from 'react-router-dom';
import { transferApi } from '../../configuration/api';
import { IUserContextState } from '../../hooks/user-context';
import { enqueueSnackbar } from 'notistack';
import { AxiosError } from 'axios';
import { ProblemDetails } from '@repo/api-client';

interface ActionParams {
  intent: 'delete';
  transferId?: string;
}

export const action: (context: IUserContextState) => ActionFunction =
  (context: IUserContextState) =>
  async ({ request }) => {
    if (context.budget === null) return null;

    const form = (await request.json()) as ActionParams;
    if (form.intent === 'delete') {
      return await deleteTransfer(context.budget!.id!, form.transferId!);
    }
    return null;
  };

const deleteTransfer = async (budgetId: string, transferId: string) =>
  await transferApi
    .deleteTransfer(budgetId, transferId)
    .then(() => {
      enqueueSnackbar({
        message: 'Transfer deleted',
        variant: 'success',
      });
      return null;
    })
    .catch((error: AxiosError<ProblemDetails>) => {
      enqueueSnackbar(error.response?.data.detail, {
        variant: 'error',
      });
      return null;
    });
