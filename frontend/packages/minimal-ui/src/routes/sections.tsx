import { lazy, Suspense } from 'react';
import { Outlet, useRoutes } from 'react-router-dom';
import { NavItem } from '../../lib/layouts/nav';
import { Layout } from '../../lib/layouts';

export const IndexPage = lazy(() => import('../pages/app'));
// export const BlogPage = lazy(() => import('src/pages/blog'));
// export const UserPage = lazy(() => import('src/pages/user'));
// export const LoginPage = lazy(() => import('src/pages/login'));
// export const ProductsPage = lazy(() => import('src/pages/products'));
// export const Page404 = lazy(() => import('src/pages/page-not-found'));

// ----------------------------------------------------------------------

const navConfig: Array<NavItem> = [
  {
    title: 'dashboard',
    path: '/',
    icon: 'ic_analytics',
  },
  {
    title: 'user',
    path: '/user',
    icon: 'ic_user',
  },
  {
    title: 'product',
    path: '/products',
    icon: 'ic_cart',
  },
  {
    title: 'blog',
    path: '/blog',
    icon: 'ic_blog',
  },
  {
    title: 'login',
    path: '/login',
    icon: 'ic_lock',
  },
  {
    title: 'Not found',
    path: '/404',
    icon: 'ic_disabled',
  },
];

export default function Router() {
  const routes = useRoutes([
    {
      element: (
        <Layout navigationItems={navConfig}>
          <Suspense>
            <Outlet />
          </Suspense>
        </Layout>
      ),
      children: [
        { element: <IndexPage />, index: true },
        // { path: 'user', element: <UserPage /> },
        // { path: 'products', element: <ProductsPage /> },
        // { path: 'blog', element: <BlogPage /> },
      ],
    },
    // {
    //   path: 'login',
    //   element: <LoginPage />,
    // },
    // {
    //   path: '404',
    //   element: <Page404 />,
    // },
    // {
    //   path: '*',
    //   element: <Navigate to="/404" replace />,
    // },
  ]);

  return routes;
}
