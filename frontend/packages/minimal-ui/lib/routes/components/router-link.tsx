import { forwardRef } from 'react';
import { Link, To } from 'react-router-dom';

// ----------------------------------------------------------------------
interface RouterLinkProps {
  href: To;
}

const RouterLink = forwardRef<HTMLAnchorElement, RouterLinkProps>(
  ({ href, ...other }: RouterLinkProps, ref) => (
    <Link ref={ref} to={href} {...other} />
  )
);

export default RouterLink;
