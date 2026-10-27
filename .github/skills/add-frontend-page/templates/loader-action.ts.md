# Loader / action template

Loader:

```ts
import { LoaderFunction } from 'react-router-dom';
import { IUserContextState } from '../../hooks/user-context';
import { transferApi } from '../../configuration/api';

export const loader: (context: IUserContextState) => LoaderFunction =
  (context: IUserContextState) => async () => {
    if (!context.budget) return null;

    const result = await transferApi.getTransfers(context.budget.id!);
    return result.data;
  };
```

Action:

```ts
import { ActionFunction, redirect } from 'react-router-dom';
import { enqueueSnackbar } from 'notistack';
import { AxiosError } from 'axios';
import { HttpValidationProblemDetails, <Request> } from '@repo/api-client';
import { IUserContextState } from '../../hooks/user-context';
import { transferApi } from '../../configuration/api';

export const action: (context: IUserContextState) => ActionFunction =
  (context: IUserContextState) =>
  async ({ request }) => {
    if (!context.budget) throw new Error('budget context is not selected');

    const form = (await request.json()) as <Request>;
    return await transferApi
      .<operation>(context.budget.id!, form)
      .then(() => {
        enqueueSnackbar({ message: '<Done>.', variant: 'success' });
        return redirect('/<path>');
      })
      .catch((error: AxiosError<HttpValidationProblemDetails>) => {
        if (error.response?.data.type == 'ValidationException') {
          enqueueSnackbar('Validation Errors Occurred', { variant: 'error' });
          return error.response.data.errors;
        }
        enqueueSnackbar(error.response?.data.detail, { variant: 'error' });
        return {};
      });
  };
```

Budget-independent loaders/actions can be plain `LoaderFunction`/`ActionFunction` exports (see `budgets.loader.ts`).
