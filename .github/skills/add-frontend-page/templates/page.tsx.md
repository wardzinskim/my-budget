# Page template

```tsx
import { Helmet } from 'react-helmet-async';
import { <Name>View } from '../../components/<area>/view/<name>-view';

const <Name>Page: React.FC = () => {
  return (
    <>
      <Helmet>
        <title> <Title> | myBudget </title>
      </Helmet>

      <<Name>View />
    </>
  );
};

export default <Name>Page;
```

Route entry:

```tsx
export const <Name>Page = lazy(() => import('../pages/<area>/<name>'));
// ...
{
  path: '/<path>',
  element: <<Name>Page />,
  loader: <name>PageLoader(userContext),
  action: <name>PageAction(userContext),
},
```
