import { Helmet } from 'react-helmet-async';
import { useUserContext } from '../hooks/user-context';

// ----------------------------------------------------------------------

export default function DashboardPage() {
  const [userContext] = useUserContext();

  return (
    <>
      <Helmet>
        <title> Dashboard | MyBudget </title>
      </Helmet>
      {/* <AppView /> */}
      {userContext.budget?.id} {userContext.budget?.name}
    </>
  );
}
