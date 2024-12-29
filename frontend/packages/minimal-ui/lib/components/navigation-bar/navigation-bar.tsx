import { Stack, Theme, useTheme } from '@mui/material';
import { NavItem } from '../../layouts/nav';

interface NavigationBarProps {
  type: 'horizontal';
  items: Array<NavItem>;
}

export const NavigationBar: React.FC<NavigationBarProps> = ({ items }) => {
  const theme: Theme = useTheme();

  return (
    <Stack
      component="nav"
      direction="row"
      display="inline-flex"
      spacing={2}
      marginBottom={theme.spacing(2)}
    >
      {items.map((item) => (
        <NavItem key={item.title} item={item} />
      ))}
    </Stack>
  );
};
