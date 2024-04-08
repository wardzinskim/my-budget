import { LoaderFunction } from 'react-router-dom';
import { budgetApi } from '../configuration/api';

export const loader: LoaderFunction = async () => {
  const result = await budgetApi.getBudgets();
  return result.data;
};
