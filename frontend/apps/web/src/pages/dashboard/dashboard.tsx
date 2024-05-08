import { Helmet } from 'react-helmet-async';
import { DashboardView } from '../../components/dashboard/view/dashboard-view';

// ----------------------------------------------------------------------

export default function DashboardPage() {
  return (
    <>
      <Helmet>
        <title> Dashboard | MyBudget </title>
      </Helmet>
      <DashboardView />
    </>
  );
}
