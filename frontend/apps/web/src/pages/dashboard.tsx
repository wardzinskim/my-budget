import { Helmet } from 'react-helmet-async';
import { useUserContext } from '../hooks/user-context';
import { DashboardView } from '../components/dashboard/view/dashboard-view';

// ----------------------------------------------------------------------

export default function DashboardPage() {
  const [userContext] = useUserContext();

  return (
    <>
      <Helmet>
        <title> Dashboard | MyBudget </title>
      </Helmet>
      <DashboardView />
    </>
  );
}
