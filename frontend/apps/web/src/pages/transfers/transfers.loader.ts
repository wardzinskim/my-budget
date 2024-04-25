import { LoaderFunction } from 'react-router-dom';
import { budgetApi } from '../../configuration/api';
import { IUserContextState } from '../../hooks/user-context';
import { TransferDTOType } from '@repo/api-client';

export const loader: (context: IUserContextState) => LoaderFunction =
  (context: IUserContextState) =>
  async ({ request }) => {
    const searchParams = new URL(request.url).searchParams;

    const result = await budgetApi.getTransfers(
      context.budget!.id!,
      searchParams.get('type') as TransferDTOType | undefined,
      searchParams.get('dateFrom')
        ? new Date(searchParams.get('dateFrom')!)
        : undefined,
      searchParams.get('dateTo')
        ? new Date(searchParams.get('dateTo')!)
        : undefined
    );
    return result.data;
  };
