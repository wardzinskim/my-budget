import { useMemo } from 'react';
import { To, useNavigate } from 'react-router-dom';

// ----------------------------------------------------------------------

export function useRouter() {
  const navigate = useNavigate();

  const router = useMemo(
    () => ({
      back: () => navigate(-1),
      forward: () => navigate(1),
      reload: () => window.location.reload(),
      push: (href: To) => navigate(href),
      replace: (href: To) => navigate(href, { replace: true }),
    }),
    [navigate]
  );

  return router;
}
